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
    public partial class frmPowerInfo : Form
    {
        #region Constant Definitions and Enumerations
        // power state flags
        private const int POWER_STATE_ON = 0x00010000;          // on state
        private const int POWER_STATE_OFF = 0x00020000;         // no power, full off
        private const int POWER_STATE_CRITICAL = 0x00040000;    // critical off
        private const int POWER_STATE_BOOT = 0x00080000;        // boot state
        private const int POWER_STATE_IDLE = 0x00100000;        // idle state
        private const int POWER_STATE_SUSPEND = 0x00200000;     // suspend state
        private const int POWER_STATE_UNATTENDED = 0x00400000;  // Unattended state.
        private const int POWER_STATE_RESET = 0x00800000;       // reset state
        private const int POWER_STATE_USERIDLE = 0x01000000;    // user idle state
        private const int POWER_STATE_BACKLIGHTON = 0x02000000; // device scree backlight on
        private const int POWER_STATE_PASSWORD = 0x10000000;    // This state is password protected.

        private const int POWER_NAME = 0x00000001;
        private const int POWER_FORCE = 0x00001000;
        private const int ERROR_SUCCESS = 0x00000000;       // return value for no errors

        // device class strings
        private const string PMCLASS_GENERIC_DEVICE = "{A32942B7-920C-486b-B0E6-92A702A99B35}";
        private const string PMCLASS_NDIS_MINIPORT = "{98C5250D-C29A-4985-AE5F-AFE5367E5006}";
        private const string PMCLASS_BLOCK_DEVICE = "{8DD679CE-8AB4-43c8-A14A-EA4963FAA715}";
        private const string PMCLASS_DISPLAY = "{EB91C7C9-8BF6-4a2d-9AB8-69724EED97D1}";

        // used for GetDevicePower and SetDevicePower calls
        private enum CE_DEVICE_POWER
        {
            PwrDeviceUnspecified = -1,
            D0 = 0,
            D1,
            D2,
            D3,
            D4,
            PwrDeviceMaximum
        };
        #endregion

        #region Imports stuff
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int SetDevicePower(
            string pvDevice, int dwDeviceFlags, CE_DEVICE_POWER DeviceState);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetDevicePower(
            [MarshalAs(UnmanagedType.LPWStr)]string pvDevice, int dwDeviceFlags, ref CE_DEVICE_POWER DeviceState);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int DevicePowerNotify(
            string device, CE_DEVICE_POWER state, int flags);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetSystemPowerState(
            string pBuffer, int Length, ref int Flags);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern uint SetSystemPowerState(
            string psState, uint StateFlags, uint Options);

        #endregion

        // This function will display the current power state of the
        // device specified by DeviceName (BKL1: for example)
        private void GetDevicePowerState(string DeviceName)
        {
            // get the power state
            CE_DEVICE_POWER state = new CE_DEVICE_POWER();
            int result = GetDevicePower(DeviceName, 1, ref state);

            if (result == ERROR_SUCCESS)
            {
                // display the results
                switch (state)
                {
                    case CE_DEVICE_POWER.PwrDeviceUnspecified:
                        txtDeviceInfo.Text = "Power State: Unspecified";
                        break;
                    case CE_DEVICE_POWER.PwrDeviceMaximum:
                        txtDeviceInfo.Text = "Power State: Maximum";
                        break;
                    default:
                        txtDeviceInfo.Text = "Power State: " + state.ToString();
                        break;
                }
            }
            else
            {
                // display error code in text box
                txtDeviceInfo.Text = "Error occurred, code: ";

                if (result == 2)
                    txtDeviceInfo.Text += "Device not found";

            }

        }

        // This function will attempt to set the power state of the specified
        // device using both DevicePowerNotify and SetDevicePower.  Sometimes
        // DevicePowerNotify by itself will not work.
        private void SetDevicePowerState(string DeviceName, CE_DEVICE_POWER state)
        {
            // set the power state
            int result = DevicePowerNotify(DeviceName, state, POWER_NAME);
            if (result != 0)
            {
                // Show error code
                txtDeviceInfo.Text = "Error in DPN, code: " + result.ToString();
            }
            result = SetDevicePower(DeviceName, POWER_NAME, state);
            if (result != 0)
            {
                txtDeviceInfo.Text += "\r\nError in SDP, code: " + result.ToString();
            }
        }

        // This function will display the current System Power state
        private void GetSysPowerState()
        {
            // create string buffer to receive power state name
            string StateName = new string(' ', 255);
            
            int Flags = 0;
            int result = GetSystemPowerState(StateName, StateName.Length, ref Flags);

            if (result == ERROR_SUCCESS)
            {
                //display the state name
                txtSystemInfo.Text = "System State: " + StateName;
            }
            else
            {
                // display error code
                txtSystemInfo.Text = "Error occurred: " + result.ToString();
            }
        }

        // This function will attempt to force the device to transition
        // to the specified system power state.
        private bool SetSysPowerState(string state)
        {
            uint result = SetSystemPowerState(state, 0, POWER_FORCE);
            if (result != 0)
            {
                // show error code
                txtSystemInfo.Text = "Error occurred: " + result.ToString();
                return false;
            }
            else
            {
                txtSystemInfo.Text = "Success, state: " + state;
                return true;
            }

        }

        // This function takes a registry key and recursively enumerates its values
        // and subkeys while adding each to the parent TreeNode for displaying
        // in the TreeView control
        private void PopulateListFromReg(string rootKey, string keyName, ref ListBox parent)
        {

            // make sure rootKey and keyName aren't empty
            if (!rootKey.Equals(""))
            {
                // list the values first
                string Values = Registry.EnumValues(Registry.RootKey.LocalMachine, rootKey, false);

                // make sure Values is not null
                if (Values != null)
                {
                    // split the coma-delimited return string up into the individual values
                    string[] ValList = Values.Split(',');

                    // iterate through each value and add to the tree
                    // if the device is recognized by GetDevicePower
                    foreach (string curVal in ValList)
                    {
                        if (curVal.StartsWith("Name"))
                        {
                            string DeviceName = curVal.Substring(5, curVal.Length - 6);
                            
                            // check to see if it's recognized by GetDevicePower
                            // if it's recognized
                            CE_DEVICE_POWER state = new CE_DEVICE_POWER();
                            int result = GetDevicePower(DeviceName, 1, ref state);
                            if (result != 2)
                            {
                                parent.Items.Add(DeviceName);
                            }
                        }
                    }
                }

                // now list the keys
                string Keys = Registry.EnumValues(Registry.RootKey.LocalMachine, rootKey, true);
                if (Keys != null)
                {
                    string[] KeyList = Keys.Split(',');
                    // iterate through each subkey and add to the tree
                    foreach (string curKey in KeyList)
                    {
                        if (!curKey.Equals(""))
                        {
                            string subKey = rootKey + "\\" + curKey;
                            PopulateListFromReg(subKey, curKey, ref parent);
                        }
                    }
                }
            }
        }

        public frmPowerInfo()
        {
            InitializeComponent();
            uint result = SetSystemPowerState(null, POWER_STATE_ON, POWER_FORCE);
        }

        public void btnSetSystemState_Click(object sender, EventArgs e)
        {
            // determine the desired state and duration
            // should be in format "state,length(s)"
            string[] tmp;
            if (txtDeviceName.Text.IndexOf(',') > 0)
                tmp = txtDeviceName.Text.Split(',');
            else
                return;

            string StateName = tmp[0];
            int time = System.Convert.ToInt32(tmp[1]) * 1000;
            bool result = SetSysPowerState(StateName);

            if (result)
            {
                // sleep for the specified amount of time and then
                // transition back to On state
                Thread.Sleep(time);
                SetSysPowerState("On");
            }
        }

        public void btnListDevices_Click(object sender, EventArgs e)
        {
            // list all the workable devices from the registry
            string Key = "Drivers\\Active";
            Cursor.Current = Cursors.WaitCursor;
            lstDeviceList.BeginUpdate();
            lstDeviceList.Items.Clear();
            PopulateListFromReg(Key, "", ref lstDeviceList);
            lstDeviceList.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        public void lstDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the current selected item
            string Device = lstDeviceList.SelectedItem.ToString();

            // replace the device name in the text box
            txtDeviceName.Text = Device;
        }

        public void btnSetDevicePower_Click(object sender, EventArgs e)
        {
            // read the device name
            string DeviceName = txtDeviceName.Text;

            if (DeviceName.Equals(""))
            {
                return;
            }
            else
            {
                CE_DEVICE_POWER state = new CE_DEVICE_POWER();

                // determine desired state
                if (radD0.Checked)
                    state = CE_DEVICE_POWER.D0;
                else if (radD1.Checked)
                    state = CE_DEVICE_POWER.D1;
                else if (radD2.Checked)
                    state = CE_DEVICE_POWER.D2;
                else if (radD3.Checked)
                    state = CE_DEVICE_POWER.D3;
                else
                    state = CE_DEVICE_POWER.D4;

                SetDevicePowerState(DeviceName, state);

                // call GetDevicePowerState and auto-update the text box
                // so the user knows the (new) current state
                GetDevicePowerState(DeviceName);
            }
        }

        public void btnGetSystemState_Click(object sender, EventArgs e)
        {
            GetSysPowerState();
        }

        public void btnGetDevicePower_Click(object sender, EventArgs e)
        {
            // read the device name
            string DeviceName = txtDeviceName.Text;

            if (!DeviceName.Equals(""))
                GetDevicePowerState(DeviceName);
        }

        private void frmPowerInfo_Load(object sender, EventArgs e)
        {

        }

        #region form selector handlers
        private void ShowSelector(object sender, EventArgs e)
        {
            frmMain.MainForm.Show();
        }

        private void ShowPowerTrans(object sender, EventArgs e)
        {
            // create a new PowerTrans form if necessary
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

        private void ShowPowerDrain(object sender, EventArgs e)
        {
            if (frmMain.PDForm == null)
                frmMain.PDForm = new frmPowerDrain();
            frmMain.PDForm.Show();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (frmMain.BIForm == null)
                frmMain.BIForm = new frmBatteryInfo();
            frmMain.BIForm.Show();
        }

        private void ShowTimerIinfo(object sender, EventArgs e)
        {
            if (frmMain.TIForm == null)
                frmMain.TIForm = new frmTimerInfo();
            frmMain.TIForm.Show();
        }
        #endregion

        private void Exit(object sender, EventArgs e)
        {
            frmMain.MainForm.Close();
        }


    }
}