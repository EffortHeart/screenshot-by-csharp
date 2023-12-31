namespace Superscrot
{
    partial class RegionOverlay
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
            this.SuspendLayout();
            // 
            // RegionOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(620, 506);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegionOverlay";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "superscrot Region Capture";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RegionOverlay_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RegionOverlay_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RegionOverlay_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RegionOverlay_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RegionOverlay_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}