namespace Superscrot.Dialogs
{
    partial class Settings
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.uploadTab = new System.Windows.Forms.TabPage();
            this.filenameGroup = new System.Windows.Forms.GroupBox();
            this.enableDuplicateFileCheck = new System.Windows.Forms.CheckBox();
            this.formatExample = new System.Windows.Forms.Label();
            this.formatDropdown = new System.Windows.Forms.ComboBox();
            this.formatLabel = new System.Windows.Forms.Label();
            this.pathGroup = new System.Windows.Forms.GroupBox();
            this.openFailedButton = new System.Windows.Forms.Button();
            this.browseFailedButton = new System.Windows.Forms.Button();
            this.failedText = new System.Windows.Forms.TextBox();
            this.baseUrlText = new System.Windows.Forms.TextBox();
            this.serverPathText = new System.Windows.Forms.TextBox();
            this.failedLabel = new System.Windows.Forms.Label();
            this.baseUrlLabel = new System.Windows.Forms.Label();
            this.serverPathLabel = new System.Windows.Forms.Label();
            this.imageGroup = new System.Windows.Forms.GroupBox();
            this.qualitySlider = new Superscrot.Controls.Slider();
            this.jpegQualityLabel = new System.Windows.Forms.Label();
            this.useJpeg = new System.Windows.Forms.CheckBox();
            this.connectionTab = new System.Windows.Forms.TabPage();
            this.authGroup = new System.Windows.Forms.GroupBox();
            this.browseKeyButton = new System.Windows.Forms.Button();
            this.keyText = new System.Windows.Forms.TextBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.serverGroup = new System.Windows.Forms.GroupBox();
            this.protocolDropdown = new System.Windows.Forms.ComboBox();
            this.protocolLabel = new System.Windows.Forms.Label();
            this.portNud = new System.Windows.Forms.NumericUpDown();
            this.portLabel = new System.Windows.Forms.Label();
            this.addressText = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.interfaceTab = new System.Windows.Forms.TabPage();
            this.overlayGroup = new System.Windows.Forms.GroupBox();
            this.opacitySlider = new Superscrot.Controls.Slider();
            this.opacityLabel = new System.Windows.Forms.Label();
            this.selectionColor = new Superscrot.Controls.ColorPickerButton();
            this.selectionLabel = new System.Windows.Forms.Label();
            this.backgroundColor = new Superscrot.Controls.ColorPickerButton();
            this.backgroundLabel = new System.Windows.Forms.Label();
            this.showPreview = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.helpLink = new System.Windows.Forms.LinkLabel();
            this.showBalloontip = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.uploadTab.SuspendLayout();
            this.filenameGroup.SuspendLayout();
            this.pathGroup.SuspendLayout();
            this.imageGroup.SuspendLayout();
            this.connectionTab.SuspendLayout();
            this.authGroup.SuspendLayout();
            this.serverGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNud)).BeginInit();
            this.interfaceTab.SuspendLayout();
            this.overlayGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.uploadTab);
            this.tabControl.Controls.Add(this.connectionTab);
            this.tabControl.Controls.Add(this.interfaceTab);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(441, 507);
            this.tabControl.TabIndex = 0;
            // 
            // uploadTab
            // 
            this.uploadTab.Controls.Add(this.filenameGroup);
            this.uploadTab.Controls.Add(this.pathGroup);
            this.uploadTab.Controls.Add(this.imageGroup);
            this.uploadTab.Location = new System.Drawing.Point(4, 24);
            this.uploadTab.Name = "uploadTab";
            this.uploadTab.Padding = new System.Windows.Forms.Padding(3);
            this.uploadTab.Size = new System.Drawing.Size(433, 479);
            this.uploadTab.TabIndex = 2;
            this.uploadTab.Text = "Upload";
            this.uploadTab.UseVisualStyleBackColor = true;
            // 
            // filenameGroup
            // 
            this.filenameGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameGroup.Controls.Add(this.enableDuplicateFileCheck);
            this.filenameGroup.Controls.Add(this.formatExample);
            this.filenameGroup.Controls.Add(this.formatDropdown);
            this.filenameGroup.Controls.Add(this.formatLabel);
            this.filenameGroup.Location = new System.Drawing.Point(6, 143);
            this.filenameGroup.Name = "filenameGroup";
            this.filenameGroup.Size = new System.Drawing.Size(421, 116);
            this.filenameGroup.TabIndex = 2;
            this.filenameGroup.TabStop = false;
            this.filenameGroup.Text = "File name";
            // 
            // enableDuplicateFileCheck
            // 
            this.enableDuplicateFileCheck.AutoSize = true;
            this.enableDuplicateFileCheck.Location = new System.Drawing.Point(9, 82);
            this.enableDuplicateFileCheck.Name = "enableDuplicateFileCheck";
            this.enableDuplicateFileCheck.Size = new System.Drawing.Size(293, 19);
            this.enableDuplicateFileCheck.TabIndex = 8;
            this.enableDuplicateFileCheck.Text = "&Check for possible duplicate files before uploading";
            this.enableDuplicateFileCheck.UseVisualStyleBackColor = true;
            this.enableDuplicateFileCheck.CheckedChanged += new System.EventHandler(this.enableDuplicateFileCheck_CheckedChanged);
            // 
            // formatExample
            // 
            this.formatExample.AutoSize = true;
            this.formatExample.Location = new System.Drawing.Point(62, 51);
            this.formatExample.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.formatExample.Name = "formatExample";
            this.formatExample.Size = new System.Drawing.Size(0, 15);
            this.formatExample.TabIndex = 7;
            // 
            // formatDropdown
            // 
            this.formatDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formatDropdown.FormattingEnabled = true;
            this.formatDropdown.Items.AddRange(new object[] {
            "{yyyy}/{MM}/{Unix}-{Name}",
            "{Machine}/{Source}/{Time}-{Name}",
            "{Machine}/{Source}/{Unix}-{Name}",
            "{Unix}-{Name}",
            "{Unix}",
            "{Guid}"});
            this.formatDropdown.Location = new System.Drawing.Point(65, 22);
            this.formatDropdown.Name = "formatDropdown";
            this.formatDropdown.Size = new System.Drawing.Size(275, 23);
            this.formatDropdown.TabIndex = 6;
            this.formatDropdown.TextChanged += new System.EventHandler(this.formatDropdown_TextChanged);
            // 
            // formatLabel
            // 
            this.formatLabel.AutoSize = true;
            this.formatLabel.Location = new System.Drawing.Point(6, 25);
            this.formatLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.Size = new System.Drawing.Size(48, 15);
            this.formatLabel.TabIndex = 0;
            this.formatLabel.Text = "For&mat:";
            // 
            // pathGroup
            // 
            this.pathGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathGroup.Controls.Add(this.openFailedButton);
            this.pathGroup.Controls.Add(this.browseFailedButton);
            this.pathGroup.Controls.Add(this.failedText);
            this.pathGroup.Controls.Add(this.baseUrlText);
            this.pathGroup.Controls.Add(this.serverPathText);
            this.pathGroup.Controls.Add(this.failedLabel);
            this.pathGroup.Controls.Add(this.baseUrlLabel);
            this.pathGroup.Controls.Add(this.serverPathLabel);
            this.pathGroup.Location = new System.Drawing.Point(6, 265);
            this.pathGroup.Name = "pathGroup";
            this.pathGroup.Size = new System.Drawing.Size(421, 199);
            this.pathGroup.TabIndex = 1;
            this.pathGroup.TabStop = false;
            this.pathGroup.Text = "Locations";
            // 
            // openFailedButton
            // 
            this.openFailedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFailedButton.Location = new System.Drawing.Point(340, 166);
            this.openFailedButton.Name = "openFailedButton";
            this.openFailedButton.Size = new System.Drawing.Size(75, 23);
            this.openFailedButton.TabIndex = 3;
            this.openFailedButton.Text = "&Show";
            this.openFailedButton.UseVisualStyleBackColor = true;
            this.openFailedButton.Click += new System.EventHandler(this.openFailedButton_Click);
            // 
            // browseFailedButton
            // 
            this.browseFailedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseFailedButton.Location = new System.Drawing.Point(340, 137);
            this.browseFailedButton.Name = "browseFailedButton";
            this.browseFailedButton.Size = new System.Drawing.Size(75, 23);
            this.browseFailedButton.TabIndex = 2;
            this.browseFailedButton.Text = "&Browse...";
            this.browseFailedButton.UseVisualStyleBackColor = true;
            this.browseFailedButton.Click += new System.EventHandler(this.browseFailedButton_Click);
            // 
            // failedText
            // 
            this.failedText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.failedText.Location = new System.Drawing.Point(6, 137);
            this.failedText.Name = "failedText";
            this.failedText.Size = new System.Drawing.Size(328, 23);
            this.failedText.TabIndex = 1;
            this.failedText.TextChanged += new System.EventHandler(this.failedText_TextChanged);
            // 
            // baseUrlText
            // 
            this.baseUrlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baseUrlText.Location = new System.Drawing.Point(6, 90);
            this.baseUrlText.Name = "baseUrlText";
            this.baseUrlText.Size = new System.Drawing.Size(328, 23);
            this.baseUrlText.TabIndex = 1;
            this.baseUrlText.TextChanged += new System.EventHandler(this.baseUrlText_TextChanged);
            // 
            // serverPathText
            // 
            this.serverPathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPathText.Location = new System.Drawing.Point(6, 43);
            this.serverPathText.Name = "serverPathText";
            this.serverPathText.Size = new System.Drawing.Size(328, 23);
            this.serverPathText.TabIndex = 1;
            this.serverPathText.TextChanged += new System.EventHandler(this.serverPathText_TextChanged);
            // 
            // failedLabel
            // 
            this.failedLabel.AutoSize = true;
            this.failedLabel.Location = new System.Drawing.Point(6, 119);
            this.failedLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.failedLabel.Name = "failedLabel";
            this.failedLabel.Size = new System.Drawing.Size(106, 15);
            this.failedLabel.TabIndex = 0;
            this.failedLabel.Text = "&Failed screenshots:";
            // 
            // baseUrlLabel
            // 
            this.baseUrlLabel.AutoSize = true;
            this.baseUrlLabel.Location = new System.Drawing.Point(6, 72);
            this.baseUrlLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.baseUrlLabel.Name = "baseUrlLabel";
            this.baseUrlLabel.Size = new System.Drawing.Size(58, 15);
            this.baseUrlLabel.TabIndex = 0;
            this.baseUrlLabel.Text = "Base &URL:";
            // 
            // serverPathLabel
            // 
            this.serverPathLabel.AutoSize = true;
            this.serverPathLabel.Location = new System.Drawing.Point(6, 25);
            this.serverPathLabel.Name = "serverPathLabel";
            this.serverPathLabel.Size = new System.Drawing.Size(141, 15);
            this.serverPathLabel.TabIndex = 0;
            this.serverPathLabel.Text = "&Server screenshots folder:";
            // 
            // imageGroup
            // 
            this.imageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageGroup.Controls.Add(this.qualitySlider);
            this.imageGroup.Controls.Add(this.jpegQualityLabel);
            this.imageGroup.Controls.Add(this.useJpeg);
            this.imageGroup.Location = new System.Drawing.Point(6, 6);
            this.imageGroup.Name = "imageGroup";
            this.imageGroup.Size = new System.Drawing.Size(421, 131);
            this.imageGroup.TabIndex = 0;
            this.imageGroup.TabStop = false;
            this.imageGroup.Text = "Image settings";
            // 
            // qualitySlider
            // 
            this.qualitySlider.BackColor = System.Drawing.SystemColors.Window;
            this.qualitySlider.Enabled = false;
            this.qualitySlider.Format = null;
            this.qualitySlider.Location = new System.Drawing.Point(22, 68);
            this.qualitySlider.MaximumText = "High";
            this.qualitySlider.MinimumText = "Low";
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(240, 57);
            this.qualitySlider.TabIndex = 2;
            // 
            // jpegQualityLabel
            // 
            this.jpegQualityLabel.AutoSize = true;
            this.jpegQualityLabel.Enabled = false;
            this.jpegQualityLabel.Location = new System.Drawing.Point(22, 50);
            this.jpegQualityLabel.Name = "jpegQualityLabel";
            this.jpegQualityLabel.Size = new System.Drawing.Size(48, 15);
            this.jpegQualityLabel.TabIndex = 1;
            this.jpegQualityLabel.Text = "&Quality:";
            // 
            // useJpeg
            // 
            this.useJpeg.AutoSize = true;
            this.useJpeg.Location = new System.Drawing.Point(6, 25);
            this.useJpeg.Name = "useJpeg";
            this.useJpeg.Size = new System.Drawing.Size(180, 19);
            this.useJpeg.TabIndex = 0;
            this.useJpeg.Text = "Compress images using &JPEG";
            this.useJpeg.UseVisualStyleBackColor = true;
            this.useJpeg.CheckedChanged += new System.EventHandler(this.useJpeg_CheckedChanged);
            // 
            // connectionTab
            // 
            this.connectionTab.Controls.Add(this.authGroup);
            this.connectionTab.Controls.Add(this.serverGroup);
            this.connectionTab.Location = new System.Drawing.Point(4, 24);
            this.connectionTab.Name = "connectionTab";
            this.connectionTab.Padding = new System.Windows.Forms.Padding(3);
            this.connectionTab.Size = new System.Drawing.Size(433, 479);
            this.connectionTab.TabIndex = 1;
            this.connectionTab.Text = "Connection";
            this.connectionTab.UseVisualStyleBackColor = true;
            // 
            // authGroup
            // 
            this.authGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authGroup.Controls.Add(this.browseKeyButton);
            this.authGroup.Controls.Add(this.keyText);
            this.authGroup.Controls.Add(this.keyLabel);
            this.authGroup.Controls.Add(this.passwordText);
            this.authGroup.Controls.Add(this.passwordLabel);
            this.authGroup.Controls.Add(this.usernameText);
            this.authGroup.Controls.Add(this.usernameLabel);
            this.authGroup.Location = new System.Drawing.Point(6, 131);
            this.authGroup.Name = "authGroup";
            this.authGroup.Size = new System.Drawing.Size(421, 143);
            this.authGroup.TabIndex = 1;
            this.authGroup.TabStop = false;
            this.authGroup.Text = "Authentication";
            // 
            // browseKeyButton
            // 
            this.browseKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseKeyButton.Location = new System.Drawing.Point(340, 110);
            this.browseKeyButton.Name = "browseKeyButton";
            this.browseKeyButton.Size = new System.Drawing.Size(75, 23);
            this.browseKeyButton.TabIndex = 6;
            this.browseKeyButton.Text = "&Browse...";
            this.browseKeyButton.UseVisualStyleBackColor = true;
            this.browseKeyButton.Click += new System.EventHandler(this.browseKeyButton_Click);
            // 
            // keyText
            // 
            this.keyText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyText.Location = new System.Drawing.Point(9, 110);
            this.keyText.Name = "keyText";
            this.keyText.Size = new System.Drawing.Size(325, 23);
            this.keyText.TabIndex = 5;
            this.keyText.TextChanged += new System.EventHandler(this.keyText_TextChanged);
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(6, 92);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(86, 15);
            this.keyLabel.TabIndex = 4;
            this.keyLabel.Text = "Private &key file:";
            // 
            // passwordText
            // 
            this.passwordText.Location = new System.Drawing.Point(75, 51);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(203, 23);
            this.passwordText.TabIndex = 3;
            this.passwordText.UseSystemPasswordChar = true;
            this.passwordText.TextChanged += new System.EventHandler(this.passwordText_TextChanged);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(6, 54);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(60, 15);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "&Password:";
            // 
            // usernameText
            // 
            this.usernameText.Location = new System.Drawing.Point(75, 22);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(203, 23);
            this.usernameText.TabIndex = 1;
            this.usernameText.TextChanged += new System.EventHandler(this.usernameText_TextChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(6, 25);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(63, 15);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "&Username:";
            // 
            // serverGroup
            // 
            this.serverGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverGroup.Controls.Add(this.protocolDropdown);
            this.serverGroup.Controls.Add(this.protocolLabel);
            this.serverGroup.Controls.Add(this.portNud);
            this.serverGroup.Controls.Add(this.portLabel);
            this.serverGroup.Controls.Add(this.addressText);
            this.serverGroup.Controls.Add(this.addressLabel);
            this.serverGroup.Location = new System.Drawing.Point(6, 6);
            this.serverGroup.Name = "serverGroup";
            this.serverGroup.Size = new System.Drawing.Size(421, 119);
            this.serverGroup.TabIndex = 0;
            this.serverGroup.TabStop = false;
            this.serverGroup.Text = "Server";
            // 
            // protocolDropdown
            // 
            this.protocolDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolDropdown.FormattingEnabled = true;
            this.protocolDropdown.Items.AddRange(new object[] {
            "FTP",
            "SFTP"});
            this.protocolDropdown.Location = new System.Drawing.Point(75, 80);
            this.protocolDropdown.Name = "protocolDropdown";
            this.protocolDropdown.Size = new System.Drawing.Size(121, 23);
            this.protocolDropdown.TabIndex = 5;
            this.protocolDropdown.SelectedIndexChanged += new System.EventHandler(this.protocolDropdown_SelectedIndexChanged);
            // 
            // protocolLabel
            // 
            this.protocolLabel.AutoSize = true;
            this.protocolLabel.Location = new System.Drawing.Point(6, 83);
            this.protocolLabel.Name = "protocolLabel";
            this.protocolLabel.Size = new System.Drawing.Size(55, 15);
            this.protocolLabel.TabIndex = 4;
            this.protocolLabel.Text = "Pr&otocol:";
            // 
            // portNud
            // 
            this.portNud.Location = new System.Drawing.Point(75, 51);
            this.portNud.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNud.Name = "portNud";
            this.portNud.Size = new System.Drawing.Size(120, 23);
            this.portNud.TabIndex = 3;
            this.portNud.ValueChanged += new System.EventHandler(this.portNud_ValueChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(6, 53);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(32, 15);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Po&rt:";
            // 
            // addressText
            // 
            this.addressText.Location = new System.Drawing.Point(75, 22);
            this.addressText.Name = "addressText";
            this.addressText.Size = new System.Drawing.Size(203, 23);
            this.addressText.TabIndex = 1;
            this.addressText.TextChanged += new System.EventHandler(this.addressText_TextChanged);
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(6, 25);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(52, 15);
            this.addressLabel.TabIndex = 0;
            this.addressLabel.Text = "&Address:";
            // 
            // interfaceTab
            // 
            this.interfaceTab.Controls.Add(this.showBalloontip);
            this.interfaceTab.Controls.Add(this.overlayGroup);
            this.interfaceTab.Controls.Add(this.showPreview);
            this.interfaceTab.Location = new System.Drawing.Point(4, 24);
            this.interfaceTab.Name = "interfaceTab";
            this.interfaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.interfaceTab.Size = new System.Drawing.Size(433, 479);
            this.interfaceTab.TabIndex = 3;
            this.interfaceTab.Text = "Interface";
            this.interfaceTab.UseVisualStyleBackColor = true;
            // 
            // overlayGroup
            // 
            this.overlayGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overlayGroup.Controls.Add(this.opacitySlider);
            this.overlayGroup.Controls.Add(this.opacityLabel);
            this.overlayGroup.Controls.Add(this.selectionColor);
            this.overlayGroup.Controls.Add(this.selectionLabel);
            this.overlayGroup.Controls.Add(this.backgroundColor);
            this.overlayGroup.Controls.Add(this.backgroundLabel);
            this.overlayGroup.Location = new System.Drawing.Point(6, 56);
            this.overlayGroup.Name = "overlayGroup";
            this.overlayGroup.Size = new System.Drawing.Size(421, 172);
            this.overlayGroup.TabIndex = 1;
            this.overlayGroup.TabStop = false;
            this.overlayGroup.Text = "Overlay";
            // 
            // opacitySlider
            // 
            this.opacitySlider.BackColor = System.Drawing.SystemColors.Window;
            this.opacitySlider.Format = "0\\%";
            this.opacitySlider.Location = new System.Drawing.Point(6, 102);
            this.opacitySlider.MaximumText = "Opaque";
            this.opacitySlider.MinimumText = "Transparent";
            this.opacitySlider.Name = "opacitySlider";
            this.opacitySlider.Size = new System.Drawing.Size(259, 53);
            this.opacitySlider.TabIndex = 5;
            this.opacitySlider.ValueChanged += new System.EventHandler(this.opacitySlider_ValueChanged);
            // 
            // opacityLabel
            // 
            this.opacityLabel.AutoSize = true;
            this.opacityLabel.Location = new System.Drawing.Point(6, 84);
            this.opacityLabel.Name = "opacityLabel";
            this.opacityLabel.Size = new System.Drawing.Size(51, 15);
            this.opacityLabel.TabIndex = 4;
            this.opacityLabel.Text = "Opacity:";
            // 
            // selectionColor
            // 
            this.selectionColor.Location = new System.Drawing.Point(116, 51);
            this.selectionColor.Name = "selectionColor";
            this.selectionColor.Size = new System.Drawing.Size(55, 23);
            this.selectionColor.TabIndex = 3;
            this.selectionColor.UseVisualStyleBackColor = true;
            this.selectionColor.ColorChanged += new System.EventHandler(this.selectionColor_ColorChanged);
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Location = new System.Drawing.Point(6, 55);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(88, 15);
            this.selectionLabel.TabIndex = 2;
            this.selectionLabel.Text = "&Selection color:";
            // 
            // backgroundColor
            // 
            this.backgroundColor.Location = new System.Drawing.Point(116, 22);
            this.backgroundColor.Name = "backgroundColor";
            this.backgroundColor.Size = new System.Drawing.Size(55, 23);
            this.backgroundColor.TabIndex = 1;
            this.backgroundColor.UseVisualStyleBackColor = true;
            this.backgroundColor.ColorChanged += new System.EventHandler(this.backgroundColor_ColorChanged);
            // 
            // backgroundLabel
            // 
            this.backgroundLabel.AutoSize = true;
            this.backgroundLabel.Location = new System.Drawing.Point(6, 26);
            this.backgroundLabel.Name = "backgroundLabel";
            this.backgroundLabel.Size = new System.Drawing.Size(104, 15);
            this.backgroundLabel.TabIndex = 0;
            this.backgroundLabel.Text = "&Background color:";
            // 
            // showPreview
            // 
            this.showPreview.AutoSize = true;
            this.showPreview.Location = new System.Drawing.Point(6, 6);
            this.showPreview.Name = "showPreview";
            this.showPreview.Size = new System.Drawing.Size(267, 19);
            this.showPreview.TabIndex = 0;
            this.showPreview.Text = "Show a &preview before uploading screenshots";
            this.showPreview.UseVisualStyleBackColor = true;
            this.showPreview.CheckedChanged += new System.EventHandler(this.showPreview_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(212, 525);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(293, 525);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(374, 525);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "&Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // helpLink
            // 
            this.helpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.helpLink.AutoSize = true;
            this.helpLink.Location = new System.Drawing.Point(12, 529);
            this.helpLink.Name = "helpLink";
            this.helpLink.Size = new System.Drawing.Size(32, 15);
            this.helpLink.TabIndex = 2;
            this.helpLink.TabStop = true;
            this.helpLink.Text = "Help";
            this.helpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_LinkClicked);
            // 
            // showBalloontip
            // 
            this.showBalloontip.AutoSize = true;
            this.showBalloontip.Location = new System.Drawing.Point(6, 31);
            this.showBalloontip.Name = "showBalloontip";
            this.showBalloontip.Size = new System.Drawing.Size(358, 19);
            this.showBalloontip.TabIndex = 2;
            this.showBalloontip.Text = "Show a balloon tip when a screenshot is successfully uploaded ";
            this.showBalloontip.UseVisualStyleBackColor = true;
            this.showBalloontip.CheckedChanged += new System.EventHandler(this.showBalloontip_CheckedChanged);
            // 
            // Settings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(465, 560);
            this.Controls.Add(this.helpLink);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Shown += new System.EventHandler(this.Settings_Shown);
            this.tabControl.ResumeLayout(false);
            this.uploadTab.ResumeLayout(false);
            this.filenameGroup.ResumeLayout(false);
            this.filenameGroup.PerformLayout();
            this.pathGroup.ResumeLayout(false);
            this.pathGroup.PerformLayout();
            this.imageGroup.ResumeLayout(false);
            this.imageGroup.PerformLayout();
            this.connectionTab.ResumeLayout(false);
            this.authGroup.ResumeLayout(false);
            this.authGroup.PerformLayout();
            this.serverGroup.ResumeLayout(false);
            this.serverGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNud)).EndInit();
            this.interfaceTab.ResumeLayout(false);
            this.interfaceTab.PerformLayout();
            this.overlayGroup.ResumeLayout(false);
            this.overlayGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage connectionTab;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.GroupBox serverGroup;
        private System.Windows.Forms.TextBox addressText;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.ComboBox protocolDropdown;
        private System.Windows.Forms.Label protocolLabel;
        private System.Windows.Forms.NumericUpDown portNud;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.GroupBox authGroup;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button browseKeyButton;
        private System.Windows.Forms.TextBox keyText;
        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.LinkLabel helpLink;
        private System.Windows.Forms.TabPage uploadTab;
        private System.Windows.Forms.GroupBox imageGroup;
        private System.Windows.Forms.CheckBox useJpeg;
        private System.Windows.Forms.Label jpegQualityLabel;
        private System.Windows.Forms.GroupBox pathGroup;
        private System.Windows.Forms.Label serverPathLabel;
        private System.Windows.Forms.TextBox serverPathText;
        private System.Windows.Forms.Button browseFailedButton;
        private System.Windows.Forms.TextBox failedText;
        private System.Windows.Forms.TextBox baseUrlText;
        private System.Windows.Forms.Label failedLabel;
        private System.Windows.Forms.Label baseUrlLabel;
        private System.Windows.Forms.GroupBox filenameGroup;
        private System.Windows.Forms.ComboBox formatDropdown;
        private System.Windows.Forms.Label formatLabel;
        private System.Windows.Forms.Label formatExample;
        private System.Windows.Forms.CheckBox enableDuplicateFileCheck;
        private System.Windows.Forms.TabPage interfaceTab;
        private System.Windows.Forms.CheckBox showPreview;
        private System.Windows.Forms.GroupBox overlayGroup;
        private System.Windows.Forms.Label backgroundLabel;
        private Controls.ColorPickerButton backgroundColor;
        private Controls.ColorPickerButton selectionColor;
        private System.Windows.Forms.Label selectionLabel;
        private System.Windows.Forms.Label opacityLabel;
        private Controls.Slider opacitySlider;
        private Controls.Slider qualitySlider;
        private System.Windows.Forms.Button openFailedButton;
        private System.Windows.Forms.CheckBox showBalloontip;
    }
}