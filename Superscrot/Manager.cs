using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Superscrot.Uploaders;

namespace Superscrot
{
    /// <summary>
    /// Acts as a temporary user interface replacement.
    /// </summary>
    public class Manager : IDisposable
    {
        private bool enabled = true;
        private History history = null;
        private KeyboardHook hook = null;

        /// <summary>
        /// The synchronization context for the main (UI) thread.
        /// </summary>
        /// <remarks>
        /// This is used to show Windows Forms dialogs on the UI thread while in
        /// the context of a background thread (e.g. while <c>await</c> ing) to
        /// preview issues that would require some components to run on an STA
        /// thread.
        /// </remarks>
        private SynchronizationContext uiContext = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        public Manager()
        {
        }

        /// <summary>
        /// Occurs when the <see cref="Enabled"/> property changes.
        /// </summary>
        public event EventHandler EnabledChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Manager"/>
        /// will respond to keyboard input or not.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (value != enabled)
                {
                    enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Provides information about taken screenshots.
        /// </summary>
        public History History
        {
            get
            {
                if (history == null)
                {
                    history = new History();
                }
                return history;
            }
        }

        /// <summary>
        /// Releases resources used by the <see cref="Superscrot.Manager"/>
        /// class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes the global keyboard hook.
        /// </summary>
        public bool InitializeKeyboardHook()
        {
            try
            {
                hook = new KeyboardHook();
                hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed);
                hook.RegisterHotKey(ModifierKeys.None, Keys.PrintScreen);      //desktop screenshot
                hook.RegisterHotKey(ModifierKeys.Alt, Keys.PrintScreen);       //active window
                hook.RegisterHotKey(ModifierKeys.Control, Keys.PrintScreen);   //region
                hook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift, Keys.PrintScreen); //undo last
                hook.RegisterHotKey(ModifierKeys.Control, Keys.PageUp); //clipboard
                return true;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Trace.WriteLine(ex);
                MessageBox.Show(SR.HotkeyAlreadyRegistered, "Superscrot",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Deletes the last uploaded file. Can be called multiple times
        /// consecutively.
        /// </summary>
        public async void UndoUpload()
        {
            try
            {
                if (History.Count == 0)
                {
                    Debug.WriteLine("Nothing to undo!");
                    return;
                }

                var screenshot = History.Pop();
                if (await screenshot.DeleteAsync())
                {
                    Debug.WriteLine("Screenshot deleted successfully");
                    System.Media.SystemSounds.Asterisk.Play();
                }
                else
                {
                    ReportDeletionError(screenshot);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        /// <summary>
        /// Uploads images and files on the clipboard to FTP.
        /// </summary>
        public async Task UploadClipboard()
        {
            if (Clipboard.ContainsImage())
            {
                var capture = Screenshot.FromClipboard();
                await UploadScreenshot(capture);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                var files = Clipboard.GetFileDropList();
                if (files.Count == 1)
                {
                    var capture = Screenshot.FromFile(files[0]);
                    await UploadScreenshot(capture);
                }
                else
                {
                    var result = await MultiUploadAsync(files);
                    if (!string.IsNullOrWhiteSpace(result))
                        SetClipboard(result.Trim());
                }
            }
        }

        /// <summary>
        /// Uploads a screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot to upload.</param>
        public async Task UploadScreenshot(Screenshot screenshot)
        {
            if (screenshot == null) throw new ArgumentNullException("screenshot");

            screenshot.Uploading += Screenshot_Uploading;
            screenshot.DuplicateFileFound += Screenshot_DuplicateFileFound;
            if (await screenshot.UploadAsync())
            {
                Program.Tray.SetStatus(TrayIcon.Status.None);

                if (screenshot.IsUploaded)
                {
                    Debug.WriteLine("Screenshot uploaded successfully to "
                        + screenshot.PublicUrl);
                    SetClipboard(screenshot);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (Program.Config.ShowBalloontip)
                    {
                        Program.Tray.ShowMessage("Screenshot uploaded sucessfully", screenshot.PublicUrl, ToolTipIcon.Info);
                    }
                    History.Push(screenshot);
                }
            }
            else
            {
                ReportUploadError(screenshot);
            }
        }

        /// <summary>
        /// Releases resources used by the <see cref="Superscrot.Manager"/>
        /// class.
        /// </summary>
        /// <param name="disposing">True to release managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free managed resources
                if (hook != null)
                {
                    hook.Dispose();
                    hook = null;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="EnabledChanged"/> event.
        /// </summary>
        protected virtual void OnEnabledChanged()
        {
            var handler = EnabledChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private static bool IsImageFile(string file)
        {
            string ext = Path.GetExtension(file);
            string[] recognizedExtensions = { ".png", ".jpg", ".jpeg", ".bmp",
                                                ".tiff", ".gif" };
            foreach (string recognizedExtension in recognizedExtensions)
            {
                if (string.Compare(ext, recognizedExtension, true) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static void SetClipboard(Screenshot screenshot)
        {
            SetClipboard(screenshot.PublicUrl);
        }

        private static void SetClipboard(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                var hWnd = NativeMethods.GetOpenClipboardWindow();
                if (hWnd != IntPtr.Zero)
                {
                    var culprit = new NativeWindow(hWnd);
                    throw new Exception(SR.ClipboardBlockedBy.With(culprit), ex);
                }
                else
                {
                    throw new Exception(SR.ClipboardBlocked);
                }
            }
        }

        private async void KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (!Enabled) return;

            try
            {
                uiContext = SynchronizationContext.Current;

                if (e.Key == Keys.PrintScreen)
                {
                    Screenshot screenshot = null;
                    switch (e.Modifier)
                    {
                        case ModifierKeys.None:
                            screenshot = Screenshot.FromDesktop();
                            break;

                        case ModifierKeys.Alt:
                            screenshot = Screenshot.FromActiveWindow();
                            break;

                        case ModifierKeys.Control:
                            screenshot = Screenshot.FromRegion();
                            break;

                        case ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift:
                            UndoUpload();
                            return;
                    }

                    if (screenshot != null)
                        await UploadScreenshot(screenshot);
                }
                else if (e.Key == Keys.PageUp && e.Modifier == ModifierKeys.Control)
                {
                    await UploadClipboard();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                System.Media.SystemSounds.Exclamation.Play();
                Program.Tray.ShowError(SR.GenericError,
                    SR.GenericErrorMessage.With(ex.Message));
            }
        }

        private async Task<string> MultiUploadAsync(IEnumerable files)
        {
            var stringBuilder = new StringBuilder();

            foreach (string file in files)
            {
                if (!IsImageFile(file)) continue;

                var screenshot = Screenshot.FromFile(file);
                if (await screenshot.UploadAsync())
                {
                    Debug.WriteLine("Screenshot successfully uploaded to "
                        + screenshot.PublicUrl);

                    if (!string.IsNullOrWhiteSpace(screenshot.PublicUrl))
                        stringBuilder.AppendLine(screenshot.PublicUrl);
                    History.Push(screenshot);
                }
                else
                {
                    ReportUploadError(screenshot);
                }
            }

            return stringBuilder.ToString();
        }

        private void ReportDeletionError(Screenshot screenshot)
        {
            try
            {
                Program.Tray.ShowError(SR.GenericDeleteFailed,
                    SR.CheckHostConnection.With(Program.Config.FtpHostname));
                System.Media.SystemSounds.Exclamation.Play();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        private void ReportUploadError(Screenshot screenshot)
        {
            try
            {
                Program.Tray.SetStatus(TrayIcon.Status.None);
                Program.Tray.ShowError(SR.GenericUploadFailed,
                    SR.CheckHostConnection.With(Program.Config.FtpHostname));
                System.Media.SystemSounds.Exclamation.Play();

                var fileName = PathUtility.RemoveInvalidFilenameChars(screenshot.GetFileName());
                var target = Path.Combine(Program.Config.FailedScreenshotsFolder, fileName);
                screenshot.Save(target);
                Trace.WriteLine("Failed screenshot saved to " + target);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        private void Screenshot_DuplicateFileFound(object sender, DuplicateFileEventArgs e)
        {
            Trace.WriteLine("Duplicate file found: " + e.DuplicateFileName);

            using (var dialog = new Dialogs.DuplicateFileFoundDialog(
                e.TargetFileName, e.DuplicateFileName))
            {
                var result = dialog.ShowDialog();
                switch (result)
                {
                    case DialogResult.Ignore:
                        e.Action = DuplicateFileAction.Ignore;
                        break;

                    case DialogResult.Yes:
                        e.Action = DuplicateFileAction.Replace;
                        break;

                    case DialogResult.Abort:
                    default:
                        e.Action = DuplicateFileAction.Abort;
                        break;
                }
            }
        }

        private void Screenshot_Uploading(object sender, UploadingEventArgs e)
        {
            if (Program.Config.ShowPreviewDialog)
            {
                var screenshot = (Screenshot)sender;

                Debug.Assert(uiContext != null,
                    "Main thread context should not be null");
                uiContext.Send(_ =>
                {
                    using (var dialog = new PreviewDialog(screenshot))
                    {
                        if (dialog.ShowDialog() != DialogResult.OK)
                            e.Cancel = true;
                        e.FileName = dialog.FileName;
                    }
                }, null);
            }

            Program.Tray.SetStatus(TrayIcon.Status.Uploading);
        }
    }
}
