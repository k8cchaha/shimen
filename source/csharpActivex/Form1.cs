using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Aimetis.Connectivity;
using Aimetis.Symphony;
using Aimetis.Symphony.SDK;
using Aimetis.Symphony.SDK.NotificationMonitors;
using Aimetis.Symphony.Configuration;
using Aimetis.Symphony.DeviceModel;
using AxVideoRecvCtrlLib;
using BaseIDL;
using BaseLibCS;
using BaseLibCS.Communication;
using DeviceModel.Client;
using FarmLib.Client;
using ICSharpCode.SharpZipLib.GZip;
using VideoRecvCtrlLib;

namespace CsharpActiveX
{
    /// <summary>
    /// Live and Historical streaming SDK example
    /// 
    /// To add the Active X control, follow these steps:
    /// 1.  Open the design view of the form where you want to add the control
    /// 2.  From the Tools menu, select "Add/Remove Toolbox Items..."
    /// 3.  Click on the COM Components tab
    /// 4.  Scroll down and check "VideoRecvCtrl Control"
    /// 5.  Click OK
    /// 6.  The Active X control should now appear in the toolbox,
    ///     at the end of the Windows Forms section.
    /// 7.  Drag it onto the design view where you want it.
    /// </summary>
    public class Form1 : UserControl//System.Windows.Forms.Form
    {
        #region Member Variables

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnConnectToFarm;
        private System.Windows.Forms.CheckBox cbHistorical;
        private System.Windows.Forms.DateTimePicker dtpHistorical;
        private System.Windows.Forms.GroupBox gbPTZ;
        private System.Windows.Forms.Button bnStopRecording;
        private System.Windows.Forms.Button bnStartRecording;
        private System.Windows.Forms.Button bnResumeRecordMode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bnMarkAlarm;
        private System.Windows.Forms.ListBox listAlarms;
        private System.Windows.Forms.CheckBox cbAlarmFalsePositive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbAlarmComment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudStream;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Button bnJpg;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private SdkFarm m_farm = null;
        private CCamera m_camera = null;
        private AutoResetEvent m_waitForServerConnection = new AutoResetEvent(false);
        private DateTime m_dtGMT;
        private AxVideoRecvCtrl m_ctrl;
        private IVideoRecvCtrl m_Ictrl;
        private object m_ocx;
        private ComboBox cbCameras;
        private bool m_bDirectConnect = false;
        private bool m_bConnectedToThisCamerasServer = false;
        private uint m_iCameraId = 1;
		private bool m_bViewPrivateVideo;
        private Button btnRefreshAlarms;
        private bool m_bDeviceModelEventHandlerAdded = false;
        private Label label12;
        private ManualResetEvent m_waitForServerInitialized = new ManualResetEvent(false);
        private System.Windows.Forms.Timer _runTimeTimer = new System.Windows.Forms.Timer();
        private AlarmMonitor m_alarmMonitor;
        private DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private bool m_bCameraListPopulating = false;

        private delegate void DelegateVoid();
        private delegate void DelegateBool(bool bEnable);

        #endregion

        #region Constructors

        public Form1(/*string[] args*/)
        {
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            UpdateControls();

            // Read user settings.
            //if (args.Length >= 1) tbUser.Text = args[0];
            //if (args.Length >= 2) tbPass.Text = args[1];
            //if (args.Length >= 3) tbIp.Text = args[2];
            //if (args.Length >= 4)
            //{
            //    if (!uint.TryParse(args[3], out m_iCameraId))
            //        m_iCameraId = 1;
            //}

            _runTimeTimer.Interval = 1000;
            _runTimeTimer.Tick += new EventHandler(_runTimeTimer_Tick);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled Exception: " + e.ExceptionObject.ToString());
        }

        #endregion

        #region Properties

        private string Password
        {
            get
            {
                return Utils.EncodeString(tbPass.Text);
            }
        }

        private string User
        {
            get
            {
                return tbUser.Text;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Ictrl != null)
                {
                    try
                    {
                        m_Ictrl.DestroyGraph();
                    }
                    catch { }
                }
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.bnResumeRecordMode = new System.Windows.Forms.Button();
            this.bnStopRecording = new System.Windows.Forms.Button();
            this.bnStartRecording = new System.Windows.Forms.Button();
            this.gbPTZ = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.bnJpg = new System.Windows.Forms.Button();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.nudStream = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpHistorical = new System.Windows.Forms.DateTimePicker();
            this.cbHistorical = new System.Windows.Forms.CheckBox();
            this.bnConnectToFarm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRefreshAlarms = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAlarmFalsePositive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAlarmComment = new System.Windows.Forms.TextBox();
            this.listAlarms = new System.Windows.Forms.ListBox();
            this.bnMarkAlarm = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbPTZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStream)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.btnDisconnect);
            this.panel1.Controls.Add(this.bnResumeRecordMode);
            this.panel1.Controls.Add(this.bnStopRecording);
            this.panel1.Controls.Add(this.bnStartRecording);
            this.panel1.Controls.Add(this.gbPTZ);
            this.panel1.Controls.Add(this.cbCameras);
            this.panel1.Controls.Add(this.bnJpg);
            this.panel1.Controls.Add(this.tbTime);
            this.panel1.Controls.Add(this.nudStream);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpHistorical);
            this.panel1.Controls.Add(this.cbHistorical);
            this.panel1.Controls.Add(this.bnConnectToFarm);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbIp);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbPass);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 186);
            this.panel1.TabIndex = 0;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(86, 111);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 20);
            this.btnDisconnect.TabIndex = 30;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Visible = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // bnResumeRecordMode
            // 
            this.bnResumeRecordMode.Location = new System.Drawing.Point(535, 91);
            this.bnResumeRecordMode.Name = "bnResumeRecordMode";
            this.bnResumeRecordMode.Size = new System.Drawing.Size(100, 34);
            this.bnResumeRecordMode.TabIndex = 29;
            this.bnResumeRecordMode.Text = "Resume Record Mode";
            this.bnResumeRecordMode.Click += new System.EventHandler(this.bnResumeRecordMode_Click);
            // 
            // bnStopRecording
            // 
            this.bnStopRecording.Location = new System.Drawing.Point(429, 91);
            this.bnStopRecording.Name = "bnStopRecording";
            this.bnStopRecording.Size = new System.Drawing.Size(100, 34);
            this.bnStopRecording.TabIndex = 28;
            this.bnStopRecording.Text = "Stop Recording";
            this.bnStopRecording.Click += new System.EventHandler(this.bnStopRecording_Click);
            // 
            // bnStartRecording
            // 
            this.bnStartRecording.Location = new System.Drawing.Point(323, 91);
            this.bnStartRecording.Name = "bnStartRecording";
            this.bnStartRecording.Size = new System.Drawing.Size(100, 34);
            this.bnStartRecording.TabIndex = 27;
            this.bnStartRecording.Text = "Start Recording";
            this.bnStartRecording.Click += new System.EventHandler(this.bnStartRecording_Click);
            // 
            // gbPTZ
            // 
            this.gbPTZ.Controls.Add(this.label11);
            this.gbPTZ.Location = new System.Drawing.Point(10, 129);
            this.gbPTZ.Name = "gbPTZ";
            this.gbPTZ.Size = new System.Drawing.Size(623, 51);
            this.gbPTZ.TabIndex = 26;
            this.gbPTZ.TabStop = false;
            this.gbPTZ.Text = "PTZ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(404, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Demonstrations of PTZ and tour functionality are in the FarmTest sample applicati" +
                "on.";
            // 
            // cbCameras
            // 
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(260, 10);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(224, 21);
            this.cbCameras.TabIndex = 5;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.cbCameras_SelectedIndexChanged);
            // 
            // bnJpg
            // 
            this.bnJpg.Location = new System.Drawing.Point(384, 37);
            this.bnJpg.Name = "bnJpg";
            this.bnJpg.Size = new System.Drawing.Size(100, 20);
            this.bnJpg.TabIndex = 17;
            this.bnJpg.Text = "Get JPG";
            this.bnJpg.UseVisualStyleBackColor = true;
            this.bnJpg.Click += new System.EventHandler(this.bnJpg_Click);
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(429, 62);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(204, 20);
            this.tbTime.TabIndex = 16;
            // 
            // nudStream
            // 
            this.nudStream.Location = new System.Drawing.Point(261, 37);
            this.nudStream.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudStream.Name = "nudStream";
            this.nudStream.Size = new System.Drawing.Size(39, 20);
            this.nudStream.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Stream";
            // 
            // dtpHistorical
            // 
            this.dtpHistorical.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpHistorical.Enabled = false;
            this.dtpHistorical.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHistorical.Location = new System.Drawing.Point(497, 37);
            this.dtpHistorical.Name = "dtpHistorical";
            this.dtpHistorical.Size = new System.Drawing.Size(136, 20);
            this.dtpHistorical.TabIndex = 11;
            this.dtpHistorical.ValueChanged += new System.EventHandler(this.dtpHistorical_ValueChanged);
            // 
            // cbHistorical
            // 
            this.cbHistorical.Location = new System.Drawing.Point(497, 11);
            this.cbHistorical.Name = "cbHistorical";
            this.cbHistorical.Size = new System.Drawing.Size(104, 16);
            this.cbHistorical.TabIndex = 9;
            this.cbHistorical.Text = "Historical";
            this.cbHistorical.CheckedChanged += new System.EventHandler(this.comboHistorical_CheckedChanged);
            // 
            // bnConnectToFarm
            // 
            this.bnConnectToFarm.Location = new System.Drawing.Point(86, 87);
            this.bnConnectToFarm.Name = "bnConnectToFarm";
            this.bnConnectToFarm.Size = new System.Drawing.Size(100, 20);
            this.bnConnectToFarm.TabIndex = 4;
            this.bnConnectToFarm.Text = "Connect to Farm";
            this.bnConnectToFarm.Click += new System.EventHandler(this.bnConnectToFarm_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(198, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Camera";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(86, 9);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(100, 20);
            this.tbIp.TabIndex = 1;
            this.tbIp.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Server";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(86, 61);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.tbPass.TabIndex = 3;
            this.tbPass.Text = "admin";
            this.tbPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(86, 35);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 2;
            this.tbUser.Text = "admin";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(10, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 466);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRefreshAlarms);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.cbAlarmFalsePositive);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.tbAlarmComment);
            this.panel3.Controls.Add(this.listAlarms);
            this.panel3.Controls.Add(this.bnMarkAlarm);
            this.panel3.Location = new System.Drawing.Point(652, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 669);
            this.panel3.TabIndex = 3;
            // 
            // btnRefreshAlarms
            // 
            this.btnRefreshAlarms.Location = new System.Drawing.Point(251, 130);
            this.btnRefreshAlarms.Name = "btnRefreshAlarms";
            this.btnRefreshAlarms.Size = new System.Drawing.Size(100, 20);
            this.btnRefreshAlarms.TabIndex = 34;
            this.btnRefreshAlarms.Text = "Refresh Alarms";
            this.btnRefreshAlarms.Click += new System.EventHandler(this.btnRefreshAlarms_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(149, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 33;
            this.label10.Text = "Alarms";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(118, 165);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(201, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "[ServerAddress:Port - Camera]";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(73, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Time";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "Date";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbAlarmFalsePositive
            // 
            this.cbAlarmFalsePositive.Location = new System.Drawing.Point(127, 81);
            this.cbAlarmFalsePositive.Name = "cbAlarmFalsePositive";
            this.cbAlarmFalsePositive.Size = new System.Drawing.Size(104, 16);
            this.cbAlarmFalsePositive.TabIndex = 30;
            this.cbAlarmFalsePositive.Text = "False Positive";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(65, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Comment";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbAlarmComment
            // 
            this.tbAlarmComment.Location = new System.Drawing.Point(127, 103);
            this.tbAlarmComment.Name = "tbAlarmComment";
            this.tbAlarmComment.Size = new System.Drawing.Size(224, 20);
            this.tbAlarmComment.TabIndex = 30;
            // 
            // listAlarms
            // 
            this.listAlarms.FormattingEnabled = true;
            this.listAlarms.Location = new System.Drawing.Point(3, 185);
            this.listAlarms.Name = "listAlarms";
            this.listAlarms.Size = new System.Drawing.Size(348, 485);
            this.listAlarms.TabIndex = 4;
            // 
            // bnMarkAlarm
            // 
            this.bnMarkAlarm.Location = new System.Drawing.Point(127, 130);
            this.bnMarkAlarm.Name = "bnMarkAlarm";
            this.bnMarkAlarm.Size = new System.Drawing.Size(100, 20);
            this.bnMarkAlarm.TabIndex = 30;
            this.bnMarkAlarm.Text = "Mark Alarm";
            this.bnMarkAlarm.Click += new System.EventHandler(this.bnMarkAlarm_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(360, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Frame Time:";
            // 
            // Form1
            // 
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //this.ClientSize = new System.Drawing.Size(1008, 670);
            this.Size = new System.Drawing.Size(1008, 670);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Live Stream Test";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbPTZ.ResumeLayout(false);
            this.gbPTZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStream)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //private static void Main(string[] args)
        //{
        //    Application.Run(new Form1(args));
        //}

        public static uint GetAddr(string address)
        {
            IPAddress ip = IPAddress.Parse(address);
            Byte[] bytes = ip.GetAddressBytes();
            int addr;

            addr = (bytes[0] << 24) |
                (bytes[1] << 16) |
                (bytes[2] << 8) |
                (bytes[3]);
            return (uint)addr;
        }

        private string RefreshDeviceManager()
        {
            try
            {
                if (m_farm.DeviceManager == null)
                    return "Failed to access Device Manager. Value null";

                CDeviceManager deviceManager = m_farm.DeviceManager;
                if (!m_bDeviceModelEventHandlerAdded)
                {
                    deviceManager.DataLoadedEvent += new EventHandler<EventArgs>(DeviceManager_DataLoadedEvent);
                    m_bDeviceModelEventHandlerAdded = true;
                }
                deviceManager.Refresh();
            }
            catch (Exception ex)
            {
                return "Failed to refresh device manager: " + ex.ToString();
            }

            return "";
        }

        private void DestroyFarm()
        {
            if (m_farm != null)
            {
                CDeviceManager deviceManager = m_farm.DeviceManager;
                if (m_bDeviceModelEventHandlerAdded)
                {
                    deviceManager.DataLoadedEvent -= new EventHandler<EventArgs>(DeviceManager_DataLoadedEvent);
                    m_bDeviceModelEventHandlerAdded = false;
                }

                //m_alarmMonitor.AlarmReceived -= new EventHandler<AlarmMessageEventArgs>(HandleAlarmMessageReceived);

                m_farm.SetEnabled(false);
                Thread.Sleep(1000);
                m_farm.Dispose();
                m_farm = null;
                m_camera = null;
                m_bConnectedToThisCamerasServer = false;
            }
            m_waitForServerInitialized.Reset();
        }

        private void ConnectToCamera()
        {
            m_bConnectedToThisCamerasServer = false;
            UpdateControls();
            // if the server isn't connected yet
            if (m_camera.Server.State != CServer.ServerState.Connected)
            {
                m_camera.Server.StateChanged += new EventHandler<ValueChangedEventArgs<CServer.ServerState>>(Server_StateChanged);

                // wait 20 seconds				
                if (!m_waitForServerConnection.WaitOne(TimeSpan.FromSeconds(20)))
                {
                    MessageBox.Show("Failed to connect to server");
                    return;
                }
            }

            m_bConnectedToThisCamerasServer = true;
			m_bViewPrivateVideo = m_camera.CanAccess(DeviceRight.ViewPrivateVideo);

            LoadVideo();
            UpdateControls();

            _runTimeTimer.Start();
        }

        private string[] AttemptConnection()
        {
            try
            {
                IPEndPoint[] endPoints = null;
                endPoints = Utils.ToEndPoints(tbIp.Text);
                using (ServerConnectionManager scm = ServerConnectionManager.CreateManager(endPoints, Guid.Empty,
                    new EstablishConnectionOptions(0, TimeSpan.FromSeconds(0))))
                {

                    // Verify the credentials and get list of server addresses
                    BaseLibCS.Proxy.Registration.Registration registrationProxy =
                        scm.GetWebServiceProxy<BaseLibCS.Proxy.Registration.Registration>();
                    string[] servers = registrationProxy.GetAddressesOfServers(User, Password);
                    return servers;
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Authorization exception encountered
                throw new Exception("Not Authorized. Check user name and password. Please make sure the service \"AI Infoservice\" is running on the server and that it is not firewalled. If authenticating against ActiveDirectory you may need to specify <domain>\\<username> (eg microsoft\\bgates).");
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ex.ToString().IndexOf("WebException") >= 0) //No Convert
                {
                    AILog.Log(LogLevels.LogError, ex.ToString());

                    //return "AIRA Server is not online or not reachable";
                    message = string.Format("{0} [{1}]. {2}",
                        "Symphony Server is not online or not reachable",
                        tbIp.Text,
                        "Please make sure the service \"AI Infoservice\" is running on the server and that it is not firewalled. If authenticating against ActiveDirectory you may need to specify <domain>\\<username> (eg microsoft\\bgates)");
                }
                else
                {
                    AILog.Log(LogLevels.LogError, ex.ToString());

                    //return "Error: Could not connect to " + signals.Url;
                    message = string.Format("{0} {1}. {2}",
                        "Error: Could not connect to",
                        tbIp.Text,
                        "Please make sure the service \"AI Infoservice\" is running on the server and that it is not firewalled. If authenticating against ActiveDirectory you may need to specify <domain>\\<username> (eg microsoft\\bgates)");
                }

                throw new Exception(message);
            }
        }

        /// <summary>
        /// Load the farm and connect to it.
        /// </summary>
        /// <returns></returns>
        private string LoadFarm()
        {
            try
            {
                string[] servers = AttemptConnection();
                // Store servers list for future connection attempts when the specified server is down. 
                // In this situation you should create a new CFarm object with a different server's address from this list
                // and attempt initial connection again via m_farm.SetEnabled(true).

                CNetworkAddress address = new CNetworkAddress(tbIp.Text);
                m_farm = new SdkFarm(address, User, Password);
                m_farm.DeviceModelRefreshTrigger = FarmLib.Client.CFarm.DeviceAutoRefreshTrigger.AnyChange;
                MessageBox.Show(m_farm.ConnectionState.ToString());
                return m_farm.Connect();
            }
            catch (Exception ex)
            {
                return "Failed to connect to farm: " + ex.Message;
            }
        }

        private void SetConnectButton(bool bEnable)
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateBool(SetConnectButton), new object[] { bEnable });
                return;
            }
            bnConnectToFarm.Text = ((bEnable) ? "Connect to Farm" : "Connecting...");
            bnConnectToFarm.Enabled = bEnable;

            btnDisconnect.Visible = bEnable && m_farm != null;
        }

        private void UpdateControls()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateVoid(UpdateControls));
                return;
            }

            bnStartRecording.Enabled = m_bConnectedToThisCamerasServer;
            bnStopRecording.Enabled = m_bConnectedToThisCamerasServer;
            bnResumeRecordMode.Enabled = m_bConnectedToThisCamerasServer;
            bnJpg.Enabled = m_bConnectedToThisCamerasServer;
            nudStream.Enabled = m_bConnectedToThisCamerasServer;
            tbTime.Enabled = m_bConnectedToThisCamerasServer;
            cbHistorical.Enabled = m_bConnectedToThisCamerasServer;
            cbCameras.Enabled = m_bConnectedToThisCamerasServer;
            listAlarms.Enabled = m_bConnectedToThisCamerasServer;
            cbAlarmFalsePositive.Enabled = m_bConnectedToThisCamerasServer;
            tbAlarmComment.Enabled = m_bConnectedToThisCamerasServer;
            bnMarkAlarm.Enabled = m_bConnectedToThisCamerasServer;
            btnRefreshAlarms.Enabled = m_bConnectedToThisCamerasServer;
            gbPTZ.Enabled = m_bConnectedToThisCamerasServer ? m_camera.PTZ : false;
        }

        private void SelectCamera(CCamera cam)
        {
            cbCameras.SelectedItem = cam;
            m_camera = cam;
            m_iCameraId = cam.CameraId;
        }

        private void SelectCameraByIdOrAny(uint id)
        {
            if (!SelectCameraById(id))
                SelectAnyCamera();
        }

        private bool SelectCameraById(uint id)
        {
            //If it matches our currently selected camera, reselect it rather than search
            if (m_camera != null && m_camera.CameraId == id)
            {
                SelectCamera(m_camera);
                return true;
            }

            foreach (CCamera cam in m_farm.DeviceManager.GetAllCameras())
            {
                if (id == cam.CameraId)
                {
                    SelectCamera(cam);
                    return true;
                }
            }
            return false;
        }

        private void SelectAnyCamera()
        {
            SelectCamera((CCamera)cbCameras.Items[0]);
        }

        private void PopulateCameraList()
        {
            m_bCameraListPopulating = true;
            cbCameras.Items.Clear();
            foreach (CCamera cam in m_farm.DeviceManager.GetAllCameras())
            {
                cbCameras.Items.Add(cam);
            }
            m_bCameraListPopulating = false;
        }

        private void PopulateUiFromConnectedFarm()
        {
            PopulateCameraList();
            SelectCameraByIdOrAny(m_iCameraId);
            RefreshAlarms();
        }

        private void DestroyOcx()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateVoid(DestroyOcx));
                return;
            }
            _runTimeTimer.Stop();

            AxVideoRecvCtrl ctrl = null;
            if (m_ctrl != null)
            {
                m_ctrl.Enabled = false;
                m_ctrl.Visible = false;
                ctrl = m_ctrl;
                m_ctrl = null;
            }

            m_ocx = null;
            m_Ictrl = null;

            if (ctrl != null)
            {
                ctrl.Parent = null;
                ctrl.Destroy();
                ctrl.Dispose();
            }
        }

        private void CreateOcx()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateVoid(CreateOcx));
                return;
            }

            m_ctrl = new AxVideoRecvCtrl();
            m_ctrl.Name = "VideoRecvCtrlLive";
            m_ctrl.BeginInit();
            m_ctrl.Parent = this.panel2;
            //m_ctrl.Dock = DockStyle.Fill;
            m_ctrl.Enabled = true;
            m_ctrl.Visible = true;
            m_ctrl.BringToFront();
            m_ctrl.EndInit();
            m_ocx = m_ctrl.GetOcx();
            m_Ictrl = (IVideoRecvCtrl)m_ocx;
            SetModeOcx();
            // Delay initialization until now, because we have to set
            //	direct connect settings before init, and we can't set direct connect
            //	settings until we know which camera we're going to use.
            m_Ictrl.InitGraph();
        }

        private void SetModeOcx()
        {
            if (!cbHistorical.Checked && m_bDirectConnect)
            {
                m_Ictrl.SetMode(GraphMode.NetworkFilter, GetParamString(m_camera));
            }
            else
            {
                m_Ictrl.SetMode(GraphMode.VideoRecvFilter, GetParamString(m_camera));
            }
        }

        private string GetParamString(CCamera camera)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DevName=" + m_camera.DevName); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("BitRate=" + m_camera.Bitrate); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("FPS=" + m_camera.RecordFPS); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("Resolution=" + m_camera.Resolution.ToString()); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("Standard=" + m_camera.VideoStandard); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("RotateDegrees=" + m_camera.RotateDegrees); //No Convert
            sb.Append(";"); //No Convert
            sb.Append("DetectionXML=" + m_camera.DetectionXML); //No Convert
            return sb.ToString();
        }

        private void LoadVideo()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(LoadVideo));
                    return;
                }

                if (m_ocx == null)
                    return;

                // The INetworkEndpoint interface on the ocx is not available until after initialization.
                VideoRecvCtrlLib.INetworkEndpoint addr = m_ocx as VideoRecvCtrlLib.INetworkEndpoint;
                if (addr == null)
                {
                    return;
                }

                if (!cbHistorical.Checked && m_bDirectConnect)
                {
                    addr.put_IPAddr(GetAddr(m_camera.Server.NetworkAddress.IPString));
                    if (m_camera.DevName != "VideoRecvIQeye") //No Convert
                        addr.put_Hostname(m_camera.IPAddress);

                    // special case added for munich where they did not want external users
                    // to be able to see web page of camera therefore we could not connect to
                    // port 80 to get video (since port 80 would be blocked.  The RCP+ port
                    // (1756) allows access to video.
                    if (m_camera.DevName == "VideoRecvVCS")
                    {
                        addr.put_URL(m_camera.IPAddress + ":1756");
                        addr.put_PortNum((ushort)1756);
                    }
                    else
                    {
                        addr.put_URL(m_camera.IPAddress + ":" + m_camera.Port);
                        addr.put_PortNum((ushort)m_camera.Port);
                    }
                    addr.put_Username(m_camera.Username);
                    addr.put_Password(Utils.DecodeString(m_camera.Password));
                    string allOptions = m_camera.AllDevOptions;
                    if (!allOptions.EndsWith(";"))
                        allOptions += ";";
                    string devOptions = allOptions + "DirectMode=true;" + GetParamString(m_camera); //No Convert
                    if (m_camera.DevName == "VideoRecvPanasonic") //No Convert
                        devOptions += ";port_base=1";//Panasonic performs BindUDP on the port base 0; this should be removed when this issue s addressed in Panasonic.cpp //No Convert
                    //m_netaddr.put_Options(devOptions);
                    try
                    {
                        m_Ictrl.PutOptionsOnNetaddr(devOptions);
                    }
                    catch (Exception ex)
                    {
                        AILog.Log(ex);
                    }
                }
                else
                {
                    // This code is as it were before direct connect considerations.
                    // Set the IP address in host byte order
                    addr.put_IPAddr(GetAddr(m_camera.Server.IP));

                    // Set the port in host byte order
                    // 500x0 == live stream, where x is the camera ID
                    // 500x2 == historical streaming, where x is the camera ID
                    addr.put_PortNum(cbHistorical.Checked ? m_camera.HistPort : m_camera.LivePort);
                    addr.put_Username(User);
                    addr.put_Password(Password);
                }

                // iDateFormat == 0: dd.mm.yyyy
                // iDateFormat == 1: mm.dd.yyyy
                // iDateFormat == 2: yyyy.mm.dd
                int i24Hour = 1;
                int iDateFormat = 2;
                int iDecorations = 15;
                int iStream = (int)nudStream.Value;
                m_Ictrl.SetStreamNumber(iDecorations + (iDateFormat << 16) + (i24Hour << 8) + (iStream << 12));
				m_Ictrl.SetPrivacyMask(m_bViewPrivateVideo ? 1 : 0);

                int iTZOffsetSeconds = (int)((DateTime.Now.Ticks - DateTime.Now.ToUniversalTime().Ticks) / TimeSpan.TicksPerSecond);
                m_Ictrl.SetRegionalSettings(iTZOffsetSeconds, i24Hour, iDateFormat);

                if (cbHistorical.Checked)
                {
                    // For historical streaming, set the start time
                    // In this example, seek to 2004/11/17 15:00:00.000 GMT
                    VideoRecvCtrlLib.IHistoricalSeek histSeek = m_ocx as VideoRecvCtrlLib.IHistoricalSeek;
                    if (histSeek != null)
                    {
                        histSeek.HistSeek((dtpHistorical.Value.ToUniversalTime() - dt1970).Ticks);
                    }

                    // Increase the speed of playback.
                    // Fast +4.0, but accepts higher numbers as well
                    // Normal Speed: 1.0
                    // Backwards -4.0, but accepts lower numbers as well
                    //IAVSync avs = (IAVSync)m_Ictrl;
                    //avs.SetSpeedD(4.0);
                }

                if (m_Ictrl.IsConnected() == 0)
                    // If we haven't been in here yet, connect up the
                    //   filters in the DirectShow graph.  This has to
                    //   be done AFTER setting the IP address, port,
                    //   username, and password.
                    m_Ictrl.ConnectGraphAuth(User, Password);

                // Start the streaming.
                m_Ictrl.Start(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                try
                {
                    MessageBox.Show(m_Ictrl.GetLastError());
                }
                catch { }
            }
        }

        private void DisplayTime()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateVoid(DisplayTime));
                return;
            }

            if (m_ocx == null)
                return;

            VideoRecvCtrlLib.IServerControl srvr = m_ocx as VideoRecvCtrlLib.IServerControl;
            if (srvr != null)
            {
                long tm;

                srvr.GetLastMediaTime(out tm);
                m_dtGMT = new DateTime(tm + dt1970.Ticks);
                tbTime.Text = m_dtGMT.ToLocalTime().ToString();
            }
        }

        public void GetAlarmsFromWS(DateTime dtStartGMT, DateTime dtEndGMT)
        {
            if (!m_farm.CanAccess(FarmLib.FarmRight.ViewAlarm) || dtStartGMT >= dtEndGMT)
                return;	// not allowed 

            DataSet ret = null;

            // now get the alarms
            byte[] result;
            try
            {
                BaseLibCS.AiraWS.Signals signals = CreateAiraWSSignals();
                if (signals == null)
                    return;

                signals.Timeout = 1000 * 60;
                result = signals.GetAlarms(
                    User,
                    Password,
                    FixDateTimeForWebService(dtStartGMT),
                    FixDateTimeForWebService(dtEndGMT),
                    m_farm.Version.WebServiceAtleast(6, 17) ? string.Empty : m_farm.DeviceManager.GetDeviceListForOldWS(),
                    1,
                    CommonData.DT_1970);

                if ((result == null) || (result.Length < 1))
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving alarm list from server " + m_camera.Server.Name + ": " + ex.ToString());
                return;
            }

            switch (result[0])
            {
                case 1:
                    MemoryStream compStream = new MemoryStream(result, 1, result.Length - 1, false);
                    GZipInputStream uncompStream = new GZipInputStream(compStream);

                    ret = new DataSet();
                    ret.ReadXml(uncompStream);
                    break;

                default:
                    MessageBox.Show("Unexpected version retreiving alarm list from server " + m_camera.Server.Name);
                    return;
            }

            // populate the control
            if ((ret != null) && (ret.Tables.Count > 0))
            {
                AddAlarms(ret.Tables[0]);
            }
        }

        public void AddAlarms(DataTable dt)
        {
            int iRows = 0;
            int iCountLoaded = 0;
            int i = 0;

            iRows = dt.Rows.Count;
            for (i = 0; i < iRows; i++)
            {
                DataRow dr = dt.Rows[i];
                CameraMessageStruct cms = new CameraMessageStruct();

                // Set up Camera Struct from dt data row info
                uint deviceId = uint.Parse(dr["CameraId"].ToString(), CultureInfo.InvariantCulture); //No Convert

                cms.m_iCameraId = deviceId;
                cms.m_iState = (System.UInt32)T_ALARM_STATES.WAIT_TILL_STABLE;
                cms.m_iEvent = uint.Parse(dr["AlarmTypeId"].ToString(), CultureInfo.InvariantCulture); //No Convert
                cms.m_milliTime = ushort.Parse(dr["TmAlarmMs"].ToString(), CultureInfo.InvariantCulture); //No Convert
                cms.m_utcTime = uint.Parse(dr["TmAlarm"].ToString(), CultureInfo.InvariantCulture); //No Convert
                cms.m_iAlarmDbId = uint.Parse(dr["Id"].ToString(), CultureInfo.InvariantCulture); //No Convert

                if (dr.Table.Columns.Contains("PolicyId")) //No Convert
                    cms.m_iPolicyId = int.Parse(dr["PolicyId"].ToString(), CultureInfo.InvariantCulture); //No Convert
                else
                    cms.m_iPolicyId = 0;

                if (dr.Table.Columns.Contains("MsSinceChange"))
                {
                    uint val = 0;
                    if (uint.TryParse(dr["MsSinceChange"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out val))
                    {
                        cms.m_milliSinceChangeBegan = val;
                    }
                    else
                    {
                        cms.m_milliSinceChangeBegan = 0;
                    }
                }
                else
                {
                    cms.m_milliSinceChangeBegan = 0;
                }

                //AddAlarm(cms);
                iCountLoaded++;
            }
        }

        //public void AddAlarm(CameraMessageStruct cms)
        //{
        //    // Calculate the timezone time
        //    DateTime clientTime = Utils.DateTimeFromUTC(cms.m_utcTime);
        //    DateTime serverTime = m_camera.Server.ToLocalTime(clientTime);
        //    cms.m_timezoneTime = (short)(clientTime - serverTime).TotalMinutes;

        //    CDeviceBaseClient device = null;
        //    bool bCameraExists = true;

        //    CCamera camera = m_farm.DeviceManager.GetCameraById(cms.m_iCameraId);
        //    string deviceName;
        //    if (camera != null)
        //    {
        //        device = camera;
        //    }
        //    else
        //    {
        //        device = m_farm.DeviceManager.GetAccessDevice(cms.m_iCameraId);
        //        if (device == null)
        //        {
        //            bCameraExists = false;
        //        }
        //    }

        //    if (bCameraExists)
        //    {
        //        deviceName = device.Name;
        //    }
        //    else
        //        deviceName = "Camera not present";

        //    CameraMessage cm = new CameraMessage(cms, deviceName);
        //    AddAlarm(cm);
        //}

        //private void AddAlarm(CameraMessage cm)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new System.Action<CameraMessage>(AddAlarm), cm);
        //        return;
        //    }

        //    listAlarms.Items.Add(cm);
        //}

        public DateTime FixDateTimeForWebService(DateTime p_dt)
        {
            try
            {
                return m_farm.FirstServer.ToUtcTime(new DateTime(p_dt.Ticks).ToLocalTime());
            }
            catch
            {
                MessageBox.Show("Failed to convert time to UTC time");
                return p_dt;
            }
        }

        private BaseLibCS.AiraWS.Signals CreateAiraWSSignals()
        {
            if (m_camera != null)
            {
                BaseLibCS.AiraWS.Signals signals = m_camera.Server.Signals;
                return signals;
            }
            MessageBox.Show("You need to Connect first.");
            return null;
        }

        private void ConnectToFarm()
        {
            try
            {
                SetConnectButton(false);
                ClearAlarms();

                string sStatus = "";
                DestroyFarm();

                if ((sStatus = LoadFarm()) != "")
                {
                    MessageBox.Show(sStatus);
                    return;
                }

                if ((sStatus = RefreshDeviceManager()) != "")
                {
                    MessageBox.Show(sStatus);
                    return;
                }

                if (!m_waitForServerInitialized.WaitOne(TimeSpan.FromSeconds(60)))
                {
                    MessageBox.Show("Server connection established but server did not initialize within 60 seconds.");
                    DestroyFarm();
                    return;
                }

                m_alarmMonitor = new AlarmMonitor(m_farm);
                m_alarmMonitor.AlarmReceived += new EventHandler<AlarmMessageEventArgs>(HandleAlarmMessageReceived);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error connecting to the farm: " + ex.ToString());
            }
            finally
            {
                SetConnectButton(true);
            }
        }

        private void ClearAlarms()
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateVoid(ClearAlarms));
                return;
            }

            listAlarms.Items.Clear();
            listAlarms.Invalidate();
        }

        private void PlayVideo()
        {
            if (m_ctrl != null)
            {
                DestroyOcx();
            }
            CreateOcx();
            ConnectToCamera();
        }

        #endregion

        #region Event Handlers

        private void bnConnectToFarm_Click(object sender, System.EventArgs e)
        {
            Utils.CreateWorkerThread("ConnectToFarm", ConnectToFarm);
        }

        private void Server_StateChanged(object sender, ValueChangedEventArgs<CServer.ServerState> e)
        {
            if (e.NewValue == e.PreviousValue)
                return;

            if (e.NewValue == CServer.ServerState.Connected)
                m_waitForServerConnection.Set();
        }

        /// <summary>
        /// Once the device manager is reloaded we can load the video into the control.
        /// </summary>
        /// <param name="manager"></param>
        private void DeviceManager_DataLoadedEvent(object sender, EventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler<EventArgs>(DeviceManager_DataLoadedEvent), sender, e);
                    return;
                }

                PopulateUiFromConnectedFarm();
                m_waitForServerInitialized.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load video: " + ex.ToString());
            }
        }

        private void cbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bCameraListPopulating || cbCameras.SelectedItem == m_camera)
            {
                return;
            }

            m_camera = (CCamera)cbCameras.SelectedItem;
            m_iCameraId = m_camera.CameraId;
			// Direct connect has to be specified before InitGraph.
			m_bDirectConnect = CUserSettings.GetUserSettings().EffectiveDirectConnectionToCamera == DefaultableBool.True && m_camera.DirectConnectSupported;

            PlayVideo();
        }

        private void comboHistorical_CheckedChanged(object sender, System.EventArgs e)
        {
            dtpHistorical.Enabled = cbHistorical.Checked;
            PlayVideo();
        }

        private void bnJpg_Click(object sender, EventArgs e)
        {
   //         DateTime jpgTime = m_camera.Server.ToUtcTime(DateTime.Now);
   //         if (cbHistorical.Checked)
   //         {
   //             jpgTime = m_camera.Server.ToUtcTime(dtpHistorical.Value);
   //         }

   //         BaseLibCS.AiraWS.Signals signals = CreateAiraWSSignals();
   //         string sFilename;
			//int iStuffedDecoration = 15 + Utils.GetViewPrivateVideoDecoration(m_bViewPrivateVideo);
			//byte[] byteJpg = signals.GetJPEGImage3(User, Password, m_camera.CameraId, jpgTime, 0, false, iStuffedDecoration, out sFilename);
   //         if (byteJpg != null)
   //         {
   //             MemoryStream ms = new MemoryStream(byteJpg, false);
   //             Image image = (Bitmap)Image.FromStream(ms);
   //             FormJpg form = new FormJpg();
   //             form.pbJpg.Image = image;
   //             form.Show();
   //         }
        }

        private void bnStopRecording_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.StopRecording();
        }

        private void bnResumeRecordMode_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.ResumeRecording();
        }

        private void bnStartRecording_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.StartRecording();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DestroyFarm();
            DestroyOcx();
            UpdateControls();
            SetConnectButton(true);
        }

        private void bnMarkAlarm_Click(object sender, EventArgs e)
        {
            CameraMessage cm = (CameraMessage)listAlarms.SelectedItem;
            if (cm != null)
            {
                uint uiAlarmId = cm.m_msg.m_iAlarmDbId;
                BaseLibCS.AiraWS.Signals signals = CreateAiraWSSignals();

                signals.MarkAlarm3(User, Password, uiAlarmId, cbAlarmFalsePositive.Checked,
                                    Utils.SqlEnquote(tbAlarmComment.Text),
                                    "",		// do not use this
                                    true);
            }
        }

        private void btnRefreshAlarms_Click(object sender, EventArgs e)
        {
            RefreshAlarms();           
        }

        private void RefreshAlarms()
        {
            listAlarms.Items.Clear();
            GetAlarmsFromWS(DateTime.Today, DateTime.Today.AddDays(1));
        }

        private void dtpHistorical_ValueChanged(object sender, EventArgs e)
        {
            PlayVideo();
        }

        private void _runTimeTimer_Tick(object sender, EventArgs e)
        {
            DisplayTime();
        }

        private void HandleAlarmMessageReceived(object sender, AlarmMessageEventArgs e)
        {
            CameraMessageStruct cameraMessageStruct = e.Message;
            //AddAlarm(cameraMessageStruct);
        }
        #endregion
    }
}
