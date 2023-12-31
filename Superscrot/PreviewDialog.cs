using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Superscrot
{
    /// <summary>
    /// Shows a preview of a screenshot, and allows the user to change the
    /// filename, as well as to save the screenshot to a file or copy it to the
    /// clipboard.
    /// </summary>
    public partial class PreviewDialog : Form
    {
        private Screenshot _screenshot;
        private string _tempFileName;

        /// <summary>
        /// Initializes a new instance of the preview dialog for the specified
        /// screenshot.
        /// </summary>
        /// <param name="s">The screenshot to preview.</param>
        public PreviewDialog(Screenshot s)
        {
            _screenshot = s;

            InitializeComponent();
            FileSizeLabel.Text = string.Format(new FileSizeFormatProvider(), SR.FileSizeLabel, s.CalculateSize());

            if (s.IsFile)
            {
                SaveButton.Enabled = false;
            }
        }

        /// <summary>
        /// Gets or sets the filename on the form.
        /// </summary>
        public string FileName
        {
            get { return FileNameInput.Text; }
            set { FileNameInput.Text = value; }
        }

        /// <summary>
        /// Gets the filename to a temporary local copy of the screenshot.
        /// </summary>
        private string TempFileName
        {
            get
            {
                if (_tempFileName == null)
                {
                    _tempFileName = System.IO.Path.GetTempFileName();
                    System.IO.File.Delete(_tempFileName);
                    _tempFileName += ".png";

                    _screenshot.Bitmap.Save(_tempFileName);
                }
                return _tempFileName;
            }
        }

        /// <summary>
        /// Closes the form when the cancel button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Deletes the temporary local copy (if any).
        /// </summary>
        private void Cleanup()
        {
            try
            {
                if (_tempFileName != null && System.IO.File.Exists(_tempFileName))
                {
                    System.IO.File.Delete(_tempFileName);
                }
            }
            catch { }
        }

        /// <summary>
        /// Copies the image to the clipboard when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(_screenshot.Bitmap);
        }

        /// <summary>
        /// Updates the public link when the filename is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNameInput_TextChanged(object sender, EventArgs e)
        {
            UploadButton.Enabled = !string.IsNullOrEmpty(FileName);
            if (!string.IsNullOrEmpty(FileName))
            {
                PublicUrl.Text = PathUtility.UriCombine(Program.Config.HttpBaseUri, PathUtility.UrlEncode(FileName));
            }
            else
            {
                PublicUrl.Text = string.Empty;
            }
        }

        /// <summary>
        /// Deletes the temporary local copy (if any) when the dialog is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cleanup();
        }

        /// <summary>
        /// Opens the link when it is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublicUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && PublicUrl.Text.Length > 0)
            {
                System.Diagnostics.Process.Start(PublicUrl.Text);
            }
        }

        /// <summary>
        /// Prompt the user to save the file when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                sfd.FileName = FileName.Replace("/", "-").Replace("\\", "-");
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _screenshot.Save(sfd.FileName);
                }
            }
        }

        /// <summary>
        /// Opens the image in the default image viewer when the preview is
        /// clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenshotPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process viewer = Process.Start(TempFileName);
                if (viewer != null)
                {
                    viewer.Exited += (sender2, e2) =>
                    {
                        Cleanup();
                    };
                }
            }
        }

        /// <summary>
        /// Saves the "Don't show this dialog again" setting and closes the form
        /// when the Upload button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (DontShowAgain.Checked)
            {
                Program.Config.ShowPreviewDialog = false;
                Program.Config.SaveSettings(Program.SettingsPath);
            }
            Close();
        }

        /// <summary>
        /// Displays the screenshot and filename when the form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDialog_Load(object sender, EventArgs e)
        {
            if (_screenshot != null)
            {
                ScreenshotPreview.Image = _screenshot.Bitmap;

                string defaultFileName = _screenshot.GetFileName();
                FileNameInput.Text = defaultFileName;

                // Select only the filename itself
                string fileName = System.IO.Path.GetFileNameWithoutExtension(defaultFileName);
                int iStart = defaultFileName.IndexOf(fileName);
                if (iStart < 0)
                    iStart = 0;

                FileNameInput.Select(iStart, fileName.Length);
                FileNameInput.Focus();
            }
        }
    }
}
