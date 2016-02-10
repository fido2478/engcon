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
    public partial class frmTimerInfo : Form
    {
        [DllImport("coredll.dll")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, int pvParam, uint fWinIni);

        private const uint SPI_SETBATTERYIDLETIMEOUT = 251;
        private const uint SPI_GETBATTERYIDLETIMEOUT = 252;
        private const uint SPI_SETEXTERNALIDLETIMEOUT = 253;
        private const uint SPI_GETEXTERNALIDLETIMEOUT = 254;
        private const uint SPI_SETWAKEUPIDLETIMEOUT = 256;
        private const uint SPI_GETWAKEUPIDLETIMEOUT = 256;
        private const int PlatformTypeBufferSize = 256;

        private int GetIdleTimeout(uint Param)
        {
            int timeout = 1;
            bool result = SystemParametersInfo(Param, 0, timeout, 0);

            if (result)
            {
                return timeout;
            }
            else
            {
                return 0;
            }

        }

        public frmTimerInfo()
        {
            InitializeComponent();
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            // get the gwes info
            string MainKey = "System\\CurrentControlSet\\Control\\Power";
            string Values = Registry.EnumValues(Registry.RootKey.LocalMachine, MainKey, false);

            // now add the power manager info
            MainKey = "System\\CurrentControlSet\\Control\\Power\\Timeouts";
            Values += "," + Registry.EnumValues(Registry.RootKey.LocalMachine, MainKey, false);

            if (!Values.Equals(""))
            {
                string[] ValList = Values.Split(',');
                foreach (string curVal in ValList)
                {
                    if (!curVal.Equals(""))
                    {

                        // grab the part of the string before the first '('
                        string ValName = curVal.Substring(0, curVal.IndexOf("("));
                        string Number = curVal.Substring(curVal.LastIndexOf("d") + 1, curVal.Length - curVal.LastIndexOf("d") - 2);

                        switch (ValName)
                        {
                            case "BattSuspendTimeout":
                                txtBatteryIdleTimeout.Text = Number + " s";
                                break;
                            case "ACSuspendTimeout":
                                txtACIdleTimeout.Text = Number + " s";
                                break;
                            case "WakeupPowerOff":
                                txtWakeIdleTimeout.Text = Number + " s";
                                break;
                            case "ACResumingSuspendTimeout":
                                txtACResumeTimeout.Text = Number + " s";
                                break;
                            case "BattResumingSuspendTimeout":
                                txtBatteryResumeTimeout.Text = Number + " s";
                                break;
                            default:
                                break;
                        }
                    }
                }
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

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (frmMain.BIForm == null)
                frmMain.BIForm = new frmBatteryInfo();
            frmMain.BIForm.Show();
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
    }
}