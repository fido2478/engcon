namespace SystemUtils
{
    partial class frmPowerDrain
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
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnPreventSleep = new System.Windows.Forms.Button();
            this.btnSetReserve = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
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
            this.menuItem4.Text = "Reg Info";
            this.menuItem4.Click += new System.EventHandler(this.ShowRegInfo);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Battery Info";
            this.menuItem5.Click += new System.EventHandler(this.ShowBatteryInfo);
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
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(0, 126);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(237, 124);
            this.txtStatus.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(94, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.Text = "Status:";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(22, 14);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(88, 25);
            this.btnStartStop.TabIndex = 6;
            this.btnStartStop.Text = "Start Comp";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnPreventSleep
            // 
            this.btnPreventSleep.Location = new System.Drawing.Point(116, 14);
            this.btnPreventSleep.Name = "btnPreventSleep";
            this.btnPreventSleep.Size = new System.Drawing.Size(93, 25);
            this.btnPreventSleep.TabIndex = 7;
            this.btnPreventSleep.Text = "Prevent Sleep";
            this.btnPreventSleep.Click += new System.EventHandler(this.btnPreventSleep_Click);
            // 
            // btnSetReserve
            // 
            this.btnSetReserve.Location = new System.Drawing.Point(52, 45);
            this.btnSetReserve.Name = "btnSetReserve";
            this.btnSetReserve.Size = new System.Drawing.Size(138, 26);
            this.btnSetReserve.TabIndex = 8;
            this.btnSetReserve.Text = "Set BKL Pw Reserve";
            this.btnSetReserve.Click += new System.EventHandler(this.btnSetReserve_Click);
            // 
            // frmPowerDrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnSetReserve);
            this.Controls.Add(this.btnPreventSleep);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "frmPowerDrain";
            this.Text = "frmPowerDrain";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmPowerDrain_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnPreventSleep;
        private System.Windows.Forms.Button btnSetReserve;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
    }
}