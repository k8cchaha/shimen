using System;
using System.Collections;
using System.Collections.Generic;
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
using Aimetis.Symphony.Internationalization;
using Aimetis.Symphony.SDK;
using BaseIDL;
using BaseLibCS;
using BaseLibCS.AiraWS;
using BaseLibCS.Communication;
using DeviceModel;
using DeviceModel.Client;
using FarmLib.Client;

namespace CsharpActiveX
{
    public partial class fMain : UserControl//Form
    {
        private enum ConnectedState
        {
            NOT_CONNECTED,
            CONNECTED,
            BUSY
        };

        #region Constants

        public const uint NAV_1FRAME_BACK = 2;
        public const uint NAV_1FRAME_FORWARD = 4;
        public const uint NAV_1SEC_BACK = 8;
        public const uint NAV_1SEC_FORWARD = 16;
        public const uint NAV_10SEC_BACK = 32;
        public const uint NAV_10SEC_FORWARD = 64;
        public const uint NAV_PLAY = 128; // You probably want to use this one
        public const uint NAV_STOP = 256; // You probably want to use this one
        public const uint NAV_ACTIVITY_BACK = 512;
        public const uint NAV_ACTIVITY_FORWARD = 1024;
        public const uint NAV_ALARM_BACK = 2048;
        public const uint NAV_ALARM_FORWARD = 4096;
        public const uint NAV_LIVE = 8192; // You probably want to use this one
        public const uint NAV_BLANK_CAMERA = 262144;

        #endregion Constants

        #region Member Variables

        private CCamera m_camera = null;
        private SdkFarm m_farm = null;
        private bool m_bFarmConnected = false;
        private int m_iAimSeqNr = 0;
        private int m_iAimClientId = 0;
        private bool m_bClearQuickFind = true;

        private delegate void DelegateEnum(ConnectedState eConnected);

        #endregion

        #region Properties

        private string Password
        {
            get
            {
                return Utils.EncodeString(tbPassword.Text);
            }
        }

        private string User
        {
            get
            {
                return tbUsername.Text;
            }
        }

        #endregion

        #region Constructors

        public fMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new fMain());
        //}

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
                    try
                    {
                        BaseLibCS.Proxy.Registration.Registration registrationProxy =
                            scm.GetWebServiceProxy<BaseLibCS.Proxy.Registration.Registration>();
                        string[] servers = registrationProxy.GetAddressesOfServers(User, Password);
                        return servers;
                    }
                    catch (Exception ex)
                    {
                        string message = string.Empty;
                        Exception uaex =
                        Utils.FindWebServiceException(ex, typeof(UnauthorizedAccessException), out message);

                        if (uaex != null)
                        {
                            throw new UnauthorizedAccessException(message, uaex);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Authorization exception encountered
                throw new Exception("Not Authorized. Check user name and password. Please make sure the service \"AI Infoservice\" is running on the server and that it is not firewalled.");
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                if (ex.ToString().IndexOf("WebException") >= 0) //No Convert
                {
                    AILog.Log(LogLevels.LogError, ex.ToString());
                    message = string.Format("{0} [{1}]. {2}",
                        "Symphony Server is not online or not reachable",
                        tbIp.Text,
                        "Please make sure the service \"AI Infoservice\" is running on the server and that it is not firewalled.");
                }
                else
                {
                    AILog.Log(LogLevels.LogError, ex.ToString());
                    message = string.Format("{0} [{1}]. {2}",
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

                PopulateServerList(servers);

                CNetworkAddress address = new CNetworkAddress(tbIp.Text);
                m_farm = new SdkFarm(address, User, Password);
                m_farm.DeviceModelRefreshTrigger = FarmLib.Client.CFarm.DeviceAutoRefreshTrigger.AnyChange;

                return m_farm.Connect();
            }
            catch (Exception ex)
            {
                return "Failed to connect to farm: " + ex.Message;
            }
        }

        private string RefreshDeviceManager()
        {
            try
            {
                if (m_farm.DeviceManager == null)
                    return "Failed to access Device Manager. Value null";

                CDeviceManager deviceManager = m_farm.DeviceManager;
                deviceManager.DataLoadedEvent += new EventHandler<EventArgs>(DeviceManager_DataLoadedEvent);
                deviceManager.Refresh();
            }
            catch (Exception ex)
            {
                return "Failed to refresh device manager: " + ex.ToString();
            }

            return string.Empty;
        }

        private void DestroyFarm()
        {
            SetConnected(ConnectedState.BUSY);

            if (m_farm != null)
            {
                CDeviceManager deviceManager = m_farm.DeviceManager;
                deviceManager.DataLoadedEvent -= new EventHandler<EventArgs>(DeviceManager_DataLoadedEvent);

                m_farm.SetEnabled(false);
                Thread.Sleep(1000);
                m_farm.Dispose();
                m_farm = null;
                m_camera = null;
            }
            SetConnected(ConnectedState.NOT_CONNECTED);
        }

        private void SetConnected(ConnectedState eConnected)
        {
            bnConnect.Invoke(new EventHandler(delegate
            {
                m_bFarmConnected = (eConnected == ConnectedState.CONNECTED) ? true : false;
                bnConnect.Enabled = (eConnected == ConnectedState.NOT_CONNECTED);
                bnDisconnect.Enabled = (eConnected == ConnectedState.CONNECTED);
            }));
        }

        private bool FarmConnectionOk()
        {
            return FarmConnectionOk(false);
        }

        private bool FarmConnectionOk(bool bQuiet)
        {
            if (m_farm == null || !m_bFarmConnected)
            {
                if (!bQuiet)
                    AddStatusLine("Not connected to farm", true);
                return false;
            }
            return true;
        }

        private void PopulateCameraList()
        {
            if (!FarmConnectionOk())
                return;
            lvCameras.Items.Clear();
            foreach (CCamera cam in m_farm.DeviceManager.GetAllCameras())
            {
                ListViewItem item = new ListViewItem(new string[] { cam.CameraId.ToString(), cam.ToString() });
                lvCameras.Items.Add(item);
            }

            if (m_camera != null)
            {
                ListViewItem lvi = lvCameras.FindItemWithText(m_camera.Name);
                if (lvi != null)
                {
                    lvi.Selected = true;
                }
            }
        }

        private void PopulateServerList(string[] servers)
        {
            lvServers.Items.Clear();
            foreach (string server in servers)
            {
                lvServers.Items.Add(server);
            }
        }

        public void AddStatusLine(string str, bool error)
        {
            rtbStatus.Invoke(new EventHandler(delegate
            {
                rtbStatus.SelectedText = string.Empty;
                if (error)
                    rtbStatus.SelectionColor = Color.Red;
                else
                    rtbStatus.SelectionColor = Color.Green;
                rtbStatus.AppendText(str + "\r\n");
                rtbStatus.ScrollToCaret();
            }
           ));
        }

        private void UpdateControls()
        {
            gbCameraControl.Enabled = (m_camera != null && m_camera.IsPTZ());
            gbRecord.Enabled = (m_camera != null);
            gbAlarm.Enabled = (m_camera != null);
            gbCameras.Enabled = (FarmConnectionOk(true));
            gbVideowall.Enabled = (FarmConnectionOk(true));
            gbUsers.Enabled = (FarmConnectionOk(true));
            if (!FarmConnectionOk(true))
            {
                lvCameras.Items.Clear();
                cbTours.Items.Clear();
            }
        }

        private void CanChangeVideoWalls_Internal()
        {
            if (tbUser.Text.Trim().Length == 0)
            {
                rtbStatus.Text += "\r\nCanChangeVideoWalls: No User Provided\r\n";
                return;
            }
            if (!FarmConnectionOk())
            {
                rtbStatus.Text += "\r\nCanChangeVideoWalls: Farm Not Connected\r\n";
                return;
            }
            if (!m_farm.Version.WebServiceAtleast(6, 58))
            {
                rtbStatus.Text += "\r\nCanChangeVideoWalls: Old server. This function requires the web service to be at least 6.58\r\n";
                return;
            }
            UserModel.Client.CUserManager userManager = (UserModel.Client.CUserManager)m_farm.UserManager;
            UserModel.CUserBase loggedOnUser = userManager.GetUser(m_farm.User);
            if (!userManager.IsUserAdmin(loggedOnUser))
            {
                rtbStatus.Text += "This call is only available for administrators; Access denied";
                return;
            }
            StringBuilder sbVideowalls = new StringBuilder();
            sbVideowalls.Append("CanChangeVideoWalls:\r\n");
            SecurityLib.SecurityProfile currentSecurityProfile = m_farm.SecurityManager.ActiveSecurityProfile;
            sbVideowalls.Append("- security profile: " + currentSecurityProfile.Name + "\r\n");
            DataSet ds = m_farm.Signals.GetChangeCameraOnVideoWalls(tbUser.Text.Trim(), currentSecurityProfile.ID);
            DataTable dt = ds.Tables["ChangeCameraOnVideoWallsPermissions"];
            foreach (DataRow dr in dt.Rows)
            {
                sbVideowalls.Append("\t" + dr["VideoWall"].ToString() + ", permission: " + dr["Permission"].ToString() + ", effective permission: " + dr["EffectivePermission"].ToString() + "\r\n");
            }
            rtbStatus.Text += "\r\n" + sbVideowalls.ToString();
        }

        private void PopulateTourLists()
        {
            cbTours.Items.Clear();
            foreach (CTour tour in m_camera.CameraTours.TourList)
            {
                cbTours.Items.Add(tour.Name);
            }
            if (cbTours.Items.Count > 0)
                cbTours.SelectedIndex = 0;
        }

        private int GetSequenceNumberFromName(string sPrefix, string sName)
        {
            if (!sName.StartsWith(sPrefix))
                return -1;
            int i;
            if (int.TryParse(sName.Substring(sPrefix.Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out i))
                return i;
            else
                return -1;
        }

        private CTour GetSelectedTour()
        {
            if (cbTours.SelectedItem == null)
            {
                return null;
            }

            foreach (CTour t in m_camera.CameraTours.TourList)
            {
                if (cbTours.SelectedItem.ToString() == t.Name)
                    return t;
            }
            AddStatusLine(string.Format("Camera Tour {0} does not exist", cbTours.SelectedItem.ToString()), true);
            return null;
        }

        #endregion

        #region Event Handlers

        private void bnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SetConnected(ConnectedState.BUSY);
                string sStatus = string.Empty;
                DestroyFarm();

                if ((sStatus = LoadFarm()) != string.Empty)
                {
                    AddStatusLine(sStatus, true);
                    SetConnected(ConnectedState.NOT_CONNECTED);
                    return;
                }
                else
                {
                    AddStatusLine("Connected to farm " + m_farm.ToString(), false);
                    SetConnected(ConnectedState.CONNECTED);
                    UpdateControls();
                }

                if ((sStatus = RefreshDeviceManager()) != string.Empty)
                {
                    AddStatusLine(sStatus, true);
                    SetConnected(ConnectedState.NOT_CONNECTED);
                    return;
                }
            }
            catch (Exception ex)
            {
                AddStatusLine("There was an error connecting to the farm: " + ex.ToString(), true);
                SetConnected(ConnectedState.NOT_CONNECTED);
            }

            //Set ClientId & SeqNr for PTZ after connect  
            Random rand = new Random();
            m_iAimClientId = rand.Next(int.MaxValue);
            m_iAimSeqNr = 0;
        }

        private void bnDisconnect_Click(object sender, EventArgs e)
        {
            DestroyFarm();
            UpdateControls();
        }

        /// <summary>
        /// Once the device manager is reloaded we can load the video into the control.
        /// </summary>
        /// <param name="manager"></param>
        void DeviceManager_DataLoadedEvent(object sender, EventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new DelegateVoid((PopulateCameraList)));
                    return;
                }
            }
            catch (Exception ex)
            {
                AddStatusLine("Failed to load device model: " + ex.Message, true);
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = "FarmTest - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void bnGetServers_Click(object sender, EventArgs e)
        {
            //Get the servers and fill the listview
            try
            {
                string[] servers = AttemptConnection();
                PopulateServerList(servers);
            }
            catch (Exception ex)
            {
                AddStatusLine("Failed to connect to farm: " + ex.Message, true);
            }
        }

        private void bnCameras_Click(object sender, EventArgs e)
        {
            //Get the cameras and fill the listview
            PopulateCameraList();
            lvCameras.Select();
        }

        private void lvCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCameras.SelectedItems.Count != 1)
                return;

            uint uiId;
            if (!uint.TryParse(lvCameras.SelectedItems[0].Text, out uiId))
            {
                AddStatusLine("Cannot connect to camera " + lvCameras.SelectedItems[0].Text, true);
                return;
            }

            m_camera = m_farm.DeviceManager.GetCameraById(uiId);
            if (m_bClearQuickFind)
                tbQuickFind.Text = m_camera.Name;
            else
                m_bClearQuickFind = true;

            UpdateControls();
            PopulateTourLists();
        }

        private void bnAlarm_Click(object sender, EventArgs e)
        {
            //Insert Alarm
            // we have to push the alarm directly onto the camera's server
            try
            {
                using (Signals signals = m_camera.Server.Signals)
                {
                    if (signals.libAddAlarm(
                                User, Password,
                                (int)m_camera.CameraId,
                                (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                                (int)(nudPolicyId.Value),
                                tbAlarm.Text,
                                tbAlarm.Text))
                        AddStatusLine("Alarm added successfully.", false);
                    else
                        AddStatusLine("Alarm could not be added. Ensure that you have selected a camera, and that the appropriate service is running (ie 'AI Tracker " + m_camera.CameraId.ToString() + ")", false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                AddStatusLine("An error occured while inserting alarm: " + ex.ToString(), true);
            }
        }

        private void bnClearStatus_Click(object sender, EventArgs e)
        {
            rtbStatus.Clear();
        }

        private void bnQuickFind_Click(object sender, EventArgs e)
        {
            // clear them all and find the match
            int iMatch = -1;
            int i = 0;
            foreach (ListViewItem item in lvCameras.Items)
            {
                item.Selected = false;
				if (string.Equals(item.SubItems[1].Text, tbQuickFind.Text, StringComparison.InvariantCultureIgnoreCase))
				{
					iMatch = i;
				}
                i++;
            }

            if (iMatch == -1)
                return;

            // select just this one
            m_bClearQuickFind = false;
            lvCameras.Items[iMatch].Selected = true;
            lvCameras.Select();
        }

        private void tbUsers_CanChangeVideoWalls_Click(object sender, EventArgs e)
        {
            try
            {
                CanChangeVideoWalls_Internal();
            }
            catch (Exception ex)
            {
                rtbStatus.Text += "\r\nCanChangeVideoWalls: " + ex.ToString();
            }
            rtbStatus.SelectionStart = rtbStatus.Text.Length;
            rtbStatus.SelectionLength = 0;
            rtbStatus.Focus();
        }

        private void bnSwitchVideowallLive_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
            {
                AddStatusLine("No camera selected.", true);
                return;
            }

            try
            {
                using (Signals signals = m_farm.Signals)
                    signals.BeginWallSwitchPanelToCamera(User, Password, tbPanelname.Text, m_camera.CameraId, null, null);
            }
            catch (Exception ex)
            {
                AddStatusLine("Error: " + ex.ToString(), true);
            }
        }

        private void bnSwitchVideowallHistorical_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
            {
                AddStatusLine("No camera selected.", true);
                return;
            }

            try
            {
                // string username, string password, string panelName, uint cameraId, Int64 millisecondsFrom1970GMT, bool bPlay)
                Int64 iMsSince1970 = (dtpHistorical.Value.ToUniversalTime().Ticks - new DateTime(1970, 1, 1).ToUniversalTime().Ticks) / TimeSpan.TicksPerMillisecond;
				using (Signals signals = m_farm.Signals)
				{
					if (cbLoopVideo.Checked)
					{
						signals.WallSwitchPanelToCameraRecordedLoop(User, Password, tbPanelname.Text, m_camera.CameraId, iMsSince1970, uint.Parse(nudVideoLengthSeconds.Text));
					}
					else
					{
						signals.WallSwitchPanelToCameraHistorical(User, Password, tbPanelname.Text, m_camera.CameraId, iMsSince1970, true);
					}
				}
            }
            catch (Exception ex)
            {
                AddStatusLine("Error: " + ex.Message, true);
            }
        }

        private void bnSwitchVideowallNavigate_Click(object sender, EventArgs e)
        {
            Button bn = (Button)sender;
            if (m_camera == null && bn != bnBlank)
            {
                AddStatusLine("No camera selected.", true);
                return;
            }

            uint uiOp = 0;
            if (bn == bnBack1frame) { uiOp = NAV_1FRAME_BACK; }
            if (bn == bnFwd1frame) { uiOp = NAV_1FRAME_FORWARD; }
            if (bn == bnBack1sec) { uiOp = NAV_1SEC_BACK; }
            if (bn == bnFwd1sec) { uiOp = NAV_1SEC_FORWARD; }
            if (bn == bnBack10secs) { uiOp = NAV_10SEC_BACK; }
            if (bn == bnFwd10secs) { uiOp = NAV_10SEC_FORWARD; }
            if (bn == bnPlay) { uiOp = NAV_PLAY; }
            if (bn == bnStop) { uiOp = NAV_STOP; }
            if (bn == bnBackActivity) { uiOp = NAV_ACTIVITY_BACK; }
            if (bn == bnFwdActivity) { uiOp = NAV_ACTIVITY_FORWARD; }
            if (bn == bnBackAlarm) { uiOp = NAV_ALARM_BACK; }
            if (bn == bnFwdAlarm) { uiOp = NAV_ALARM_FORWARD; }
            if (bn == bnLive) { uiOp = NAV_LIVE; }
            if (bn == bnBlank) { uiOp = NAV_BLANK_CAMERA; }

            try
            {
                using (Signals signals = m_farm.Signals)
                    signals.WallNavigatePanel(User, Password, tbPanelname.Text, uiOp);
            }
            catch (Exception ex)
            {
                AddStatusLine("Error: " + ex.Message, true);
            }
        }

        private void bnJPG_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int iHeight = -1; // -1 returns the image in the default size.  Use another value to have it automatically resized.
            //    using (Signals signals = m_farm.Signals)
            //    {
            //        byte[] bImage = signals.WallPanelScreenshotResized(User, Password, tbPanelname.Text, iHeight);
            //        using (FormJpg frm = new FormJpg(this, bImage))
            //            frm.ShowDialog();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AddStatusLine("Error: " + ex.Message, true);
            //}
        }

        private void bnShowPresets_Click(object sender, EventArgs e)
        {
            CTour tour = GetSelectedTour();
            if (tour == null)
            {
                AddStatusLine("No tour selected.", true);
                return;
            }

            AddStatusLine(string.Format("Tour ({0}) ___________________________", tour.Name), false);
            foreach (CLocation loc in tour.Locations)
            {
                AddStatusLine(string.Format("{0} {1} {2} {3}", loc.Name, loc.Pan, loc.Tilt, loc.Zoom), false);
            }
        }

        private void bnPTZ_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int iV = (int)(nudPTZSpeed.Value);	// PTZ Velocity
            if (btn == bnLeft) { m_camera.BeginMoveCamera(-iV, 0, 0, m_iAimClientId, m_iAimSeqNr++, false); };
            if (btn == bnUp) { m_camera.BeginMoveCamera(0, -iV, 0, m_iAimClientId, m_iAimSeqNr++, false); };
            if (btn == bnRight) { m_camera.BeginMoveCamera(iV, 0, 0, m_iAimClientId, m_iAimSeqNr++, false); };
            if (btn == bnDown) { m_camera.BeginMoveCamera(0, iV, 0, m_iAimClientId, m_iAimSeqNr++, false); };
            if (btn == bnZoomTele) { m_camera.BeginMoveCamera(0, 0, iV, m_iAimClientId, m_iAimSeqNr++, false); };
            if (btn == bnZoomWide) { m_camera.BeginMoveCamera(0, 0, -iV, m_iAimClientId, m_iAimSeqNr++, false); };

            // PTZMoveSpecial(button) == 8=iris+, 9=iris-, 10=focus+, 11=focus-, 12=bnRightness+, 13=bnRightness-, 14=contrast+, 15=contrast-
            if (btn == bnFocusNear) { m_camera.PTZMoveSpecial(10); };
            if (btn == bnFocusFar) { m_camera.PTZMoveSpecial(11); };
            if (btn == bnPresetCall) { };
            if (btn == bnPresetSave) { };
        }

        private void bnPTZ_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == bnLeft ||
                btn == bnRight ||
                btn == bnUp ||
                btn == bnDown ||
                btn == bnZoomTele ||
                btn == bnZoomWide)
            {
                m_camera.EndMoveCamera(m_iAimClientId, m_iAimSeqNr++);
            };

            if (btn == bnFocusNear) { };
            if (btn == bnFocusFar) { };
        }

        private void bnPresetCall_Click(object sender, EventArgs e)
        {
            CTour tour = GetSelectedTour();
            if (tour == null)
                return;

            AddStatusLine(string.Format("Going to Tour({0}) Preset({1})", tour.Name, nudPresetPosition.Value), false);
            m_camera.GotoTourLocation(tour.Locations[(int)(nudPresetPosition.Value)]);
        }

        private void bnPresetSave_Click(object sender, EventArgs e)
        {
            //add the current location
            int iTry = 0;
        ReTryAdd:
            int pan = 0;
            int tilt = 0;
            int zoom = 0;

			if (m_camera.IsRelativePTZ)
			{
				//relative
				//the next available memory location

				// TODO JUKA				pan = GetNextId() * 100;
			}
			else
			{
				//absolute
				using (Signals signals = m_farm.Signals)
					signals.PTZGetPosition2(
						User,
						Password,
						m_camera.DeviceID,
						ref pan,
						ref tilt,
						ref zoom,
						true);
			}

            double d_pan = 0;
            double d_tilt = 0;
            double d_zoom = 0;

            string sNamePrefix = Translator.GetString(ResourceID.T_496) + " ";
            int iNewId = 1;

            CTour tour = GetSelectedTour();
            if (tour == null)
            {
                return;
            }

            foreach (CLocation location in tour.Locations)
            {
                int i = GetSequenceNumberFromName(sNamePrefix, location.Name);
                if (iNewId <= i) iNewId = i + 1;
            }

            CLocation item = new CLocation(m_camera.Name, sNamePrefix + iNewId.ToString(CultureInfo.InvariantCulture));

            //float point
            d_pan = ((double)pan) / 100.0;
            d_tilt = ((double)tilt) / 100.0;
            d_zoom = ((double)zoom) / 100.0;

            item.Pan = String.Format((new CultureInfo("en-US")), "{0:0.00}", d_pan); //No Convert
            item.Tilt = String.Format((new CultureInfo("en-US")), "{0:0.00}", d_tilt); //No Convert
            item.Zoom = String.Format((new CultureInfo("en-US")), "{0:0.00}", d_zoom); //No Convert
            item.Length = CTour.DEFAULTLENGTH.ToString(CultureInfo.InvariantCulture);

            //check duplication
            foreach (CLocation _loc in tour.Locations)
            {
                //ListViewItem temp = (ListViewItem)ienum.Current;
                if (_loc.Pan == item.Pan && _loc.Tilt == item.Tilt && _loc.Zoom == item.Zoom)
                {
                    //this position is already in the list, but we may want to try again to
                    //make sure that we get the latest position
                    if (iTry == 3)
                    {
                        //Utils.ShowMessageBox(this, "The current location is already in the list.");
                        Utils.ShowMessageBox(this, Resource.GetString("1436"));
                        return;
                    }
                    else
                    {
                        iTry++;
                        Utils.SleepDoEvents(200);
                        goto ReTryAdd;
                    }

                }
            }

            tour.Locations.Add(item);

            m_camera.CameraTours.Save();

            nudPresetPosition.Maximum = tour.Locations.Count - 1;
        }

        private void cbTours_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CTour tour in m_camera.CameraTours.TourList)
            {
                if (cbTours.SelectedItem.ToString() != tour.Name)
                    continue;

                nudPresetPosition.Maximum = tour.Locations.Count - 1;
            }
        }

        private void bnStopRec_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.StopRecording();
        }

        private void bnResumeRec_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.ResumeRecording();
        }

        private void bnStartRec_Click(object sender, EventArgs e)
        {
            if (m_camera == null)
                return;
            m_camera.StartRecording();
        }

        #endregion

		private void cbLoopVideo_CheckedChanged(object sender, EventArgs e)
		{
			nudVideoLengthSeconds.Enabled = cbLoopVideo.Checked;
		}
    }
}
