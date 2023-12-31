using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Superscrot.Dialogs
{
    /// <summary>
    /// Represents a dialog that allows users to change the application's settings.
    /// </summary>
    public partial class Settings : Form
    {
        private Configuration configuration;
        private Screenshot exampleScreenshot;
        private bool isDirty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> dialog.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the <see cref="Superscrot.Configuration"/> whose
        /// properties are presented on the form.
        /// </summary>
        public Configuration Configuration
        {
            get { return configuration; }
            set
            {
                if (value != configuration)
                {
                    configuration = value;
                    UpdateForm();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether or not changes to the
        /// current configuration have been applied
        /// </summary>
        protected bool IsDirty
        {
            get { return isDirty; }
            set
            {
                isDirty = value;
                applyButton.Enabled = value;
            }
        }

        /// <summary>
        /// Updates the current configuration with input from the form.
        /// </summary>
        protected void UpdateConfigation()
        {
            // Upload Image settings
            Configuration.UseCompression = useJpeg.Checked;
            Configuration.JpegQuality = qualitySlider.Value;

            // File name
            Configuration.FilenameFormat = formatDropdown.Text;
            Configuration.CheckForDuplicateFiles = enableDuplicateFileCheck.Checked;

            // Locations
            Configuration.FtpServerPath = serverPathText.Text;
            Configuration.HttpBaseUri = baseUrlText.Text;
            Configuration.FailedScreenshotsFolder = failedText.Text;

            // Connection Server
            Configuration.FtpHostname = addressText.Text;
            Configuration.FtpPort = (int)portNud.Value;
            Configuration.UseSSH = (protocolDropdown.SelectedIndex == 1);

            // Authentication
            Configuration.FtpUsername = usernameText.Text;
            Configuration.FtpPassword = passwordText.Text;
            Configuration.PrivateKeyPath = keyText.Text;

            // Interface
            Configuration.ShowPreviewDialog = showPreview.Checked;
            Configuration.ShowBalloontip = showBalloontip.Checked;
            Configuration.OverlayBackgroundColor = backgroundColor.Color;
            Configuration.OverlayForegroundColor = selectionColor.Color;
            Configuration.OverlayOpacity = (opacitySlider.Value / 100.0f);

            IsDirty = false;
        }

        /// <summary>
        /// Updates the form with data from the current configuration.
        /// </summary>
        protected void UpdateForm()
        {
            // Upload Image settings
            useJpeg.Checked = Configuration.UseCompression;
            qualitySlider.Value = (int)Configuration.JpegQuality;

            // File name
            formatDropdown.Text = Configuration.FilenameFormat;
            enableDuplicateFileCheck.Checked = Configuration.CheckForDuplicateFiles;

            // Locations
            serverPathText.Text = Configuration.FtpServerPath;
            baseUrlText.Text = Configuration.HttpBaseUri;
            failedText.Text = Configuration.FailedScreenshotsFolder;

            // Connection Server
            addressText.Text = Configuration.FtpHostname;
            portNud.Value = Configuration.FtpPort;
            protocolDropdown.SelectedIndex = (Configuration.UseSSH ? 1 : 0);

            // Authentication
            usernameText.Text = Configuration.FtpUsername;
            passwordText.Text = Configuration.FtpPassword;
            keyText.Text = Configuration.PrivateKeyPath;

            // Interface
            showPreview.Checked = Configuration.ShowPreviewDialog;
            showBalloontip.Checked = Configuration.ShowBalloontip;
            backgroundColor.Color = Configuration.OverlayBackgroundColor;
            selectionColor.Color = Configuration.OverlayForegroundColor;
            opacitySlider.Value = (int)(Configuration.OverlayOpacity * 100);

            IsDirty = false;
        }

        private void addressText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            UpdateConfigation();
            Program.Config = Configuration;
            Program.Config.SaveSettings(Program.SettingsPath);
        }

        private void backgroundColor_ColorChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void baseUrlText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void browseFailedButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.InitialDirectory = failedText.Text;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    failedText.Text = dialog.FileName;
                    IsDirty = true;
                }
            }
        }

        private void browseKeyButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (!string.IsNullOrWhiteSpace(keyText.Text))
                {
                    dialog.InitialDirectory = System.IO.Path.GetDirectoryName(keyText.Text);
                    dialog.FileName = System.IO.Path.GetFileName(keyText.Text);
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    keyText.Text = dialog.FileName;
                    IsDirty = true;
                }
            }
        }

        private void enableDuplicateFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void failedText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void formatDropdown_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;

            if (exampleScreenshot != null)
            {
                try
                {
                    var format = formatDropdown.Text;
                    formatExample.Text = exampleScreenshot.GetFileName(format);
                    formatExample.ForeColor = SystemColors.ControlText;
                }
                catch (FormatException ex)
                {
                    formatExample.Text = ex.Message;
                    formatExample.ForeColor = Color.Red;
                }
            }
        }

        private void helpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = @"https://github.com/horsedrowner/Superscrot/wiki";
            System.Diagnostics.Process.Start(link);
        }

        private void keyText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            applyButton_Click(sender, e);
            Close();
        }

        private void opacitySlider_ValueChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void openFailedButton_Click(object sender, EventArgs e)
        {
            var path = failedText.Text;
            if (System.IO.Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void portNud_ValueChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void protocolDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;

            // Enable/disable inputs depending on their relevance
            var useSsh = (protocolDropdown.SelectedIndex == 1);
            keyLabel.Enabled = keyText.Enabled = browseKeyButton.Enabled = useSsh;
        }

        private void selectionColor_ColorChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void serverPathText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void Settings_Shown(object sender, EventArgs e)
        {
            exampleScreenshot = Screenshot.FromActiveWindow();

            var format = formatDropdown.Text;
            formatExample.Text = exampleScreenshot.GetFileName(format);
        }

        private void showPreview_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void useJpeg_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;

            qualitySlider.Enabled = jpegQualityLabel.Enabled = useJpeg.Checked;
        }

        private void usernameText_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void showBalloontip_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }
    }
}
