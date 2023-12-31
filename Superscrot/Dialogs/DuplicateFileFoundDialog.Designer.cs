namespace Superscrot.Dialogs
{
    partial class DuplicateFileFoundDialog
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
            this.MainInstruction = new System.Windows.Forms.Label();
            this.AbortButton = new Superscrot.Controls.CommandLink();
            this.ReplaceButton = new Superscrot.Controls.CommandLink();
            this.IgnoreButton = new Superscrot.Controls.CommandLink();
            this.DetailsPanel = new System.Windows.Forms.Panel();
            this.PreventDialogCheckbox = new System.Windows.Forms.CheckBox();
            this.DetailsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainInstruction
            // 
            this.MainInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(99)))));
            this.MainInstruction.Location = new System.Drawing.Point(12, 9);
            this.MainInstruction.Name = "MainInstruction";
            this.MainInstruction.Size = new System.Drawing.Size(570, 42);
            this.MainInstruction.TabIndex = 1;
            this.MainInstruction.Text = "The server already contains a file with the name \"{0}\"";
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.AbortButton.Location = new System.Drawing.Point(16, 167);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(566, 41);
            this.AbortButton.TabIndex = 4;
            this.AbortButton.Text = "Do&n\'t upload";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ReplaceButton.Location = new System.Drawing.Point(16, 120);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(566, 41);
            this.ReplaceButton.TabIndex = 3;
            this.ReplaceButton.Text = "Upload and &replace";
            this.ReplaceButton.UseVisualStyleBackColor = true;
            // 
            // IgnoreButton
            // 
            this.IgnoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IgnoreButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.IgnoreButton.Location = new System.Drawing.Point(16, 73);
            this.IgnoreButton.Name = "IgnoreButton";
            this.IgnoreButton.Size = new System.Drawing.Size(566, 41);
            this.IgnoreButton.TabIndex = 2;
            this.IgnoreButton.Text = "&Upload and keep both files";
            this.IgnoreButton.UseVisualStyleBackColor = true;
            // 
            // DetailsPanel
            // 
            this.DetailsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.DetailsPanel.Controls.Add(this.PreventDialogCheckbox);
            this.DetailsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DetailsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DetailsPanel.Location = new System.Drawing.Point(0, 230);
            this.DetailsPanel.Name = "DetailsPanel";
            this.DetailsPanel.Size = new System.Drawing.Size(594, 45);
            this.DetailsPanel.TabIndex = 5;
            // 
            // PreventDialogCheckbox
            // 
            this.PreventDialogCheckbox.AutoSize = true;
            this.PreventDialogCheckbox.Location = new System.Drawing.Point(16, 14);
            this.PreventDialogCheckbox.Name = "PreventDialogCheckbox";
            this.PreventDialogCheckbox.Size = new System.Drawing.Size(189, 19);
            this.PreventDialogCheckbox.TabIndex = 0;
            this.PreventDialogCheckbox.Text = "&Don\'t show this message again";
            this.PreventDialogCheckbox.UseVisualStyleBackColor = true;
            // 
            // DuplicateFileFoundDialog
            // 
            this.AcceptButton = this.IgnoreButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(594, 275);
            this.Controls.Add(this.DetailsPanel);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.IgnoreButton);
            this.Controls.Add(this.MainInstruction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DuplicateFileFoundDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Superscrot";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DuplicateFileFoundDialog_FormClosed);
            this.DetailsPanel.ResumeLayout(false);
            this.DetailsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MainInstruction;
        private Superscrot.Controls.CommandLink IgnoreButton;
        private Superscrot.Controls.CommandLink ReplaceButton;
        private Superscrot.Controls.CommandLink AbortButton;
        private System.Windows.Forms.Panel DetailsPanel;
        private System.Windows.Forms.CheckBox PreventDialogCheckbox;
    }
}