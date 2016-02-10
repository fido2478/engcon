using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemUtils
{
    public partial class frmMain : Form
    {
        //create holders for each of the form objects
        public static frmMain MainForm = null;
        public static frmPowerInfo PIForm = null;
        public static frmPowerTrans PTForm = null;
        public static frmPowerDrain PDForm = null;
        public static frmRegInfo RIForm = null;
        public static frmBatteryInfo BIForm = null;
        public static frmTimerInfo TIForm = null;
        
        public frmMain()
        {
            InitializeComponent();

            // store a reference to this form for later use
            MainForm = this;
        }

        #region form selection handlers
        private void ShowPowerInfo(object sender, EventArgs e)
        {
            //display the PowerInfo form
            if (PIForm == null)
                PIForm = new frmPowerInfo();
            PIForm.Show();

        }

        private void ShowPowerTrans(object sender, EventArgs e)
        {
            // create a new power trans form if necessary
            if (PTForm == null)
                PTForm = new frmPowerTrans();
            PTForm.Show();
        }

        private void ShowPowerDrain(object sender, EventArgs e)
        {
            if (PDForm == null)
                PDForm = new frmPowerDrain();
            PDForm.Show();
        }

        private void ShowRegInfo(object sender, EventArgs e)
        {
            if (RIForm == null)
                RIForm = new frmRegInfo();
            RIForm.Show();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (BIForm == null)
                BIForm = new frmBatteryInfo();
            BIForm.Show();
        }

        private void ShowTimerInfo(object sender, EventArgs e)
        {
            if (TIForm == null)
                TIForm = new frmTimerInfo();
            TIForm.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            // check to make sure that the PowerDrain computation thread
            // isn't running.
            if (PDForm != null)
            {
                if (PDForm.running == true)
                {
                    PDForm.t.Abort();
                    PDForm.running = false;
                }
            }
        }
    }
}