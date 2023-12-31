namespace Superscrot.Controls
{
    partial class Slider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftLabel = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.rightLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftLabel.Location = new System.Drawing.Point(0, 0);
            this.leftLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(0, 13);
            this.leftLabel.TabIndex = 0;
            this.leftLabel.Resize += new System.EventHandler(this.leftLabel_Resize);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(6, 3);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(199, 45);
            this.trackBar.TabIndex = 1;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // rightLabel
            // 
            this.rightLabel.AutoSize = true;
            this.rightLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightLabel.Location = new System.Drawing.Point(208, 0);
            this.rightLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.rightLabel.Name = "rightLabel";
            this.rightLabel.Size = new System.Drawing.Size(0, 13);
            this.rightLabel.TabIndex = 2;
            this.rightLabel.Resize += new System.EventHandler(this.rightLabel_Resize);
            // 
            // valueLabel
            // 
            this.valueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueLabel.Location = new System.Drawing.Point(3, 34);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(205, 15);
            this.valueLabel.TabIndex = 3;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Slider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.rightLabel);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.leftLabel);
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(208, 49);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label rightLabel;
        private System.Windows.Forms.Label valueLabel;
    }
}
