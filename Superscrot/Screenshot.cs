using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Superscrot.Uploaders;

namespace Superscrot
{
    /// <summary>
    /// Represents a single taken screenshot.
    /// </summary>
    public class Screenshot : IDisposable
    {
        /// <summary>
        /// #0D0B0C, a color that Windows doesn't seem to like very much.
        /// </summary>
        protected static readonly Color ThatFuckingColor =
            Color.FromArgb(0xFF, 0x0D, 0x0B, 0x0C);

        private Bitmap bitmap;
        private DateTime createdDate;
        private Guid guid;
        private string serverPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Screenshot"/> class.
        /// </summary>
        public Screenshot()
        {
            createdDate = DateTime.Now;
            guid = Guid.NewGuid();
        }

        /// <summary>
        /// Occurs when a file could not be deleted.
        /// </summary>
        public event EventHandler DeleteFailed;

        /// <summary>
        /// Occurs when a file was deleted succesfully.
        /// </summary>
        public event EventHandler DeleteSucceeded;

        /// <summary>
        /// Occurs when a duplicate file has been found on the server.
        /// </summary>
        public event EventHandler<DuplicateFileEventArgs> DuplicateFileFound;

        /// <summary>
        /// Occurs when an upload has failed.
        /// </summary>
        public event EventHandler UploadFailed;

        /// <summary>
        /// Occurs before the screenshot starts uploading.
        /// </summary>
        public event EventHandler<UploadingEventArgs> Uploading;

        /// <summary>
        /// Occurs when an upload has succeeded.
        /// </summary>
        public event EventHandler UploadSucceeded;

        /// <summary>
        /// Specifies the source of a screenshot.
        /// </summary>
        public enum ScreenshotSource
        {
            /// <summary>
            /// The screenshot contains the user's entire desktop.
            /// </summary>
            Desktop,

            /// <summary>
            /// The screenshot contains an image from the clipboard.
            /// </summary>
            Clipboard,

            /// <summary>
            /// The screenshot was taken from a user-selected region on the screen.
            /// </summary>
            RegionCapture,

            /// <summary>
            /// The screenshot was taken from the active window.
            /// </summary>
            WindowCapture,

            /// <summary>
            /// The screenshot contains an image from a file.
            /// </summary>
            File
        }

        /// <summary>
        /// Gets or sets a bitmap image of the screenshot.
        /// </summary>
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }

        /// <summary>
        /// Gets a value indicating the date and time the screenshot was made.
        /// </summary>
        public DateTime Created
        {
            get { return createdDate; }
        }

        /// <summary>
        /// Gets a globally unique identifier for this screenshot.
        /// </summary>
        public Guid Guid
        {
            get { return guid; }
        }

        /// <summary>
        /// Gets a value that indicates whether the screenshot is based on a file.
        /// </summary>
        public bool IsFile
        {
            get { return Source == ScreenshotSource.File; }
        }

        /// <summary>
        /// Gets a value indicating whether the screenshot has been uploaded.
        /// </summary>
        public bool IsUploaded
        {
            get { return !string.IsNullOrWhiteSpace(PublicUrl); }
        }

        /// <summary>
        /// Gets or sets the original filename that the screenshot originates
        /// from, or <c>null</c> for non file-based captures.
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets the public URL to the file on the server, or <c>null</c> if the
        /// screenshot hasn't been uploaded yet.
        /// </summary>
        public string PublicUrl { get; private set; }

        /// <summary>
        /// Gets or sets the path on the server, or <c>null</c> if the
        /// screenshot hasn't been uploaded yet.
        /// </summary>
        public string ServerPath
        {
            get { return serverPath; }
            set
            {
                if (value != serverPath)
                {
                    serverPath = value;
                    PublicUrl = PathUtility.TranslateServerPath(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the source of the screenshot.
        /// </summary>
        public ScreenshotSource Source { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the owner of the window the
        /// screenshot was taken of, or <c>null</c>.
        /// </summary>
        public string WindowOwner { get; set; }

        /// <summary>
        /// Gets or sets the title of the window the screenshot was taken of, or
        /// <c>null</c> for non-window captures.
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// Retrieves an image with the contents of the active window.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> with the active window capture.</returns>
        public static Screenshot FromActiveWindow()
        {
            try
            {
                Screenshot screenshot = new Screenshot();
                screenshot.Source = ScreenshotSource.WindowCapture;

                using (var window = NativeWindow.ForegroundWindow())
                {
                    screenshot.WindowTitle = window.Caption;
                    screenshot.WindowOwner = window.ToString();

                    screenshot.Bitmap = new Bitmap(window.Width, window.Height);
                    using (Graphics g = Graphics.FromImage(screenshot.Bitmap))
                    {
                        g.Clear(ThatFuckingColor);
                        g.CopyFromScreen(window.Location, Point.Empty,
                            window.Size, CopyPixelOperation.SourceCopy);
                    }
                    return screenshot;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
            }

            return null;
        }

        /// <summary>
        /// Creates a new <see cref="Superscrot.Screenshot"/> instance based on
        /// the image data on the clipboard.
        /// </summary>
        /// <returns>
        /// A <see cref="Superscrot.Screenshot"/> based on the clipboard image.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The clipboard is empty or does not contain an image.
        /// </exception>
        public static Screenshot FromClipboard()
        {
            if (!Clipboard.ContainsImage())
                throw new InvalidOperationException(SR.NoClipboardImage);

            try
            {
                Screenshot screenshot = new Screenshot();
                screenshot.Source = ScreenshotSource.Clipboard;
                screenshot.Bitmap = (Bitmap)Clipboard.GetImage();
                return screenshot;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
                return null;
            }
        }

        /// <summary>
        /// Retrieves an image containing a screenshot of the user's entire desktop.
        /// </summary>
        /// <returns>
        /// A new <see cref="Screenshot"/> with an image containg a screenshot
        /// of all screens combined.
        /// </returns>
        public static Screenshot FromDesktop()
        {
            try
            {
                var screenshot = new Screenshot();
                screenshot.Source = ScreenshotSource.Desktop;

                var bounds = GetDesktopBounds();
                screenshot.Bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(screenshot.Bitmap))
                {
                    foreach (var screen in Screen.AllScreens)
                    {
                        var destination = new Point(
                            screen.Bounds.X + Math.Abs(bounds.Left),
                            screen.Bounds.Y + Math.Abs(bounds.Top));
                        using (var screenBitmap = CopyFromScreen(screen))
                        {
                            g.DrawImageUnscaled(screenBitmap, destination);
                        }
                    }
                }

                return screenshot;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
            }
            return null;
        }

        /// <summary>
        /// Creates a new <see cref="Superscrot.Screenshot"/> instance based on
        /// the specified image file.
        /// </summary>
        /// <returns>
        /// A <see cref="Superscrot.Screenshot"/> based on the specified image file.
        /// </returns>
        public static Screenshot FromFile(string path)
        {
            try
            {
                Screenshot screenshot = new Screenshot();
                screenshot.Source = ScreenshotSource.File;
                screenshot.OriginalFileName = path;

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    using (var image = Image.FromStream(stream))
                    {
                        screenshot.Bitmap = new Bitmap(image);
                    }
                }

                return screenshot;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
                return null;
            }
        }

        /// <summary>
        /// Shows an overlay over the screen that allows the user to select a
        /// region, of which the image is captured and returned.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> with the selected region.</returns>
        public static Screenshot FromRegion()
        {
            try
            {
                Screenshot screenshot = new Screenshot();
                screenshot.Source = ScreenshotSource.RegionCapture;
                RegionOverlay overlay = new RegionOverlay();
                if (overlay.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = overlay.SelectedRegion;
                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        screenshot.Bitmap = new Bitmap(rect.Width, rect.Height);
                        using (Graphics g = Graphics.FromImage(screenshot.Bitmap))
                        {
                            g.Clear(ThatFuckingColor);
                            g.CopyFromScreen(rect.X, rect.Y, 0, 0,
                                new Size(rect.Width, rect.Height),
                                CopyPixelOperation.SourceCopy);
                        }
                        return screenshot;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
            }

            return null;
        }

        /// <summary>
        /// Calculates the size (in bytes) of the screenshot.
        /// </summary>
        /// <returns>The size of the screenshot, in bytes.</returns>
        public long CalculateSize()
        {
            if (File.Exists(OriginalFileName))
            {
                var info = new FileInfo(OriginalFileName);
                return info.Length;
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    Save(stream);
                    return stream.Length;
                }
            }
        }

        /// <summary>
        /// Deletes the screenshot from the server.
        /// </summary>
        /// <returns>A value indicating whether the deletion succeeded.</returns>
        public bool Delete()
        {
            var uploader = Uploader.Create(Program.Config);
            return uploader.Delete(ServerPath);
        }

        /// <summary>
        /// Asynchronously deletes the screenshot from the server.
        /// </summary>
        /// <returns>A value indicating whether the deletion succeeded.</returns>
        public async Task<bool> DeleteAsync()
        {
            return await Task.Run(() =>
            {
                return Delete();
            });
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Screenshot"/> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a string that contains the filename for this screenshot,
        /// formatted using the program settings.
        /// </summary>
        public string GetFileName()
        {
            return GetFileName(Program.Config.FilenameFormat);
        }

        /// <summary>
        /// Gets a string that contains the filename for this screenshot,
        /// formatted using the specified format string.
        /// </summary>
        /// <param name="format">The composite format string.</param>
        public string GetFileName(string format)
        {
            var fileName = Path.GetFileNameWithoutExtension(OriginalFileName);
            var args = new StringDictionary();
            args.Add("machine", Environment.MachineName);
            args.Add("width", Bitmap.Width.ToString());
            args.Add("height", Bitmap.Height.ToString());
            args.Add("window", WindowTitle);
            args.Add("process", WindowOwner);
            args.Add("file", fileName);
            args.Add("guid", Guid.ToString("N"));

            // Date/time related placeholders
            args.Add("time", Created.ToString("yyyyMMddHHmmssffff"));
            args.Add("unix", Created.ToUnixTimestamp().ToString());

            switch (Source)
            {
                case ScreenshotSource.Desktop:
                    args.Add("source", "Desktop");
                    args.Add("name", $"{Bitmap.Width}x{Bitmap.Height}");
                    break;

                case ScreenshotSource.Clipboard:
                    args.Add("source", "Clipboard");
                    args.Add("name", $"{Bitmap.Width}x{Bitmap.Height}");
                    break;

                case ScreenshotSource.RegionCapture:
                    args.Add("source", "Capture");
                    args.Add("name", $"{Bitmap.Width}x{Bitmap.Height}");
                    break;

                case ScreenshotSource.WindowCapture:
                    args.Add("source", "Window");
                    args.Add("name", WindowOwner);
                    break;

                case ScreenshotSource.File:
                    args.Add("source", "File");
                    args.Add("name", fileName);
                    break;
            }

            var result = PathUtility.Format(format, args, Created);
            var ext = Program.Config.UseCompression ? "jpg" : "png";
            if (Source == ScreenshotSource.File)
                ext = Path.GetExtension(OriginalFileName);
            return Path.ChangeExtension(result, ext);
        }

        /// <summary>
        /// Saves the screenshot to a temporary file and returns the filename.
        /// If the screenshot originated from a file, that filename is returned
        /// instead and nothing is written to disk.
        /// </summary>
        /// <returns>The filename of the screenshot.</returns>
        public string Save()
        {
            if (Source == ScreenshotSource.File)
            {
                return OriginalFileName;
            }
            else
            {
                var tempFile = Path.GetTempFileName();
                return Save(tempFile);
            }
        }

        /// <summary>
        /// Saves the screenshot to a new file with the specified name in an
        /// image format based on the current program settings.
        /// </summary>
        /// <param name="path">The name of the file to save to.</param>
        /// <returns>The name of the file saved to.</returns>
        public string Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                Save(stream);
            }
            return path;
        }

        /// <summary>
        /// Saves this screenshot to the specified stream in an image format
        /// based on the current program settings.
        /// </summary>
        /// <param name="destination">
        /// The <see cref="System.IO.Stream"/> where the image will be saved to.
        /// </param>
        /// <remarks>
        /// After the screenshot has been saved, the Position property of the
        /// <paramref name="destination"/> stream is set to 0.
        /// </remarks>
        public void Save(Stream destination)
        {
            if (this.Bitmap == null) return;

            if (Source == ScreenshotSource.File)
            {
                using (StreamReader sr = new StreamReader(OriginalFileName))
                {
                    sr.BaseStream.CopyTo(destination);
                }
            }
            else if (Program.Config.UseCompression)
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg")
                    {
                        ici = codec;
                        break;
                    }
                }

                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(Encoder.Quality,
                    Program.Config.JpegQuality);
                this.Bitmap.Save(destination, ici, ep);
            }
            else
            {
                this.Bitmap.Save(destination, ImageFormat.Png);
            }

            //Reset position of the destination stream
            destination.Position = 0;
        }

        /// <summary>
        /// Uploads the screenshot to the currently configured server.
        /// </summary>
        /// <returns>
        /// A value indicating whether the upload succeeded without errors.
        /// </returns>
        public bool Upload()
        {
            var args = new UploadingEventArgs(GetFileName());
            OnUploading(args);

            if (args.Cancel) return true;

            var target = PathUtility.UriCombine(Program.Config.FtpServerPath,
                args.FileName);
            var uploader = Uploader.Create(Program.Config);
            uploader.DuplicateFileFound += (sender, e) =>
            {
                OnDuplicateFileFound(e);
            };

            if (IsFile && Program.Config.CheckForDuplicateFiles)
            {
                var name = Path.GetFileNameWithoutExtension(OriginalFileName);
                if (!uploader.CheckDuplicates(name, ref target))
                    return false;
            }

            using (var stream = new MemoryStream())
            {
                Save(stream);

                if (uploader.Upload(stream, ref target))
                {
                    ServerPath = target;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Asynchronously uploads the screenshot to the currently configured server.
        /// </summary>
        /// <returns>
        /// A value indicating whether the upload succeeded without errors.
        /// </returns>
        public async Task<bool> UploadAsync()
        {
            return await Task.Run(() =>
            {
                return Upload();
            });
        }

        /// <summary>
        /// Returns a <see cref="Bitmap"/> containing an image of the specified screen.
        /// </summary>
        /// <param name="screen">The <see cref="Screen"/> to capture.</param>
        /// <returns>
        /// A new <see cref="Bitmap"/> object representing <paramref name="screen"/>.
        /// </returns>
        protected static Bitmap CopyFromScreen(Screen screen)
        {
            var bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(ThatFuckingColor);
                g.CopyFromScreen(screen.Bounds.Location, Point.Empty,
                    screen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }

        /// <summary>
        /// Returns the coordinates of the left-most, top-most, right-most and
        /// bottom-most edges of all screens.
        /// </summary>
        protected static Rectangle GetDesktopBounds()
        {
            var left = 0;
            var top = 0;
            var right = 0;
            var bottom = 0;

            foreach (Screen s in Screen.AllScreens)
            {
                if (s.Bounds.Left < left) left = s.Bounds.Left;
                if (s.Bounds.Top < top) top = s.Bounds.Top;
                if (s.Bounds.Right > right) right = s.Bounds.Right;
                if (s.Bounds.Bottom > bottom) bottom = s.Bounds.Bottom;
            }

            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Screenshot"/> class.
        /// </summary>
        /// <param name="disposing">True to release managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="DeleteFailed"/> event.
        /// </summary>
        protected virtual void OnDeleteFailed()
        {
            var handler = DeleteFailed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="DeleteSucceeded"/> event.
        /// </summary>
        protected virtual void OnDeleteSucceeded()
        {
            var handler = DeleteSucceeded;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="DuplicateFileFound"/> event.
        /// </summary>
        protected virtual void OnDuplicateFileFound(DuplicateFileEventArgs arg)
        {
            var handler = DuplicateFileFound;
            if (handler != null)
                handler(this, arg);
        }

        /// <summary>
        /// Raises the <see cref="UploadFailed"/> event.
        /// </summary>
        protected virtual void OnUploadFailed()
        {
            var handler = UploadFailed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="Uploading"/> event.
        /// </summary>
        protected virtual void OnUploading(UploadingEventArgs arg)
        {
            var handler = Uploading;
            if (handler != null)
                handler(this, arg);
        }

        /// <summary>
        /// Raises the <see cref="UploadSucceeded"/> event.
        /// </summary>
        protected virtual void OnUploadSucceeded()
        {
            var handler = UploadSucceeded;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
