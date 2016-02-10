using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace SystemUtils
{
    public partial class frmPowerDrain : Form
    {
        #region consants, enums, and variables
        public enum CEDEVICE_POWER_STATE
        {
            PwrDeviceUnspecified = -1,
            D0 = 0,
            D1 = 1,
            D2 = 2,
            D3 = 3,
            D4 = 4,
            PwrDeviceMaximum
        }

        public const int POWER_FORCE = 0x00001000;
        public const int POWER_NAME = 0x00000001;
        public const int POWER_BOTH = 0x00001001;
        public IntPtr handle = IntPtr.Zero;
        public bool running = false;
        public Thread t = null;
        public System.Windows.Forms.Timer SleepTimer = null;
        #endregion

        #region API imports
        [DllImport("coredll.dll")]
        static public extern IntPtr SetPowerRequirement(
            string Device, CEDEVICE_POWER_STATE DeviceState, ulong DeviceFlags,
            IntPtr SystemState, ulong StateFlags);

        [DllImport("coredll.dll")]
        static public extern uint ReleasePowerRequirement(
            IntPtr Handle);

        [DllImport("coredll.dll")]
        private static extern void SystemIdleTimerReset();
        #endregion

        private double Factorial(double x)
        {
            if (x < 2)
                return 1;
            return x * Factorial(x - 1);
        }

        // This function computes exponentials using a taylor
        // expansion f = x^1/(1!)+ x^2/(2!)...
        private void ComputeExp()
        {
            running = true;
            double tempNum = 1;
            while (running)
            {
                double sum = 0;
                for (int i = 1; i < 15; i++)
                {
                    sum += (Math.Pow(tempNum, (double)i) / Factorial(i));

                }
                tempNum++;
                // just loop back to 1 after 100
                if (tempNum > 100)
                    tempNum = 1;

                Application.DoEvents();
            }
        }

        private void ResetIdleTimer(object source, EventArgs e)
        {
            // reset the systemidletimer
            SystemIdleTimerReset();
        }

        public frmPowerDrain()
        {
            InitializeComponent();

            // create the timer event to call systemidletimerreset
            SleepTimer = new System.Windows.Forms.Timer();
            SleepTimer.Tick += new EventHandler(ResetIdleTimer);
            SleepTimer.Interval = 2000;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // do stuff depending on button text
            switch (btnStartStop.Text)
            {
                case "Start Comp":
                    // create a thread to calculate exponents
                    if (t == null)
                    {
                        t = new Thread(new ThreadStart(ComputeExp));
                        t.IsBackground = true;
                        t.Start();
                    }
                    btnStartStop.Text = "Stop Comp";
                    txtStatus.Text += "Thread started\r\n";
                    break;
                case "Stop Comp":
                    // stop the thread
                    t.Abort();
                    t = null;
                    running = false;
                    btnStartStop.Text = "Start Comp";
                    txtStatus.Text += "Thread stopped\r\n";
                    break;
                default:
                    break;
            }
        }

        private void btnPreventSleep_Click(object sender, EventArgs e)
        {
            // do stuff depending on button text
            switch (btnPreventSleep.Text)
            {
                case "Prevent Sleep":
                    // enable the SleepTimer
                    SleepTimer.Enabled = true;
                    btnPreventSleep.Text = "Resume Sleep";
                    txtStatus.Text += "Starting sleep prevention\r\n";
                    break;
                case "Resume Sleep":
                    SleepTimer.Enabled = false;
                    btnPreventSleep.Text = "Prevent Sleep";
                    txtStatus.Text += "Stopping sleep prevention\r\n";
                    break;
                default:
                    break;
            }
        }

        private void btnSetReserve_Click(object sender, EventArgs e)
        {
            // do stuff based on buttont text
            switch (btnSetReserve.Text)
            {
                case "Set BKL Pw Reserve":
                    // set the backlight power requirement
                    if (handle != IntPtr.Zero)
                    {
                        // already set, ignore
                        break;
                    }
                    string DeviceName = "BKL1:";
                    handle = SetPowerRequirement(DeviceName, CEDEVICE_POWER_STATE.D0, POWER_BOTH, IntPtr.Zero, 0);

                    if (handle == IntPtr.Zero)
                    {
                        MessageBox.Show("error setting requirement");
                    }
                    btnSetReserve.Text = "End BKL Pw Reserve";
                    txtStatus.Text += "Starting backlight power reserve\r\n";
                    break;
                case "End BKL Pw Reserve":
                    // release the requirement
                    if (handle != IntPtr.Zero)
                    {
                        uint result = ReleasePowerRequirement(handle);
                        if (result != (uint)0)
                        {
                            MessageBox.Show("error releasing requirement");

                        }
                        else
                        {
                            btnSetReserve.Text = "Set BKL Pw Reserve";
                            txtStatus.Text += "Ending backlight power reserve\r\n";
                            handle = IntPtr.Zero;
                        }

                    }


                    break;
                default:
                    break;
            }
        }

        #region form selection handlers
        private void ShowPowerInfo(object sender, EventArgs e)
        {
            if (frmMain.PIForm == null)
                frmMain.PIForm = new frmPowerInfo();
            frmMain.PIForm.Show();
        }

        private void ShowPowerTrans(object sender, EventArgs e)
        {
            if (frmMain.PTForm == null)
                frmMain.PTForm = new frmPowerTrans();
            frmMain.PTForm.Show();
        }

        private void ShowRegInfo(object sender, EventArgs e)
        {
            if (frmMain.RIForm == null)
                frmMain.RIForm = new frmRegInfo();
            frmMain.RIForm.Show();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (frmMain.BIForm == null)
                frmMain.BIForm = new frmBatteryInfo();
            frmMain.BIForm.Show();
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
        #endregion

        private void Exit(object sender, EventArgs e)
        {
            frmMain.MainForm.Close();
        }

        private void frmPowerDrain_Closing(object sender, CancelEventArgs e)
        {
            // if comp thread is still running, close it
            if (running == true || t != null)
            {
                t.Abort();
                t = null;
                running = false;
            }
        }
    }
}