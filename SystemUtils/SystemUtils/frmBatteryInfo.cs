using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SystemUtils
{
    public partial class frmBatteryInfo : Form
    {
        #region Constant Definitions and Structs
        // global objects
        System.Windows.Forms.Timer timer1 = null;

        // Define Constants and objects
        private const byte AC_LINE_OFFLINE =            0x00;
        private const byte AC_LINE_ONLINE =             0x01;
        private const byte AC_LINE_BACKUP_POWER =       0x02;
        private const byte AC_LINE_UNKNOWN =            0xFF;
        private const byte BATTERY_FLAG_HIGH =          0x01;
        private const byte BATTERY_FLAG_LOW =           0x02;
        private const byte BATTERY_FLAG_CRITICAL =      0x04;
        private const byte BATTERY_FLAG_CHARGING =      0x08;
        private const byte BATTERY_FLAG_NO_BATTERY =    0x80;
        private const byte BATTERY_FLAG_UNKNOWN =       0xFF;
        private const byte BATTERY_PERCENTAGE_UNKNOWN = 0xFF;
        private const uint BATTERY_LIFE_UNKNOWN = 0xFFFFFFFF;

        private const uint MaxMeasurements = 10000000;
        private bool UpdateList  = true;
        public int TimerInterval =      1000;   // update interval in ms
        public int yScaleFactor  =      3;
        public int yScale =             300;
        public uint StartTime =         0;
        public bool KeepHistory =       true;   // if true, measurements are kept in memory
        public bool ShowGraph =         false;
        public bool setShowGraph =      false;
        public bool UsingTimeStamp =    true;   // if false, means UseTickCount
		public bool EmergencySaved =	false;


        // create queue for keeping track of near-past measurements
		public struct Measurement
		{
			public int Current;
			public uint Temperature;
			public byte Life;
		}

        static public int QueueCapacity = 100;
        //Queue<int> Measurements = new Queue<int>(QueueCapacity);
		Queue<Measurement> Measurements = new Queue<Measurement>(QueueCapacity);

        public struct SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
            public uint BatteryVoltage;
            public int BatteryCurrent;
            public uint BatteryAverageCurrent;
            public uint BatteryAverageInterval;
            public uint BatterymAHourConsumed;
            public uint BatteryTemperature;
            public uint BackupBatteryVoltage;
            public byte BatteryChemistry;
        }

        public struct SYSTEM_POWER_STATUS_EX
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
        }
        #endregion

        #region DLL imports
        [DllImport("coredll")]
        private static extern uint GetSystemPowerStatusEx2(out SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus,
            uint dwLen, bool fUpdate);

        [DllImport("coredll")]
        private static extern uint GetSystemPowerStatusEx(out SYSTEM_POWER_STATUS_EX lpSystemPowerStatus,
            bool fUpdate);

        [DllImport("coredll")]
        private static extern uint GetTickCount();
        #endregion

        public frmBatteryInfo()
        {
            InitializeComponent();

            // set up timer
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = TimerInterval;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object source, EventArgs e)
        {
            // grab status info
            SYSTEM_POWER_STATUS_EX2 status = new SYSTEM_POWER_STATUS_EX2();
       
            uint Size = (uint)Marshal.SizeOf(status);
            uint result = GetSystemPowerStatusEx2(out status,(uint)Marshal.SizeOf(status),true);
            if (Size == result)
            {
                // display the current battery information
				/*
                switch (status.ACLineStatus)
                {
                    case AC_LINE_OFFLINE:
                        lblACStatus.Text = "Offline";
                        break;
                    case AC_LINE_ONLINE:
                        lblACStatus.Text = "Online";
                        break;
                    case AC_LINE_BACKUP_POWER:
                        lblACStatus.Text = "Backup";
                        break;
                    case AC_LINE_UNKNOWN:
                        lblACStatus.Text = "Unknown";
                        break;
                    default:
                        lblACStatus.Text = "Undefined";
                        break;
                }
				*/

                switch (status.BatteryFlag)
                {
                    case BATTERY_FLAG_CHARGING:
                        lblBattStatus.Text = "Charging";
                        break;
                    case BATTERY_FLAG_CRITICAL:
                        lblBattStatus.Text = "Critical";
                        break;
                    case BATTERY_FLAG_HIGH:
                        lblBattStatus.Text = "High";
                        break;
                    case BATTERY_FLAG_LOW:
                        lblBattStatus.Text = "Low";
                        break;
                    case BATTERY_FLAG_NO_BATTERY:
                        lblBattStatus.Text = "None";
                        break;
                    case BATTERY_FLAG_UNKNOWN:
                        lblBattStatus.Text = "Unknown";
                        break;
                    default:
                        lblBattStatus.Text = "Undefined";
                        break;

                }

                if (status.BatteryLifePercent == BATTERY_PERCENTAGE_UNKNOWN)
                    lblBattPercent.Text = "Unknown";
                else
                    lblBattPercent.Text = status.BatteryLifePercent.ToString();


				// if battery percent is 10 percent or below do an emergency save of the current log
				if (status.BatteryLifePercent <= 10)
					SaveLog(source, e);

                if (status.BatteryLifeTime == BATTERY_LIFE_UNKNOWN)
                    lblBattTime.Text = "Unknown";
                else
                {
                    // convert to h:m:s format
                    uint time = status.BatteryLifeTime;
                    uint hours = time / 3600;
                    time -= (hours * 3600);
                    uint minutes = time / 60;
                    time -= (minutes * 60);
                    uint seconds = time;
                    lblBattTime.Text = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                }

                //lblBattTime.Text = status.BatteryLifeTime.ToString();
                lblBattVoltage.Text = status.BatteryVoltage.ToString();
                lblBattCurrent.Text = status.BatteryCurrent.ToString();
				lblTemperature.Text = status.BatteryTemperature.ToString();

            }
            else
            {
                //lblACStatus.Text = "X";
				lblTemperature.Text = "X";
                lblBattStatus.Text = "X";
                lblBattCurrent.Text = "X";
                lblBattPercent.Text = "X";
                lblBattTime.Text = "X";
                lblBattVoltage.Text = "X";

            }

            // determine if we should add to measurement queue and/or listbox
            if (KeepHistory)
            {
                // format the current time
                string curTime = "";
                if (UsingTimeStamp)
                    curTime = System.DateTime.Now.ToString("hh:mm:ss");
                else
                    curTime = (GetTickCount() - StartTime).ToString();

                // add measurement to listbox
                if (UpdateList)
                {
                    lstCurrentInfo.Items.Add("" + curTime + " " + lblBattCurrent.Text);
                    lstCurrentInfo.SelectedIndex = lstCurrentInfo.Items.Count - 1;
                }

                // make room if necessary and add measurement to queue
                if (Measurements.Count == MaxMeasurements)
                    Measurements.Dequeue();

				Measurement M = new Measurement();
				M.Current = status.BatteryCurrent;
				M.Temperature = status.BatteryTemperature;
				M.Life = status.BatteryLifePercent;
                Measurements.Enqueue(M);

            }


            // force garbage collect to prevent memory leak
            System.GC.Collect();

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // Get the current time in the right format
            string curTime = "";
            if (UsingTimeStamp)
                curTime = System.DateTime.Now.ToString("hh:mm:ss");
            else
            {
                StartTime = GetTickCount();
                curTime = (GetTickCount() - StartTime).ToString();
            }

            // check to see whether we need to Stop or Start
            if (btnStartStop.Text == "Start")
            {
                timer1.Enabled = true;
                btnStartStop.Text = "Stop";
                lstCurrentInfo.Items.Add("(" + curTime  + ") Logging started");

            }
            else
            {
                timer1.Enabled = false;
                btnStartStop.Text = "Start";
                lstCurrentInfo.Items.Add("(" + curTime + ") Logging stopped");
            }
        }

        #region Form selection handlers
        private void ShowPowerInfo(object sender, EventArgs e)
        {
            // create a new PowerInfo form if necessary
            if (frmMain.PIForm == null)
                frmMain.PIForm = new frmPowerInfo();
            frmMain.PIForm.Show();
        }

        private void ShowPowerTrans(object sender, EventArgs e)
        {
            // create a new PowerTrans form if necessary
            if (frmMain.PTForm == null)
                frmMain.PTForm = new frmPowerTrans();
            frmMain.PTForm.Show();
        }

        private void ShowPowerDrain(object sender, EventArgs e)
        {
            if (frmMain.PDForm == null)
                frmMain.PDForm = new frmPowerDrain();
            frmMain.PDForm.Show();
        }

        private void ShowRegInfo(object sender, EventArgs e)
        {
            if (frmMain.RIForm == null)
                frmMain.RIForm = new frmRegInfo();
            frmMain.RIForm.Show();
        }

        private void ShowTimerInfo(object sender, EventArgs e)
        {
            if (frmMain.TIForm == null)
                frmMain.TIForm = new frmTimerInfo();
            frmMain.TIForm.Show();
        }

        private void ShowSelector(object sender, EventArgs e)
        {
            frmMain.MainForm.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            frmMain.MainForm.Close();
        }
        #endregion

        #region Update Speed menu handlers

        private void ResetMenuChecks()
        {
            //uncheck all of the menu items
            mnuUpdateSpeed1mSec.Checked = true;
            mnuUpdateSpeed5mSec.Checked = false;
            mnuUpdateSpeed10mSec.Checked = false;
            mnuUpdateSpeed100mSec.Checked = false;
            mnuUpdateSpeed1Sec.Checked = false;
            mnuUpdateSpeed5Sec.Checked = false;
            mnuUpdateSpeed10Sec.Checked = false;
            mnuUpdateSpeed30Sec.Checked = false;
        }

        private void UpdateSpeed1mSec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 1;
            TimerInterval = 1;
            ResetMenuChecks();
            mnuUpdateSpeed1mSec.Checked = true;
        }

        private void UpdateSpeed5mSec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 5;
            TimerInterval = 5;
            ResetMenuChecks();
            mnuUpdateSpeed5mSec.Checked = true;

        }
        private void UpdateSpeed10mSec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 10;
            TimerInterval = 10;
            ResetMenuChecks();
            mnuUpdateSpeed10mSec.Checked = true;
        }

        private void UpdateSpeed100mSec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 100;
            TimerInterval = 100;
            ResetMenuChecks();
            mnuUpdateSpeed100mSec.Checked = true;
        }

        private void UpdateSpeed1Sec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 1000;
            TimerInterval = 1000;
            ResetMenuChecks();
            mnuUpdateSpeed1Sec.Checked = true;
        }

        private void UpdateSpeed5Sec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 5000;
            TimerInterval = 5000;
            ResetMenuChecks();
            mnuUpdateSpeed5Sec.Checked = true;
        }

        private void UpdateSpeed10Sec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 10000;
            TimerInterval = 10000;
            ResetMenuChecks();
            mnuUpdateSpeed10Sec.Checked = true;
        }

        private void UpdateSpeed30Sec(object sender, EventArgs e)
        {
            // change the timer interval
            timer1.Interval = 30000;
            TimerInterval = 30000;
            ResetMenuChecks();
            mnuUpdateSpeed30Sec.Checked = true;
        }
        #endregion

        // This function will save the measurements in memory to a
        // txt Log file.
        private void SaveLog(object sender, EventArgs e)
        {
            // Make sure that there are items in the list
            if (lstCurrentInfo.Items.Count < 1)
            {
                MessageBox.Show("Log is empty.");
                return;
            }

            // Stop the timer for saving
            bool RestartTimer = false;
            if (timer1.Enabled == true)
            {
                RestartTimer = true;
                timer1.Enabled = false;
            }

            // find the next available filename of form PowerLogXXX format
            string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string BaseTextName = folder+ "/PowerLog";
            int FileNum = 0;
            for (; FileNum < 100; FileNum++)
            {
                bool exists = System.IO.File.Exists(BaseTextName + FileNum.ToString() + ".txt");
                if (!exists)
                    break;
            }
            BaseTextName += FileNum.ToString() + ".txt";

            // open the text file for saving
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(BaseTextName))
            {
                try
                {
                    foreach (Measurement line in Measurements)
                        sw.WriteLine(line.Current.ToString() + ", " + line.Temperature.ToString() + ", " + line.Life.ToString());
                    MessageBox.Show("Done saving log.");
                }
                catch (Exception Exc)
                {
                    // display the exception in the list box
                    lstCurrentInfo.Items.Add(Exc.ToString());
                }
            }

            // restart the timer if necessary
            if (RestartTimer)
                timer1.Enabled = true;
        }

        // This function will toggle the KeepHistory option
        private void SetKeepHistory(object sender, EventArgs e)
        {
            if (KeepHistory)
            {
                KeepHistory = false;
                mnuKeepHistory.Checked = false;
            }
            else
            {
                KeepHistory = true;
                mnuKeepHistory.Checked = true;
            }

        }


        private void UseTimeStamp(object sender, EventArgs e)
        {
            mnuUseTimeStamp.Checked = true;
            mnuUseTickCount.Checked = false;
            UsingTimeStamp = true;

        }

        private void UseTickCount(object sender, EventArgs e)
        {
            mnuUseTimeStamp.Checked = false;
            mnuUseTickCount.Checked = true;
            UsingTimeStamp = false;
        }

        // this function will toggle the UpdateList option
        private void SetUpdateList(object sender, EventArgs e)
        {
            UpdateList = !UpdateList;
            mnuSetUpdateList.Checked = UpdateList;

        }

		private void mnuClearHist_Click(object sender, EventArgs e)
		{
			// clear the history
			Measurements.Clear();
			lstCurrentInfo.Items.Clear();
		}
    }
}