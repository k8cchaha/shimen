namespace CsharpActiveX
{
    partial class fMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
			this.gbConnect = new System.Windows.Forms.GroupBox();
			this.bnDisconnect = new System.Windows.Forms.Button();
			this.tbIp = new System.Windows.Forms.TextBox();
			this.bnConnect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tbUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.gbServers = new System.Windows.Forms.GroupBox();
			this.bnGetServers = new System.Windows.Forms.Button();
			this.lvServers = new System.Windows.Forms.ListView();
			this.colIp = new System.Windows.Forms.ColumnHeader();
			this.gbCameras = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.bnQuickFind = new System.Windows.Forms.Button();
			this.bnCameras = new System.Windows.Forms.Button();
			this.tbQuickFind = new System.Windows.Forms.TextBox();
			this.lvCameras = new System.Windows.Forms.ListView();
			this.colId = new System.Windows.Forms.ColumnHeader();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.gbCameraControl = new System.Windows.Forms.GroupBox();
			this.bnShowPresets = new System.Windows.Forms.Button();
			this.cbTours = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.bnPresetCall = new System.Windows.Forms.Button();
			this.bnPresetSave = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.nudPresetPosition = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.nudPTZSpeed = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.bnZoomWide = new System.Windows.Forms.Button();
			this.bnZoomTele = new System.Windows.Forms.Button();
			this.bnDown = new System.Windows.Forms.Button();
			this.bnFocusFar = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.bnFocusNear = new System.Windows.Forms.Button();
			this.bnUp = new System.Windows.Forms.Button();
			this.bnLeft = new System.Windows.Forms.Button();
			this.bnRight = new System.Windows.Forms.Button();
			this.gbVideowall = new System.Windows.Forms.GroupBox();
			this.label17 = new System.Windows.Forms.Label();
			this.nudVideoLengthSeconds = new System.Windows.Forms.NumericUpDown();
			this.cbLoopVideo = new System.Windows.Forms.CheckBox();
			this.bnJPG = new System.Windows.Forms.Button();
			this.bnBlank = new System.Windows.Forms.Button();
			this.bnLive = new System.Windows.Forms.Button();
			this.bnStop = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.bnFwdAlarm = new System.Windows.Forms.Button();
			this.bnBackAlarm = new System.Windows.Forms.Button();
			this.bnFwdActivity = new System.Windows.Forms.Button();
			this.bnBackActivity = new System.Windows.Forms.Button();
			this.bnFwd10secs = new System.Windows.Forms.Button();
			this.bnBack10secs = new System.Windows.Forms.Button();
			this.bnFwd1sec = new System.Windows.Forms.Button();
			this.bnBack1sec = new System.Windows.Forms.Button();
			this.bnPlay = new System.Windows.Forms.Button();
			this.bnFwd1frame = new System.Windows.Forms.Button();
			this.bnBack1frame = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.bnSwitchVideowallHistorical = new System.Windows.Forms.Button();
			this.dtpHistorical = new System.Windows.Forms.DateTimePicker();
			this.bnSwitchVideowallLive = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.tbPanelname = new System.Windows.Forms.TextBox();
			this.gbStatus = new System.Windows.Forms.GroupBox();
			this.bnClearStatus = new System.Windows.Forms.Button();
			this.rtbStatus = new System.Windows.Forms.RichTextBox();
			this.gbAlarm = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.nudPolicyId = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.bnAlarm = new System.Windows.Forms.Button();
			this.tbAlarm = new System.Windows.Forms.TextBox();
			this.gbRecord = new System.Windows.Forms.GroupBox();
			this.bnResumeRec = new System.Windows.Forms.Button();
			this.bnStopRec = new System.Windows.Forms.Button();
			this.bnStartRec = new System.Windows.Forms.Button();
			this.gbUsers = new System.Windows.Forms.GroupBox();
			this.tbUsers_CanChangeVideoWalls = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.gbConnect.SuspendLayout();
			this.gbServers.SuspendLayout();
			this.gbCameras.SuspendLayout();
			this.gbCameraControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPresetPosition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPTZSpeed)).BeginInit();
			this.gbVideowall.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudVideoLengthSeconds)).BeginInit();
			this.gbStatus.SuspendLayout();
			this.gbAlarm.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPolicyId)).BeginInit();
			this.gbRecord.SuspendLayout();
			this.gbUsers.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbConnect
			// 
			this.gbConnect.Controls.Add(this.bnDisconnect);
			this.gbConnect.Controls.Add(this.tbIp);
			this.gbConnect.Controls.Add(this.bnConnect);
			this.gbConnect.Controls.Add(this.label1);
			this.gbConnect.Controls.Add(this.tbUsername);
			this.gbConnect.Controls.Add(this.label2);
			this.gbConnect.Controls.Add(this.label3);
			this.gbConnect.Controls.Add(this.tbPassword);
			this.gbConnect.Location = new System.Drawing.Point(12, 12);
			this.gbConnect.Name = "gbConnect";
			this.gbConnect.Size = new System.Drawing.Size(114, 197);
			this.gbConnect.TabIndex = 0;
			this.gbConnect.TabStop = false;
			this.gbConnect.Text = "Connect Farm";
			// 
			// bnDisconnect
			// 
			this.bnDisconnect.Enabled = false;
			this.bnDisconnect.Location = new System.Drawing.Point(19, 166);
			this.bnDisconnect.Name = "bnDisconnect";
			this.bnDisconnect.Size = new System.Drawing.Size(75, 23);
			this.bnDisconnect.TabIndex = 7;
			this.bnDisconnect.Text = "Disconnect";
			this.bnDisconnect.UseVisualStyleBackColor = true;
			this.bnDisconnect.Click += new System.EventHandler(this.bnDisconnect_Click);
			// 
			// tbIp
			// 
			this.tbIp.Location = new System.Drawing.Point(6, 34);
			this.tbIp.Name = "tbIp";
			this.tbIp.Size = new System.Drawing.Size(100, 20);
			this.tbIp.TabIndex = 1;
			// 
			// bnConnect
			// 
			this.bnConnect.Location = new System.Drawing.Point(19, 138);
			this.bnConnect.Name = "bnConnect";
			this.bnConnect.Size = new System.Drawing.Size(75, 23);
			this.bnConnect.TabIndex = 6;
			this.bnConnect.Text = "Connect";
			this.bnConnect.UseVisualStyleBackColor = true;
			this.bnConnect.Click += new System.EventHandler(this.bnConnect_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP";
			// 
			// tbUsername
			// 
			this.tbUsername.Location = new System.Drawing.Point(6, 73);
			this.tbUsername.Name = "tbUsername";
			this.tbUsername.Size = new System.Drawing.Size(100, 20);
			this.tbUsername.TabIndex = 3;
			this.tbUsername.Text = "admin";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Username";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Password";
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(6, 112);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(100, 20);
			this.tbPassword.TabIndex = 5;
			this.tbPassword.Text = "admin";
			// 
			// gbServers
			// 
			this.gbServers.Controls.Add(this.bnGetServers);
			this.gbServers.Controls.Add(this.lvServers);
			this.gbServers.Location = new System.Drawing.Point(132, 12);
			this.gbServers.Name = "gbServers";
			this.gbServers.Size = new System.Drawing.Size(196, 197);
			this.gbServers.TabIndex = 1;
			this.gbServers.TabStop = false;
			this.gbServers.Text = "Available Servers";
			// 
			// bnGetServers
			// 
			this.bnGetServers.Location = new System.Drawing.Point(6, 166);
			this.bnGetServers.Name = "bnGetServers";
			this.bnGetServers.Size = new System.Drawing.Size(94, 23);
			this.bnGetServers.TabIndex = 45;
			this.bnGetServers.Text = "Get Servers";
			this.bnGetServers.UseVisualStyleBackColor = true;
			this.bnGetServers.Click += new System.EventHandler(this.bnGetServers_Click);
			// 
			// lvServers
			// 
			this.lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIp});
			this.lvServers.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvServers.FullRowSelect = true;
			this.lvServers.Location = new System.Drawing.Point(6, 18);
			this.lvServers.Name = "lvServers";
			this.lvServers.Size = new System.Drawing.Size(181, 142);
			this.lvServers.TabIndex = 44;
			this.lvServers.UseCompatibleStateImageBehavior = false;
			this.lvServers.View = System.Windows.Forms.View.Details;
			// 
			// colIp
			// 
			this.colIp.Text = "IP";
			this.colIp.Width = 175;
			// 
			// gbCameras
			// 
			this.gbCameras.Controls.Add(this.label13);
			this.gbCameras.Controls.Add(this.bnQuickFind);
			this.gbCameras.Controls.Add(this.bnCameras);
			this.gbCameras.Controls.Add(this.tbQuickFind);
			this.gbCameras.Controls.Add(this.lvCameras);
			this.gbCameras.Enabled = false;
			this.gbCameras.Location = new System.Drawing.Point(334, 12);
			this.gbCameras.Name = "gbCameras";
			this.gbCameras.Size = new System.Drawing.Size(228, 262);
			this.gbCameras.TabIndex = 2;
			this.gbCameras.TabStop = false;
			this.gbCameras.Text = "Cameras";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 208);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 13);
			this.label13.TabIndex = 66;
			this.label13.Text = "Find by Name";
			// 
			// bnQuickFind
			// 
			this.bnQuickFind.Location = new System.Drawing.Point(154, 222);
			this.bnQuickFind.Name = "bnQuickFind";
			this.bnQuickFind.Size = new System.Drawing.Size(64, 23);
			this.bnQuickFind.TabIndex = 65;
			this.bnQuickFind.Text = "Find";
			this.bnQuickFind.UseVisualStyleBackColor = true;
			this.bnQuickFind.Click += new System.EventHandler(this.bnQuickFind_Click);
			// 
			// bnCameras
			// 
			this.bnCameras.Location = new System.Drawing.Point(8, 166);
			this.bnCameras.Name = "bnCameras";
			this.bnCameras.Size = new System.Drawing.Size(94, 23);
			this.bnCameras.TabIndex = 45;
			this.bnCameras.Text = "Get Cameras";
			this.bnCameras.UseVisualStyleBackColor = true;
			this.bnCameras.Click += new System.EventHandler(this.bnCameras_Click);
			// 
			// tbQuickFind
			// 
			this.tbQuickFind.Location = new System.Drawing.Point(8, 224);
			this.tbQuickFind.Name = "tbQuickFind";
			this.tbQuickFind.Size = new System.Drawing.Size(139, 20);
			this.tbQuickFind.TabIndex = 53;
			// 
			// lvCameras
			// 
			this.lvCameras.AllowColumnReorder = true;
			this.lvCameras.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colName});
			this.lvCameras.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvCameras.FullRowSelect = true;
			this.lvCameras.Location = new System.Drawing.Point(8, 19);
			this.lvCameras.MultiSelect = false;
			this.lvCameras.Name = "lvCameras";
			this.lvCameras.Size = new System.Drawing.Size(210, 141);
			this.lvCameras.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvCameras.TabIndex = 44;
			this.lvCameras.UseCompatibleStateImageBehavior = false;
			this.lvCameras.View = System.Windows.Forms.View.Details;
			this.lvCameras.SelectedIndexChanged += new System.EventHandler(this.lvCameras_SelectedIndexChanged);
			// 
			// colId
			// 
			this.colId.Text = "ID";
			this.colId.Width = 52;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 150;
			// 
			// gbCameraControl
			// 
			this.gbCameraControl.Controls.Add(this.bnShowPresets);
			this.gbCameraControl.Controls.Add(this.cbTours);
			this.gbCameraControl.Controls.Add(this.label14);
			this.gbCameraControl.Controls.Add(this.bnPresetCall);
			this.gbCameraControl.Controls.Add(this.bnPresetSave);
			this.gbCameraControl.Controls.Add(this.label6);
			this.gbCameraControl.Controls.Add(this.nudPresetPosition);
			this.gbCameraControl.Controls.Add(this.label5);
			this.gbCameraControl.Controls.Add(this.nudPTZSpeed);
			this.gbCameraControl.Controls.Add(this.label4);
			this.gbCameraControl.Controls.Add(this.bnZoomWide);
			this.gbCameraControl.Controls.Add(this.bnZoomTele);
			this.gbCameraControl.Controls.Add(this.bnDown);
			this.gbCameraControl.Controls.Add(this.bnFocusFar);
			this.gbCameraControl.Controls.Add(this.label8);
			this.gbCameraControl.Controls.Add(this.bnFocusNear);
			this.gbCameraControl.Controls.Add(this.bnUp);
			this.gbCameraControl.Controls.Add(this.bnLeft);
			this.gbCameraControl.Controls.Add(this.bnRight);
			this.gbCameraControl.Enabled = false;
			this.gbCameraControl.Location = new System.Drawing.Point(12, 215);
			this.gbCameraControl.Name = "gbCameraControl";
			this.gbCameraControl.Size = new System.Drawing.Size(316, 179);
			this.gbCameraControl.TabIndex = 4;
			this.gbCameraControl.TabStop = false;
			this.gbCameraControl.Text = "Camera Control";
			// 
			// bnShowPresets
			// 
			this.bnShowPresets.Location = new System.Drawing.Point(264, 106);
			this.bnShowPresets.Name = "bnShowPresets";
			this.bnShowPresets.Size = new System.Drawing.Size(43, 23);
			this.bnShowPresets.TabIndex = 14;
			this.bnShowPresets.Text = "Show";
			this.bnShowPresets.UseVisualStyleBackColor = true;
			this.bnShowPresets.Click += new System.EventHandler(this.bnShowPresets_Click);
			// 
			// cbTours
			// 
			this.cbTours.FormattingEnabled = true;
			this.cbTours.Location = new System.Drawing.Point(149, 107);
			this.cbTours.Name = "cbTours";
			this.cbTours.Size = new System.Drawing.Size(109, 21);
			this.cbTours.TabIndex = 13;
			this.cbTours.SelectedIndexChanged += new System.EventHandler(this.cbTours_SelectedIndexChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(114, 110);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(29, 13);
			this.label14.TabIndex = 12;
			this.label14.Text = "Tour";
			// 
			// bnPresetCall
			// 
			this.bnPresetCall.Location = new System.Drawing.Point(207, 140);
			this.bnPresetCall.Name = "bnPresetCall";
			this.bnPresetCall.Size = new System.Drawing.Size(46, 23);
			this.bnPresetCall.TabIndex = 17;
			this.bnPresetCall.Text = "Call";
			this.bnPresetCall.UseVisualStyleBackColor = true;
			this.bnPresetCall.Click += new System.EventHandler(this.bnPresetCall_Click);
			this.bnPresetCall.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnPresetCall.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnPresetSave
			// 
			this.bnPresetSave.Location = new System.Drawing.Point(264, 140);
			this.bnPresetSave.Name = "bnPresetSave";
			this.bnPresetSave.Size = new System.Drawing.Size(43, 23);
			this.bnPresetSave.TabIndex = 18;
			this.bnPresetSave.Text = "Save";
			this.bnPresetSave.UseVisualStyleBackColor = true;
			this.bnPresetSave.Click += new System.EventHandler(this.bnPresetSave_Click);
			this.bnPresetSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnPresetSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(107, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Preset";
			// 
			// nudPresetPosition
			// 
			this.nudPresetPosition.Location = new System.Drawing.Point(149, 140);
			this.nudPresetPosition.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.nudPresetPosition.Name = "nudPresetPosition";
			this.nudPresetPosition.Size = new System.Drawing.Size(47, 20);
			this.nudPresetPosition.TabIndex = 16;
			this.nudPresetPosition.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(28, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "PTZ Speed";
			// 
			// nudPTZSpeed
			// 
			this.nudPTZSpeed.Location = new System.Drawing.Point(30, 126);
			this.nudPTZSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudPTZSpeed.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.nudPTZSpeed.Name = "nudPTZSpeed";
			this.nudPTZSpeed.Size = new System.Drawing.Size(60, 20);
			this.nudPTZSpeed.TabIndex = 11;
			this.nudPTZSpeed.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(156, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Zoom";
			// 
			// bnZoomWide
			// 
			this.bnZoomWide.Location = new System.Drawing.Point(140, 65);
			this.bnZoomWide.Name = "bnZoomWide";
			this.bnZoomWide.Size = new System.Drawing.Size(64, 23);
			this.bnZoomWide.TabIndex = 8;
			this.bnZoomWide.Text = "Wide";
			this.bnZoomWide.UseVisualStyleBackColor = true;
			this.bnZoomWide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnZoomWide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnZoomTele
			// 
			this.bnZoomTele.Location = new System.Drawing.Point(140, 36);
			this.bnZoomTele.Name = "bnZoomTele";
			this.bnZoomTele.Size = new System.Drawing.Size(64, 23);
			this.bnZoomTele.TabIndex = 6;
			this.bnZoomTele.Text = "Tele";
			this.bnZoomTele.UseVisualStyleBackColor = true;
			this.bnZoomTele.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnZoomTele.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnDown
			// 
			this.bnDown.Location = new System.Drawing.Point(59, 75);
			this.bnDown.Name = "bnDown";
			this.bnDown.Size = new System.Drawing.Size(24, 23);
			this.bnDown.TabIndex = 3;
			this.bnDown.Text = "D";
			this.bnDown.UseVisualStyleBackColor = true;
			this.bnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnFocusFar
			// 
			this.bnFocusFar.Location = new System.Drawing.Point(210, 65);
			this.bnFocusFar.Name = "bnFocusFar";
			this.bnFocusFar.Size = new System.Drawing.Size(64, 23);
			this.bnFocusFar.TabIndex = 9;
			this.bnFocusFar.Text = "Far";
			this.bnFocusFar.UseVisualStyleBackColor = true;
			this.bnFocusFar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnFocusFar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(225, 20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(36, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "Focus";
			// 
			// bnFocusNear
			// 
			this.bnFocusNear.Location = new System.Drawing.Point(210, 36);
			this.bnFocusNear.Name = "bnFocusNear";
			this.bnFocusNear.Size = new System.Drawing.Size(64, 23);
			this.bnFocusNear.TabIndex = 7;
			this.bnFocusNear.Text = "Near";
			this.bnFocusNear.UseVisualStyleBackColor = true;
			this.bnFocusNear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnFocusNear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnUp
			// 
			this.bnUp.Location = new System.Drawing.Point(59, 30);
			this.bnUp.Name = "bnUp";
			this.bnUp.Size = new System.Drawing.Size(24, 23);
			this.bnUp.TabIndex = 0;
			this.bnUp.Text = "U";
			this.bnUp.UseVisualStyleBackColor = true;
			this.bnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnLeft
			// 
			this.bnLeft.Location = new System.Drawing.Point(31, 52);
			this.bnLeft.Name = "bnLeft";
			this.bnLeft.Size = new System.Drawing.Size(22, 23);
			this.bnLeft.TabIndex = 1;
			this.bnLeft.Text = "L";
			this.bnLeft.UseVisualStyleBackColor = true;
			this.bnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// bnRight
			// 
			this.bnRight.Location = new System.Drawing.Point(89, 52);
			this.bnRight.Name = "bnRight";
			this.bnRight.Size = new System.Drawing.Size(25, 23);
			this.bnRight.TabIndex = 2;
			this.bnRight.Text = "R";
			this.bnRight.UseVisualStyleBackColor = true;
			this.bnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseDown);
			this.bnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bnPTZ_MouseUp);
			// 
			// gbVideowall
			// 
			this.gbVideowall.Controls.Add(this.label17);
			this.gbVideowall.Controls.Add(this.nudVideoLengthSeconds);
			this.gbVideowall.Controls.Add(this.cbLoopVideo);
			this.gbVideowall.Controls.Add(this.bnJPG);
			this.gbVideowall.Controls.Add(this.bnBlank);
			this.gbVideowall.Controls.Add(this.bnLive);
			this.gbVideowall.Controls.Add(this.bnStop);
			this.gbVideowall.Controls.Add(this.label16);
			this.gbVideowall.Controls.Add(this.label15);
			this.gbVideowall.Controls.Add(this.bnFwdAlarm);
			this.gbVideowall.Controls.Add(this.bnBackAlarm);
			this.gbVideowall.Controls.Add(this.bnFwdActivity);
			this.gbVideowall.Controls.Add(this.bnBackActivity);
			this.gbVideowall.Controls.Add(this.bnFwd10secs);
			this.gbVideowall.Controls.Add(this.bnBack10secs);
			this.gbVideowall.Controls.Add(this.bnFwd1sec);
			this.gbVideowall.Controls.Add(this.bnBack1sec);
			this.gbVideowall.Controls.Add(this.bnPlay);
			this.gbVideowall.Controls.Add(this.bnFwd1frame);
			this.gbVideowall.Controls.Add(this.bnBack1frame);
			this.gbVideowall.Controls.Add(this.label12);
			this.gbVideowall.Controls.Add(this.bnSwitchVideowallHistorical);
			this.gbVideowall.Controls.Add(this.dtpHistorical);
			this.gbVideowall.Controls.Add(this.bnSwitchVideowallLive);
			this.gbVideowall.Controls.Add(this.label7);
			this.gbVideowall.Controls.Add(this.tbPanelname);
			this.gbVideowall.Enabled = false;
			this.gbVideowall.Location = new System.Drawing.Point(568, 12);
			this.gbVideowall.Name = "gbVideowall";
			this.gbVideowall.Size = new System.Drawing.Size(316, 262);
			this.gbVideowall.TabIndex = 3;
			this.gbVideowall.TabStop = false;
			this.gbVideowall.Text = "Videowall";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(129, 108);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(119, 13);
			this.label17.TabIndex = 25;
			this.label17.Text = "Video Length (seconds)";
			// 
			// nudVideoLengthSeconds
			// 
			this.nudVideoLengthSeconds.Enabled = false;
			this.nudVideoLengthSeconds.Location = new System.Drawing.Point(254, 105);
			this.nudVideoLengthSeconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.nudVideoLengthSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudVideoLengthSeconds.Name = "nudVideoLengthSeconds";
			this.nudVideoLengthSeconds.Size = new System.Drawing.Size(47, 20);
			this.nudVideoLengthSeconds.TabIndex = 24;
			this.nudVideoLengthSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// cbLoopVideo
			// 
			this.cbLoopVideo.AutoSize = true;
			this.cbLoopVideo.Location = new System.Drawing.Point(10, 107);
			this.cbLoopVideo.Name = "cbLoopVideo";
			this.cbLoopVideo.Size = new System.Drawing.Size(80, 17);
			this.cbLoopVideo.TabIndex = 23;
			this.cbLoopVideo.Text = "Loop Video";
			this.cbLoopVideo.UseVisualStyleBackColor = true;
			this.cbLoopVideo.CheckedChanged += new System.EventHandler(this.cbLoopVideo_CheckedChanged);
			// 
			// bnJPG
			// 
			this.bnJPG.Location = new System.Drawing.Point(19, 201);
			this.bnJPG.Name = "bnJPG";
			this.bnJPG.Size = new System.Drawing.Size(41, 23);
			this.bnJPG.TabIndex = 14;
			this.bnJPG.Text = "JPG";
			this.bnJPG.UseVisualStyleBackColor = true;
			this.bnJPG.Click += new System.EventHandler(this.bnJPG_Click);
			// 
			// bnBlank
			// 
			this.bnBlank.Location = new System.Drawing.Point(264, 230);
			this.bnBlank.Name = "bnBlank";
			this.bnBlank.Size = new System.Drawing.Size(41, 23);
			this.bnBlank.TabIndex = 22;
			this.bnBlank.Text = "blank";
			this.bnBlank.UseVisualStyleBackColor = true;
			this.bnBlank.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnLive
			// 
			this.bnLive.Location = new System.Drawing.Point(264, 201);
			this.bnLive.Name = "bnLive";
			this.bnLive.Size = new System.Drawing.Size(41, 23);
			this.bnLive.TabIndex = 18;
			this.bnLive.Text = "live";
			this.bnLive.UseVisualStyleBackColor = true;
			this.bnLive.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnStop
			// 
			this.bnStop.Location = new System.Drawing.Point(136, 155);
			this.bnStop.Name = "bnStop";
			this.bnStop.Size = new System.Drawing.Size(31, 23);
			this.bnStop.TabIndex = 9;
			this.bnStop.Text = "| |";
			this.bnStop.UseVisualStyleBackColor = true;
			this.bnStop.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(133, 237);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(32, 13);
			this.label16.TabIndex = 20;
			this.label16.Text = "alarm";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(131, 207);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(40, 13);
			this.label15.TabIndex = 16;
			this.label15.Text = "activity";
			// 
			// bnFwdAlarm
			// 
			this.bnFwdAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bnFwdAlarm.ForeColor = System.Drawing.Color.Red;
			this.bnFwdAlarm.Location = new System.Drawing.Point(174, 230);
			this.bnFwdAlarm.Name = "bnFwdAlarm";
			this.bnFwdAlarm.Size = new System.Drawing.Size(31, 23);
			this.bnFwdAlarm.TabIndex = 21;
			this.bnFwdAlarm.Text = "->";
			this.bnFwdAlarm.UseVisualStyleBackColor = true;
			this.bnFwdAlarm.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnBackAlarm
			// 
			this.bnBackAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bnBackAlarm.ForeColor = System.Drawing.Color.Red;
			this.bnBackAlarm.Location = new System.Drawing.Point(98, 230);
			this.bnBackAlarm.Name = "bnBackAlarm";
			this.bnBackAlarm.Size = new System.Drawing.Size(31, 23);
			this.bnBackAlarm.TabIndex = 19;
			this.bnBackAlarm.Text = "<-";
			this.bnBackAlarm.UseVisualStyleBackColor = true;
			this.bnBackAlarm.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnFwdActivity
			// 
			this.bnFwdActivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bnFwdActivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.bnFwdActivity.Location = new System.Drawing.Point(174, 201);
			this.bnFwdActivity.Name = "bnFwdActivity";
			this.bnFwdActivity.Size = new System.Drawing.Size(31, 23);
			this.bnFwdActivity.TabIndex = 17;
			this.bnFwdActivity.Text = "->";
			this.bnFwdActivity.UseVisualStyleBackColor = true;
			this.bnFwdActivity.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnBackActivity
			// 
			this.bnBackActivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bnBackActivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.bnBackActivity.Location = new System.Drawing.Point(98, 201);
			this.bnBackActivity.Name = "bnBackActivity";
			this.bnBackActivity.Size = new System.Drawing.Size(31, 23);
			this.bnBackActivity.TabIndex = 15;
			this.bnBackActivity.Text = "<-";
			this.bnBackActivity.UseVisualStyleBackColor = true;
			this.bnBackActivity.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnFwd10secs
			// 
			this.bnFwd10secs.Location = new System.Drawing.Point(248, 167);
			this.bnFwd10secs.Name = "bnFwd10secs";
			this.bnFwd10secs.Size = new System.Drawing.Size(35, 23);
			this.bnFwd10secs.TabIndex = 13;
			this.bnFwd10secs.Text = ">>>";
			this.bnFwd10secs.UseVisualStyleBackColor = true;
			this.bnFwd10secs.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnBack10secs
			// 
			this.bnBack10secs.Location = new System.Drawing.Point(19, 167);
			this.bnBack10secs.Name = "bnBack10secs";
			this.bnBack10secs.Size = new System.Drawing.Size(36, 23);
			this.bnBack10secs.TabIndex = 6;
			this.bnBack10secs.Text = "<<<";
			this.bnBack10secs.UseVisualStyleBackColor = true;
			this.bnBack10secs.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnFwd1sec
			// 
			this.bnFwd1sec.Location = new System.Drawing.Point(211, 167);
			this.bnFwd1sec.Name = "bnFwd1sec";
			this.bnFwd1sec.Size = new System.Drawing.Size(31, 23);
			this.bnFwd1sec.TabIndex = 12;
			this.bnFwd1sec.Text = ">>";
			this.bnFwd1sec.UseVisualStyleBackColor = true;
			this.bnFwd1sec.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnBack1sec
			// 
			this.bnBack1sec.Location = new System.Drawing.Point(61, 167);
			this.bnBack1sec.Name = "bnBack1sec";
			this.bnBack1sec.Size = new System.Drawing.Size(31, 23);
			this.bnBack1sec.TabIndex = 7;
			this.bnBack1sec.Text = "<<";
			this.bnBack1sec.UseVisualStyleBackColor = true;
			this.bnBack1sec.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnPlay
			// 
			this.bnPlay.Location = new System.Drawing.Point(136, 180);
			this.bnPlay.Name = "bnPlay";
			this.bnPlay.Size = new System.Drawing.Size(31, 23);
			this.bnPlay.TabIndex = 10;
			this.bnPlay.Text = "|>";
			this.bnPlay.UseVisualStyleBackColor = true;
			this.bnPlay.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnFwd1frame
			// 
			this.bnFwd1frame.Location = new System.Drawing.Point(174, 167);
			this.bnFwd1frame.Name = "bnFwd1frame";
			this.bnFwd1frame.Size = new System.Drawing.Size(31, 23);
			this.bnFwd1frame.TabIndex = 11;
			this.bnFwd1frame.Text = ">";
			this.bnFwd1frame.UseVisualStyleBackColor = true;
			this.bnFwd1frame.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// bnBack1frame
			// 
			this.bnBack1frame.Location = new System.Drawing.Point(98, 167);
			this.bnBack1frame.Name = "bnBack1frame";
			this.bnBack1frame.Size = new System.Drawing.Size(31, 23);
			this.bnBack1frame.TabIndex = 8;
			this.bnBack1frame.Text = "<";
			this.bnBack1frame.UseVisualStyleBackColor = true;
			this.bnBack1frame.Click += new System.EventHandler(this.bnSwitchVideowallNavigate_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(3, 86);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64, 13);
			this.label12.TabIndex = 3;
			this.label12.Text = "Date / Time";
			// 
			// bnSwitchVideowallHistorical
			// 
			this.bnSwitchVideowallHistorical.Location = new System.Drawing.Point(9, 127);
			this.bnSwitchVideowallHistorical.Name = "bnSwitchVideowallHistorical";
			this.bnSwitchVideowallHistorical.Size = new System.Drawing.Size(296, 23);
			this.bnSwitchVideowallHistorical.TabIndex = 5;
			this.bnSwitchVideowallHistorical.Text = "Switch Videowall Historical to Selected Camera";
			this.bnSwitchVideowallHistorical.UseVisualStyleBackColor = true;
			this.bnSwitchVideowallHistorical.Click += new System.EventHandler(this.bnSwitchVideowallHistorical_Click);
			// 
			// dtpHistorical
			// 
			this.dtpHistorical.CustomFormat = "dd.MM.yyyy HH:mm:ss";
			this.dtpHistorical.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpHistorical.Location = new System.Drawing.Point(73, 82);
			this.dtpHistorical.Name = "dtpHistorical";
			this.dtpHistorical.Size = new System.Drawing.Size(159, 20);
			this.dtpHistorical.TabIndex = 4;
			// 
			// bnSwitchVideowallLive
			// 
			this.bnSwitchVideowallLive.Location = new System.Drawing.Point(9, 47);
			this.bnSwitchVideowallLive.Name = "bnSwitchVideowallLive";
			this.bnSwitchVideowallLive.Size = new System.Drawing.Size(296, 23);
			this.bnSwitchVideowallLive.TabIndex = 2;
			this.bnSwitchVideowallLive.Text = "Switch Videowall Live to Selected Camera";
			this.bnSwitchVideowallLive.UseVisualStyleBackColor = true;
			this.bnSwitchVideowallLive.Click += new System.EventHandler(this.bnSwitchVideowallLive_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Panel Name";
			// 
			// tbPanelname
			// 
			this.tbPanelname.Location = new System.Drawing.Point(86, 14);
			this.tbPanelname.Name = "tbPanelname";
			this.tbPanelname.Size = new System.Drawing.Size(219, 20);
			this.tbPanelname.TabIndex = 1;
			this.tbPanelname.Text = "-MV Console-P1";
			// 
			// gbStatus
			// 
			this.gbStatus.Controls.Add(this.bnClearStatus);
			this.gbStatus.Controls.Add(this.rtbStatus);
			this.gbStatus.Location = new System.Drawing.Point(12, 445);
			this.gbStatus.Name = "gbStatus";
			this.gbStatus.Size = new System.Drawing.Size(872, 178);
			this.gbStatus.TabIndex = 7;
			this.gbStatus.TabStop = false;
			this.gbStatus.Text = "Status";
			// 
			// bnClearStatus
			// 
			this.bnClearStatus.Location = new System.Drawing.Point(9, 149);
			this.bnClearStatus.Name = "bnClearStatus";
			this.bnClearStatus.Size = new System.Drawing.Size(75, 23);
			this.bnClearStatus.TabIndex = 38;
			this.bnClearStatus.Text = "Clear";
			this.bnClearStatus.UseVisualStyleBackColor = true;
			this.bnClearStatus.Click += new System.EventHandler(this.bnClearStatus_Click);
			// 
			// rtbStatus
			// 
			this.rtbStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbStatus.Location = new System.Drawing.Point(9, 19);
			this.rtbStatus.Name = "rtbStatus";
			this.rtbStatus.Size = new System.Drawing.Size(852, 124);
			this.rtbStatus.TabIndex = 24;
			this.rtbStatus.Text = "";
			// 
			// gbAlarm
			// 
			this.gbAlarm.Controls.Add(this.label11);
			this.gbAlarm.Controls.Add(this.nudPolicyId);
			this.gbAlarm.Controls.Add(this.label10);
			this.gbAlarm.Controls.Add(this.bnAlarm);
			this.gbAlarm.Controls.Add(this.tbAlarm);
			this.gbAlarm.Enabled = false;
			this.gbAlarm.Location = new System.Drawing.Point(568, 280);
			this.gbAlarm.Name = "gbAlarm";
			this.gbAlarm.Size = new System.Drawing.Size(316, 114);
			this.gbAlarm.TabIndex = 6;
			this.gbAlarm.TabStop = false;
			this.gbAlarm.Text = "Alarm";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(252, 23);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(47, 13);
			this.label11.TabIndex = 56;
			this.label11.Text = "Policy Id";
			// 
			// nudPolicyId
			// 
			this.nudPolicyId.Location = new System.Drawing.Point(255, 41);
			this.nudPolicyId.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.nudPolicyId.Name = "nudPolicyId";
			this.nudPolicyId.Size = new System.Drawing.Size(48, 20);
			this.nudPolicyId.TabIndex = 55;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 25);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(57, 13);
			this.label10.TabIndex = 54;
			this.label10.Text = "Alarm Text";
			// 
			// bnAlarm
			// 
			this.bnAlarm.Location = new System.Drawing.Point(9, 67);
			this.bnAlarm.Name = "bnAlarm";
			this.bnAlarm.Size = new System.Drawing.Size(296, 23);
			this.bnAlarm.TabIndex = 53;
			this.bnAlarm.Text = "Insert Alarm";
			this.bnAlarm.UseVisualStyleBackColor = true;
			this.bnAlarm.Click += new System.EventHandler(this.bnAlarm_Click);
			// 
			// tbAlarm
			// 
			this.tbAlarm.Location = new System.Drawing.Point(9, 41);
			this.tbAlarm.Name = "tbAlarm";
			this.tbAlarm.Size = new System.Drawing.Size(240, 20);
			this.tbAlarm.TabIndex = 52;
			this.tbAlarm.Text = "xxx";
			// 
			// gbRecord
			// 
			this.gbRecord.Controls.Add(this.bnResumeRec);
			this.gbRecord.Controls.Add(this.bnStopRec);
			this.gbRecord.Controls.Add(this.bnStartRec);
			this.gbRecord.Enabled = false;
			this.gbRecord.Location = new System.Drawing.Point(334, 280);
			this.gbRecord.Name = "gbRecord";
			this.gbRecord.Size = new System.Drawing.Size(228, 114);
			this.gbRecord.TabIndex = 5;
			this.gbRecord.TabStop = false;
			this.gbRecord.Text = "Record";
			// 
			// bnResumeRec
			// 
			this.bnResumeRec.Location = new System.Drawing.Point(6, 67);
			this.bnResumeRec.Name = "bnResumeRec";
			this.bnResumeRec.Size = new System.Drawing.Size(212, 23);
			this.bnResumeRec.TabIndex = 44;
			this.bnResumeRec.Text = "Resume Record Mode";
			this.bnResumeRec.UseVisualStyleBackColor = true;
			this.bnResumeRec.Click += new System.EventHandler(this.bnResumeRec_Click);
			// 
			// bnStopRec
			// 
			this.bnStopRec.Location = new System.Drawing.Point(114, 37);
			this.bnStopRec.Name = "bnStopRec";
			this.bnStopRec.Size = new System.Drawing.Size(104, 23);
			this.bnStopRec.TabIndex = 43;
			this.bnStopRec.Text = "Stop";
			this.bnStopRec.UseVisualStyleBackColor = true;
			this.bnStopRec.Click += new System.EventHandler(this.bnStopRec_Click);
			// 
			// bnStartRec
			// 
			this.bnStartRec.Location = new System.Drawing.Point(6, 37);
			this.bnStartRec.Name = "bnStartRec";
			this.bnStartRec.Size = new System.Drawing.Size(97, 23);
			this.bnStartRec.TabIndex = 42;
			this.bnStartRec.Text = "Start";
			this.bnStartRec.UseVisualStyleBackColor = true;
			this.bnStartRec.Click += new System.EventHandler(this.bnStartRec_Click);
			// 
			// gbUsers
			// 
			this.gbUsers.Controls.Add(this.tbUsers_CanChangeVideoWalls);
			this.gbUsers.Controls.Add(this.label9);
			this.gbUsers.Controls.Add(this.tbUser);
			this.gbUsers.Enabled = false;
			this.gbUsers.Location = new System.Drawing.Point(12, 401);
			this.gbUsers.Name = "gbUsers";
			this.gbUsers.Size = new System.Drawing.Size(316, 38);
			this.gbUsers.TabIndex = 8;
			this.gbUsers.TabStop = false;
			// 
			// tbUsers_CanChangeVideoWalls
			// 
			this.tbUsers_CanChangeVideoWalls.Location = new System.Drawing.Point(153, 11);
			this.tbUsers_CanChangeVideoWalls.Name = "tbUsers_CanChangeVideoWalls";
			this.tbUsers_CanChangeVideoWalls.Size = new System.Drawing.Size(154, 23);
			this.tbUsers_CanChangeVideoWalls.TabIndex = 2;
			this.tbUsers_CanChangeVideoWalls.Text = "Can Change Panel Videowall";
			this.tbUsers_CanChangeVideoWalls.UseVisualStyleBackColor = true;
			this.tbUsers_CanChangeVideoWalls.Click += new System.EventHandler(this.tbUsers_CanChangeVideoWalls_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(12, 15);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(29, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "User";
			// 
			// tbUser
			// 
			this.tbUser.Location = new System.Drawing.Point(47, 12);
			this.tbUser.Name = "tbUser";
			this.tbUser.Size = new System.Drawing.Size(100, 20);
			this.tbUser.TabIndex = 0;
			// 
			// fMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888, 622);
			this.Controls.Add(this.gbUsers);
			this.Controls.Add(this.gbRecord);
			this.Controls.Add(this.gbAlarm);
			this.Controls.Add(this.gbStatus);
			this.Controls.Add(this.gbVideowall);
			this.Controls.Add(this.gbCameraControl);
			this.Controls.Add(this.gbCameras);
			this.Controls.Add(this.gbServers);
			this.Controls.Add(this.gbConnect);
			this.MaximumSize = new System.Drawing.Size(904, 660);
			this.MinimumSize = new System.Drawing.Size(904, 660);
			this.Name = "fMain";
			this.Text = "AimFarmTest";
			this.Load += new System.EventHandler(this.fMain_Load);
			this.gbConnect.ResumeLayout(false);
			this.gbConnect.PerformLayout();
			this.gbServers.ResumeLayout(false);
			this.gbCameras.ResumeLayout(false);
			this.gbCameras.PerformLayout();
			this.gbCameraControl.ResumeLayout(false);
			this.gbCameraControl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPresetPosition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPTZSpeed)).EndInit();
			this.gbVideowall.ResumeLayout(false);
			this.gbVideowall.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudVideoLengthSeconds)).EndInit();
			this.gbStatus.ResumeLayout(false);
			this.gbAlarm.ResumeLayout(false);
			this.gbAlarm.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPolicyId)).EndInit();
			this.gbRecord.ResumeLayout(false);
			this.gbUsers.ResumeLayout(false);
			this.gbUsers.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConnect;
        private System.Windows.Forms.Button bnDisconnect;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Button bnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.GroupBox gbServers;
        private System.Windows.Forms.Button bnGetServers;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.ColumnHeader colIp;
        private System.Windows.Forms.GroupBox gbCameras;
        private System.Windows.Forms.ListView lvCameras;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.Button bnCameras;
        private System.Windows.Forms.GroupBox gbCameraControl;
        private System.Windows.Forms.Button bnZoomWide;
        private System.Windows.Forms.Button bnZoomTele;
        private System.Windows.Forms.Button bnDown;
        private System.Windows.Forms.Button bnFocusFar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bnFocusNear;
        private System.Windows.Forms.Button bnUp;
        private System.Windows.Forms.Button bnLeft;
        private System.Windows.Forms.Button bnRight;
        private System.Windows.Forms.Button bnPresetCall;
        private System.Windows.Forms.Button bnPresetSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPresetPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPTZSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbVideowall;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPanelname;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Button bnClearStatus;
        private System.Windows.Forms.RichTextBox rtbStatus;
		private System.Windows.Forms.Button bnSwitchVideowallLive;
        private System.Windows.Forms.Button bnSwitchVideowallHistorical;
		private System.Windows.Forms.DateTimePicker dtpHistorical;
        private System.Windows.Forms.GroupBox gbAlarm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudPolicyId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button bnAlarm;
        private System.Windows.Forms.TextBox tbAlarm;
        private System.Windows.Forms.GroupBox gbRecord;
        private System.Windows.Forms.Button bnStopRec;
		private System.Windows.Forms.Button bnStartRec;
        private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button bnResumeRec;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox cbTours;
		private System.Windows.Forms.Button bnQuickFind;
		private System.Windows.Forms.TextBox tbQuickFind;
		private System.Windows.Forms.Button bnFwd1frame;
		private System.Windows.Forms.Button bnBack1frame;
		private System.Windows.Forms.Button bnFwdAlarm;
		private System.Windows.Forms.Button bnBackAlarm;
		private System.Windows.Forms.Button bnFwdActivity;
		private System.Windows.Forms.Button bnBackActivity;
		private System.Windows.Forms.Button bnFwd10secs;
		private System.Windows.Forms.Button bnBack10secs;
		private System.Windows.Forms.Button bnFwd1sec;
		private System.Windows.Forms.Button bnBack1sec;
		private System.Windows.Forms.Button bnPlay;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button bnBlank;
		private System.Windows.Forms.Button bnLive;
		private System.Windows.Forms.Button bnStop;
		private System.Windows.Forms.Button bnShowPresets;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button bnJPG;
		private System.Windows.Forms.GroupBox gbUsers;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.Button tbUsers_CanChangeVideoWalls;
		private System.Windows.Forms.CheckBox cbLoopVideo;
		private System.Windows.Forms.NumericUpDown nudVideoLengthSeconds;
		private System.Windows.Forms.Label label17;
    }
}

