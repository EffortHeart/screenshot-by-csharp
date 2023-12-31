using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Superscrot
{
    /// <summary>
    /// Provides common configurable settings.
    /// </summary>
    public class Configuration
    {
        private string failedScreenshotsFolder;
        private string ftpServerPath;
        private string httpBaseUri;
        private long jpegQuality = 100L;
        private double overlayOpacity = 0.6;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class
        /// with default values.
        /// </summary>
        public Configuration()
        {
            EnableTrayIcon = true;
            ShowBalloontip = true;
            ShowPreviewDialog = true;

            FilenameFormat = "{yyyy}\\\\{MM}\\\\{unix}-{name}";
            FtpPort = 21;
            UseCompression = false;
            JpegQuality = 100L;
            FtpTimeout = 30000;

            OverlayBackgroundColor = Color.Black;
            OverlayForegroundColor = Color.White;
            OverlayOpacity = 0.6;

            CheckForDuplicateFiles = true;
            FailedScreenshotsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Superscrot", "Failed");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class
        /// with values from another instance.
        /// </summary>
        /// <param name="that">
        /// The <see cref="Configuration"/> instance whose values to copy.
        /// </param>
        public Configuration(Configuration that)
        {
            // Server
            this.FtpHostname = that.FtpHostname;
            this.FtpPort = that.FtpPort;
            this.UseSSH = that.UseSSH;
            this.FtpTimeout = that.FtpTimeout;

            // Authentication
            this.FtpUsername = that.FtpUsername;
            this.FtpPassword = that.FtpPassword;
            this.PrivateKeyPath = that.PrivateKeyPath;

            // Upload
            this.UseCompression = that.UseCompression;
            this.JpegQuality = that.JpegQuality;
            this.FilenameFormat = that.FilenameFormat;
            this.FtpServerPath = that.FtpServerPath;
            this.HttpBaseUri = that.HttpBaseUri;
            this.FailedScreenshotsFolder = that.FailedScreenshotsFolder;
            this.CheckForDuplicateFiles = that.CheckForDuplicateFiles;

            // UI
            this.ShowPreviewDialog = that.ShowPreviewDialog;
            this.OverlayBackgroundColor = that.OverlayBackgroundColor;
            this.OverlayForegroundColor = that.OverlayForegroundColor;
            this.OverlayOpacity = that.OverlayOpacity;
            this.EnableTrayIcon = that.EnableTrayIcon;
            this.ShowBalloontip = that.ShowBalloontip;
        }

        /// <summary>
        /// Gets or sets a value that determines whether or not to check for
        /// duplicate files when uploading a screenshot.
        /// </summary>
        [DisplayName("Check for duplicate files"), Category("Misc. settings")]
        [Description("Set to true to check if a file with the same name already exists on the server.")]
        public bool CheckForDuplicateFiles { get; set; }

        /// <summary>
        /// Determines whether to display a system tray icon.
        /// </summary>
        [DisplayName("Enable tray icon"), Category("User interface")]
        [Description("Determines whether to display a system tray icon.")]
        public bool EnableTrayIcon { get; set; }

        /// <summary>
        /// Determines whether to display a balloon tip when a screenshot is successfully uploaded.
        /// </summary>
        [DisplayName("Show Balloon tip"), Category("User interface")]
        [Description("Determines whether to display a balloon tip when a screenshot is successfully uploaded.")]
        public bool ShowBalloontip { get; set; }

        /// <summary>
        /// Gets or sets the path to the folder where failed screenshots are saved.
        /// </summary>
        [DisplayName("Failed screenshots folder"), Category("Misc. settings")]
        [Description("The location where screenshots that could not be uploaded are saved to.")]
        public string FailedScreenshotsFolder
        {
            get { return failedScreenshotsFolder; }
            set
            {
                if (value != failedScreenshotsFolder)
                {
                    failedScreenshotsFolder = value;
                    EnsureDirectoryExists(value);
                }
            }
        }

        /// <summary>
        /// Gets/sets the format of the filename that screenshots are saved as.
        /// </summary>
        [DisplayName("Filename format"), Category("Image settings")]
        [Description(@"The format of the filename that screenshots are saved as. See http://www.horsedrowner.net/superscrot#filenameformats. The extension is automatically changed or appended before uploading.")]
        public string FilenameFormat { get; set; }

        /// <summary>
        /// Gets/sets the hostname or IP address of the server to upload to.
        /// </summary>
        [DisplayName("Hostname"), Category("Connection")]
        [Description("The hostname or IP address of the server to upload to. Example: ftp.example.com")]
        public string FtpHostname { get; set; }

        /// <summary>
        /// Gets/sets the password of the user.
        /// </summary>
        [DisplayName("Password"), Category("Connection")]
        [Description("All your password are belong to me.")]
        [PasswordPropertyText(true)]
        public string FtpPassword { get; set; }

        /// <summary>
        /// Gets/sets the port of the server to upload to.
        /// </summary>
        [DisplayName("Port"), Category("Connection")]
        [Description("The port of the server to upload to. Usually 21 for FTP or 22 for SSH.")]
        public int FtpPort { get; set; }

        /// <summary>
        /// Gets/sets the path to the directory on the server to save
        /// screenshots to.
        /// </summary>
        [DisplayName("Server path"), Category("FTP settings")]
        [Description("The path to the directory on the server to save screenshots to. If it doesn't exist, it will be created. Example: public_html/screenshots/")]
        public string FtpServerPath
        {
            get { return ftpServerPath; }
            set
            {
                if (value != ftpServerPath)
                {
                    ftpServerPath = value;
                    if (!ftpServerPath.EndsWith("/"))
                        ftpServerPath += "/";
                }
            }
        }

        /// <summary>
        /// Gets/sets the timeout in milliseconds to wait for responses to FTP requests.
        /// </summary>
        [DisplayName("Timeout"), Category("Connection")]
        [Description("The timeout in milliseconds to wait for responses to requests.")]
        public int FtpTimeout { get; set; }

        /// <summary>
        /// Gets/sets the username of a user on the server to upload as.
        /// </summary>
        [DisplayName("Username"), Category("Connection")]
        [Description("The username of a user on the server to upload as.")]
        public string FtpUsername { get; set; }

        /// <summary>
        /// Gets/sets the base URI that leads to the FTP server path. This will
        /// be copied to the clipboard after a succesfull upload.
        /// </summary>
        [DisplayName("HTTP base URI"), Category("FTP settings")]
        [Description("The base URI that is copied to the clipboard after uploading. Example: http://www.example.com/screenshots/")]
        public string HttpBaseUri
        {
            get { return httpBaseUri; }
            set
            {
                if (value != httpBaseUri)
                {
                    httpBaseUri = value;
                    if (!httpBaseUri.EndsWith("/"))
                        httpBaseUri += "/";
                }
            }
        }

        /// <summary>
        /// Gets/sets the quality level of JPEG compressed images.
        /// </summary>
        [DisplayName("JPEG quality"), Category("Image settings")]
        [Description("The quality of JPEG compressed images. Supports values 0 - 100. 90 by default.")]
        public long JpegQuality
        {
            get { return jpegQuality; }
            set
            {
                if (value > 100) jpegQuality = 100;
                if (value < 0) jpegQuality = 0;
                else jpegQuality = value;
            }
        }

        /// <summary>
        /// Gets/sets the background color on the region selection overlay.
        /// </summary>
        [DisplayName("Background color"), Category("Overlay settings")]
        [Description("The background color on the region selection overlay.")]
        [XmlIgnore()]
        public Color OverlayBackgroundColor { get; set; }

        /// <summary>
        /// Gets/set the foreground color on the region selection overlay.
        /// </summary>
        [DisplayName("Foreground color"), Category("Overlay settings")]
        [Description("The foreground color on the region selection overlay.")]
        [XmlIgnore()]
        public Color OverlayForegroundColor { get; set; }

        /// <summary>
        /// Gets/sets the opacity of the overlay (0 - 1).
        /// </summary>
        [TypeConverterAttribute(typeof(System.Windows.Forms.OpacityConverter))]
        [DisplayName("Opacity"), Category("Overlay settings")]
        [Description("The opacity of the overlay (0 - 100). If you set a value of 0 or 100 you'll probably have problems though.")]
        public double OverlayOpacity
        {
            get { return overlayOpacity; }
            set
            {
                if (value > 1.0)
                    overlayOpacity = 1.0;
                else if (value < 0.0)
                    overlayOpacity = 0.0;
                else
                    overlayOpacity = value;
            }
        }

        /// <summary>
        /// Gets/sets the path to the private key to use.
        /// </summary>
        [DisplayName("SSH private key path"), Category("Connection")]
        [Description("The path to the private key file to use for authentication over SSH.")]
        public string PrivateKeyPath { get; set; }

        /// <summary>
        /// Determines whether to display a preview dialog when a screenshot is taken.
        /// </summary>
        [DisplayName("Show preview dialog"), Category("User interface")]
        [Description("If enabled, a preview dialog appears when a screenshot is taken, which allows you to change the filename and save the screenshot to a file.")]
        public bool ShowPreviewDialog { get; set; }

        /// <summary>
        /// Determines whether to use JPEG compression.
        /// </summary>
        [DisplayName("Use JPEG compression"), Category("Image settings")]
        [Description("Determines whether to use JPEG compression for screenshots (high quality, but still JPEG). If disabled, uses PNG.")]
        public bool UseCompression { get; set; }

        /// <summary>
        /// Gets/sets a value that determines whether to use SSH FTP.
        /// </summary>
        [DisplayName("Use SSH"), Category("Connection")]
        [Description("Determines whether to use FTP over SSH or not.")]
        public bool UseSSH { get; set; }

        /// <summary>
        /// I love XML Serialization but this is fucking gay.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("OverlayBackgroundColor")]
        public string XmlOverlayBackgroundColor
        {
            get { return XmlColor.SerializeColor(OverlayBackgroundColor); }
            set { OverlayBackgroundColor = XmlColor.DeserializeColor(value); }
        }

        /// <summary>
        /// I love XML Serialization but this is fucking gay.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("OverlayForegroundColor")]
        public string XmlOverlayForegroundColor
        {
            get { return XmlColor.SerializeColor(OverlayForegroundColor); }
            set { OverlayForegroundColor = XmlColor.DeserializeColor(value); }
        }

        /// <summary>
        /// Loads settings from the specified file.
        /// </summary>
        /// <param name="filename">
        /// The filename of an XML file that has been serialized by SaveSettings.
        /// </param>
        /// <returns>A new instance of the Configuration class.</returns>
        public static Configuration LoadSettings(string filename)
        {
            Configuration config = null;

            try
            {
                XmlSerializer x = new XmlSerializer(typeof(Configuration));
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlReader xr = XmlReader.Create(fs);
                    config = x.Deserialize(xr) as Configuration;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return config;
        }

        /// <summary>
        /// Saves settings to the specified file.
        /// </summary>
        /// <param name="filename">The filename of the XML file to save to.</param>
        public void SaveSettings(string filename)
        {
            try
            {
                EnsureDirectoryExists(Path.GetDirectoryName(filename));

                XmlSerializer x = new XmlSerializer(typeof(Configuration));
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    XmlWriter xw = XmlWriter.Create(fs);
                    x.Serialize(xw, this);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        private void EnsureDirectoryExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch { }
        }
    }
}
