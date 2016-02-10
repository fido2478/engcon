namespace SystemUtils
{
    partial class frmBatteryInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed1mSec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed5mSec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed10mSec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed100mSec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed1Sec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed5Sec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed10Sec = new System.Windows.Forms.MenuItem();
			this.mnuUpdateSpeed30Sec = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.mnuKeepHistory = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.mnuUseTimeStamp = new System.Windows.Forms.MenuItem();
			this.mnuUseTickCount = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.mnuSetUpdateList = new System.Windows.Forms.MenuItem();
			this.lblBattCurrent = new System.Windows.Forms.Label();
			this.lblBattVoltage = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblBattStatus = new System.Windows.Forms.Label();
			this.lblTemperature = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblBattTime = new System.Windows.Forms.Label();
			this.lblBattPercent = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnStartStop = new System.Windows.Forms.Button();
			this.lstCurrentInfo = new System.Windows.Forms.ListBox();
			this.mnuClearHist = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem10);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.MenuItems.Add(this.menuItem5);
			this.menuItem1.MenuItems.Add(this.menuItem6);
			this.menuItem1.MenuItems.Add(this.menuItem7);
			this.menuItem1.MenuItems.Add(this.menuItem8);
			this.menuItem1.MenuItems.Add(this.menuItem9);
			this.menuItem1.Text = "Tools";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Power Info";
			this.menuItem2.Click += new System.EventHandler(this.ShowPowerInfo);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Power Trans";
			this.menuItem3.Click += new System.EventHandler(this.ShowPowerTrans);
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "Power Drain";
			this.menuItem4.Click += new System.EventHandler(this.ShowPowerDrain);
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "Reg Info";
			this.menuItem5.Click += new System.EventHandler(this.ShowRegInfo);
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "Timer Info";
			this.menuItem6.Click += new System.EventHandler(this.ShowTimerInfo);
			// 
			// menuItem7
			// 
			this.menuItem7.Text = "-";
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "Selector";
			this.menuItem8.Click += new System.EventHandler(this.ShowSelector);
			// 
			// menuItem9
			// 
			this.menuItem9.Text = "Exit";
			this.menuItem9.Click += new System.EventHandler(this.Exit);
			// 
			// menuItem10
			// 
			this.menuItem10.MenuItems.Add(this.menuItem11);
			this.menuItem10.MenuItems.Add(this.menuItem13);
			this.menuItem10.MenuItems.Add(this.menuItem16);
			this.menuItem10.MenuItems.Add(this.mnuSetUpdateList);
			this.menuItem10.MenuItems.Add(this.mnuClearHist);
			this.menuItem10.Text = "Menu";
			// 
			// menuItem11
			// 
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed1mSec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed5mSec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed10mSec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed100mSec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed1Sec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed5Sec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed10Sec);
			this.menuItem11.MenuItems.Add(this.mnuUpdateSpeed30Sec);
			this.menuItem11.MenuItems.Add(this.menuItem12);
			this.menuItem11.MenuItems.Add(this.mnuKeepHistory);
			this.menuItem11.Text = "Update Speed";
			// 
			// mnuUpdateSpeed1mSec
			// 
			this.mnuUpdateSpeed1mSec.Text = "1 msec";
			this.mnuUpdateSpeed1mSec.Click += new System.EventHandler(this.UpdateSpeed1mSec);
			// 
			// mnuUpdateSpeed5mSec
			// 
			this.mnuUpdateSpeed5mSec.Text = "5 msec";
			this.mnuUpdateSpeed5mSec.Click += new System.EventHandler(this.UpdateSpeed5mSec);
			// 
			// mnuUpdateSpeed10mSec
			// 
			this.mnuUpdateSpeed10mSec.Text = "10 msec";
			this.mnuUpdateSpeed10mSec.Click += new System.EventHandler(this.UpdateSpeed10mSec);
			// 
			// mnuUpdateSpeed100mSec
			// 
			this.mnuUpdateSpeed100mSec.Text = "100 msec";
			this.mnuUpdateSpeed100mSec.Click += new System.EventHandler(this.UpdateSpeed100mSec);
			// 
			// mnuUpdateSpeed1Sec
			// 
			this.mnuUpdateSpeed1Sec.Checked = true;
			this.mnuUpdateSpeed1Sec.Text = "1 second";
			this.mnuUpdateSpeed1Sec.Click += new System.EventHandler(this.UpdateSpeed1Sec);
			// 
			// mnuUpdateSpeed5Sec
			// 
			this.mnuUpdateSpeed5Sec.Text = "5 seconds";
			this.mnuUpdateSpeed5Sec.Click += new System.EventHandler(this.UpdateSpeed5Sec);
			// 
			// mnuUpdateSpeed10Sec
			// 
			this.mnuUpdateSpeed10Sec.Text = "10 seconds";
			this.mnuUpdateSpeed10Sec.Click += new System.EventHandler(this.UpdateSpeed10Sec);
			// 
			// mnuUpdateSpeed30Sec
			// 
			this.mnuUpdateSpeed30Sec.Text = "30 seconds";
			this.mnuUpdateSpeed30Sec.Click += new System.EventHandler(this.UpdateSpeed30Sec);
			// 
			// menuItem12
			// 
			this.menuItem12.Text = "-";
			// 
			// mnuKeepHistory
			// 
			this.mnuKeepHistory.Checked = true;
			this.mnuKeepHistory.Text = "Keep History";
			this.mnuKeepHistory.Click += new System.EventHandler(this.SetKeepHistory);
			// 
			// menuItem13
			// 
			this.menuItem13.MenuItems.Add(this.mnuUseTimeStamp);
			this.menuItem13.MenuItems.Add(this.mnuUseTickCount);
			this.menuItem13.Text = "Time";
			// 
			// mnuUseTimeStamp
			// 
			this.mnuUseTimeStamp.Checked = true;
			this.mnuUseTimeStamp.Text = "Timestamp";
			this.mnuUseTimeStamp.Click += new System.EventHandler(this.UseTimeStamp);
			// 
			// mnuUseTickCount
			// 
			this.mnuUseTickCount.Text = "Tick Count";
			this.mnuUseTickCount.Click += new System.EventHandler(this.UseTickCount);
			// 
			// menuItem16
			// 
			this.menuItem16.Text = "Save Log";
			this.menuItem16.Click += new System.EventHandler(this.SaveLog);
			// 
			// mnuSetUpdateList
			// 
			this.mnuSetUpdateList.Checked = true;
			this.mnuSetUpdateList.Text = "Update List";
			this.mnuSetUpdateList.Click += new System.EventHandler(this.SetUpdateList);
			// 
			// lblBattCurrent
			// 
			this.lblBattCurrent.BackColor = System.Drawing.SystemColors.Control;
			this.lblBattCurrent.Location = new System.Drawing.Point(81, 127);
			this.lblBattCurrent.Name = "lblBattCurrent";
			this.lblBattCurrent.Size = new System.Drawing.Size(70, 15);
			this.lblBattCurrent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblBattVoltage
			// 
			this.lblBattVoltage.BackColor = System.Drawing.SystemColors.Control;
			this.lblBattVoltage.Location = new System.Drawing.Point(81, 103);
			this.lblBattVoltage.Name = "lblBattVoltage";
			this.lblBattVoltage.Size = new System.Drawing.Size(70, 16);
			this.lblBattVoltage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3, 127);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 15);
			this.label6.Text = "Batt I:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3, 103);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 15);
			this.label5.Text = "Batt V:";
			// 
			// lblBattStatus
			// 
			this.lblBattStatus.BackColor = System.Drawing.SystemColors.Control;
			this.lblBattStatus.Location = new System.Drawing.Point(81, 76);
			this.lblBattStatus.Name = "lblBattStatus";
			this.lblBattStatus.Size = new System.Drawing.Size(70, 16);
			this.lblBattStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblTemperature
			// 
			this.lblTemperature.BackColor = System.Drawing.SystemColors.Control;
			this.lblTemperature.Location = new System.Drawing.Point(81, 51);
			this.lblTemperature.Name = "lblTemperature";
			this.lblTemperature.Size = new System.Drawing.Size(70, 16);
			this.lblTemperature.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 15);
			this.label4.Text = "Batt Status:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 16);
			this.label3.Text = "Temperature:";
			// 
			// lblBattTime
			// 
			this.lblBattTime.BackColor = System.Drawing.SystemColors.Control;
			this.lblBattTime.Location = new System.Drawing.Point(81, 30);
			this.lblBattTime.Name = "lblBattTime";
			this.lblBattTime.Size = new System.Drawing.Size(70, 14);
			this.lblBattTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblBattPercent
			// 
			this.lblBattPercent.BackColor = System.Drawing.SystemColors.Control;
			this.lblBattPercent.Location = new System.Drawing.Point(81, 2);
			this.lblBattPercent.Name = "lblBattPercent";
			this.lblBattPercent.Size = new System.Drawing.Size(70, 18);
			this.lblBattPercent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 21);
			this.label2.Text = "Batt Life T:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 20);
			this.label1.Text = "Batt Life %:";
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(171, 3);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(51, 30);
			this.btnStartStop.TabIndex = 24;
			this.btnStartStop.Text = "Start";
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// lstCurrentInfo
			// 
			this.lstCurrentInfo.Location = new System.Drawing.Point(13, 151);
			this.lstCurrentInfo.Name = "lstCurrentInfo";
			this.lstCurrentInfo.Size = new System.Drawing.Size(208, 114);
			this.lstCurrentInfo.TabIndex = 37;
			// 
			// mnuClearHist
			// 
			this.mnuClearHist.Text = "Clear Hist";
			this.mnuClearHist.Click += new System.EventHandler(this.mnuClearHist_Click);
			// 
			// frmBatteryInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.lstCurrentInfo);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.lblBattCurrent);
			this.Controls.Add(this.lblBattVoltage);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblBattStatus);
			this.Controls.Add(this.lblTemperature);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblBattTime);
			this.Controls.Add(this.lblBattPercent);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Name = "frmBatteryInfo";
			this.Text = "frmBatteryInfo";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBattCurrent;
        private System.Windows.Forms.Label lblBattVoltage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBattStatus;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBattTime;
        private System.Windows.Forms.Label lblBattPercent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.ListBox lstCurrentInfo;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed5Sec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed10Sec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed30Sec;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem mnuKeepHistory;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed1mSec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed5mSec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed10mSec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed100mSec;
        private System.Windows.Forms.MenuItem mnuUpdateSpeed1Sec;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem mnuUseTimeStamp;
        private System.Windows.Forms.MenuItem mnuUseTickCount;
        private System.Windows.Forms.MenuItem mnuSetUpdateList;
		private System.Windows.Forms.MenuItem mnuClearHist;
    }
}