namespace Superscrot
{
    partial class PreviewDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.DontShowAgain = new System.Windows.Forms.CheckBox();
            this.CancelButtonASDF = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.MainInstruction = new System.Windows.Forms.Label();
            this.ScreenshotPreview = new System.Windows.Forms.PictureBox();
            this.FileNameInput = new Superscrot.Controls.TextBox();
            this.PublicUrlLabel = new System.Windows.Forms.Label();
            this.PublicUrl = new System.Windows.Forms.LinkLabel();
            this.FileSizeLabel = new System.Windows.Forms.Label();
            this.FooterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // FooterPanel
            // 
            this.FooterPanel.BackColor = System.Drawing.SystemColors.Control;
            this.FooterPanel.Controls.Add(this.DontShowAgain);
            this.FooterPanel.Controls.Add(this.CancelButtonASDF);
            this.FooterPanel.Controls.Add(this.CopyButton);
            this.FooterPanel.Controls.Add(this.SaveButton);
            this.FooterPanel.Controls.Add(this.UploadButton);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FooterPanel.Location = new System.Drawing.Point(0, 145);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(592, 67);
            this.FooterPanel.TabIndex = 4;
            // 
            // DontShowAgain
            // 
            this.DontShowAgain.AutoSize = true;
            this.DontShowAgain.Location = new System.Drawing.Point(16, 6);
            this.DontShowAgain.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.DontShowAgain.Name = "DontShowAgain";
            this.DontShowAgain.Size = new System.Drawing.Size(176, 19);
            this.DontShowAgain.TabIndex = 0;
            this.DontShowAgain.Text = "Don\'t show this dialog again";
            this.DontShowAgain.UseVisualStyleBackColor = true;
            // 
            // CancelButtonASDF
            // 
            this.CancelButtonASDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButtonASDF.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtonASDF.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CancelButtonASDF.Location = new System.Drawing.Point(493, 28);
            this.CancelButtonASDF.Name = "CancelButtonASDF";
            this.CancelButtonASDF.Size = new System.Drawing.Size(87, 27);
            this.CancelButtonASDF.TabIndex = 4;
            this.CancelButtonASDF.Text = "Cancel";
            this.CancelButtonASDF.UseVisualStyleBackColor = true;
            this.CancelButtonASDF.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CopyButton.Location = new System.Drawing.Point(109, 28);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(87, 27);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "&Copy image";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveButton.Location = new System.Drawing.Point(16, 28);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(87, 27);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "&Save...";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UploadButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UploadButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UploadButton.Location = new System.Drawing.Point(400, 28);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(87, 27);
            this.UploadButton.TabIndex = 3;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // MainInstruction
            // 
            this.MainInstruction.AutoSize = true;
            this.MainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(99)))));
            this.MainInstruction.Location = new System.Drawing.Point(12, 9);
            this.MainInstruction.Name = "MainInstruction";
            this.MainInstruction.Size = new System.Drawing.Size(139, 21);
            this.MainInstruction.TabIndex = 0;
            this.MainInstruction.Text = "Upload screenshot";
            // 
            // ScreenshotPreview
            // 
            this.ScreenshotPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ScreenshotPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScreenshotPreview.Location = new System.Drawing.Point(16, 33);
            this.ScreenshotPreview.MaximumSize = new System.Drawing.Size(150, 127);
            this.ScreenshotPreview.Name = "ScreenshotPreview";
            this.ScreenshotPreview.Size = new System.Drawing.Size(150, 106);
            this.ScreenshotPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ScreenshotPreview.TabIndex = 2;
            this.ScreenshotPreview.TabStop = false;
            this.ScreenshotPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenshotPreview_MouseClick);
            // 
            // FileNameInput
            // 
            this.FileNameInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileNameInput.Location = new System.Drawing.Point(172, 33);
            this.FileNameInput.Name = "FileNameInput";
            this.FileNameInput.Placeholder = "Enter a file name";
            this.FileNameInput.Size = new System.Drawing.Size(408, 23);
            this.FileNameInput.TabIndex = 1;
            this.FileNameInput.TextChanged += new System.EventHandler(this.FileNameInput_TextChanged);
            // 
            // PublicUrlLabel
            // 
            this.PublicUrlLabel.AutoSize = true;
            this.PublicUrlLabel.Location = new System.Drawing.Point(169, 106);
            this.PublicUrlLabel.Margin = new System.Windows.Forms.Padding(3);
            this.PublicUrlLabel.Name = "PublicUrlLabel";
            this.PublicUrlLabel.Size = new System.Drawing.Size(194, 15);
            this.PublicUrlLabel.TabIndex = 2;
            this.PublicUrlLabel.Text = "Your screenshot will be available at:";
            // 
            // PublicUrl
            // 
            this.PublicUrl.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.PublicUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PublicUrl.AutoEllipsis = true;
            this.PublicUrl.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.PublicUrl.Location = new System.Drawing.Point(172, 124);
            this.PublicUrl.Name = "PublicUrl";
            this.PublicUrl.Size = new System.Drawing.Size(408, 15);
            this.PublicUrl.TabIndex = 3;
            this.PublicUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PublicUrl_LinkClicked);
            // 
            // FileSizeLabel
            // 
            this.FileSizeLabel.AutoSize = true;
            this.FileSizeLabel.Location = new System.Drawing.Point(169, 88);
            this.FileSizeLabel.Name = "FileSizeLabel";
            this.FileSizeLabel.Size = new System.Drawing.Size(30, 15);
            this.FileSizeLabel.TabIndex = 5;
            this.FileSizeLabel.Text = "Size:";
            // 
            // PreviewDialog
            // 
            this.AcceptButton = this.UploadButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.CancelButtonASDF;
            this.ClientSize = new System.Drawing.Size(592, 212);
            this.Controls.Add(this.FileSizeLabel);
            this.Controls.Add(this.PublicUrl);
            this.Controls.Add(this.PublicUrlLabel);
            this.Controls.Add(this.FileNameInput);
            this.Controls.Add(this.ScreenshotPreview);
            this.Controls.Add(this.MainInstruction);
            this.Controls.Add(this.FooterPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Superscrot";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewDialog_FormClosed);
            this.Load += new System.EventHandler(this.UploadDialog_Load);
            this.FooterPanel.ResumeLayout(false);
            this.FooterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button CancelButtonASDF;
        private System.Windows.Forms.Label MainInstruction;
        private System.Windows.Forms.PictureBox ScreenshotPreview;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label PublicUrlLabel;
        private System.Windows.Forms.LinkLabel PublicUrl;
        private System.Windows.Forms.CheckBox DontShowAgain;
        private Superscrot.Controls.TextBox FileNameInput;
        private System.Windows.Forms.Label FileSizeLabel;
    }
}