namespace SystemUtils
{
    partial class frmTimerInfo
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
            this.txtBatteryResumeTimeout = new System.Windows.Forms.TextBox();
            this.txtACResumeTimeout = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWakeIdleTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtACIdleTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBatteryIdleTimeout = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetInfo = new System.Windows.Forms.Button();
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
            this.menuItem6.Text = "Battery Info";
            this.menuItem6.Click += new System.EventHandler(this.ShowBatteryInfo);
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
            // txtBatteryResumeTimeout
            // 
            this.txtBatteryResumeTimeout.Enabled = false;
            this.txtBatteryResumeTimeout.Location = new System.Drawing.Point(141, 127);
            this.txtBatteryResumeTimeout.Name = "txtBatteryResumeTimeout";
            this.txtBatteryResumeTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtBatteryResumeTimeout.TabIndex = 23;
            // 
            // txtACResumeTimeout
            // 
            this.txtACResumeTimeout.Enabled = false;
            this.txtACResumeTimeout.Location = new System.Drawing.Point(141, 99);
            this.txtACResumeTimeout.Name = "txtACResumeTimeout";
            this.txtACResumeTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtACResumeTimeout.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.Text = "Batt Res. Timeout:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 19);
            this.label4.Text = "AC Res. Timeout:";
            // 
            // txtWakeIdleTimeout
            // 
            this.txtWakeIdleTimeout.Enabled = false;
            this.txtWakeIdleTimeout.Location = new System.Drawing.Point(141, 68);
            this.txtWakeIdleTimeout.Name = "txtWakeIdleTimeout";
            this.txtWakeIdleTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtWakeIdleTimeout.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 19);
            this.label3.Text = "Wake Idle Timeout:";
            // 
            // txtACIdleTimeout
            // 
            this.txtACIdleTimeout.Enabled = false;
            this.txtACIdleTimeout.Location = new System.Drawing.Point(141, 41);
            this.txtACIdleTimeout.Name = "txtACIdleTimeout";
            this.txtACIdleTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtACIdleTimeout.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 19);
            this.label2.Text = "AC Idle Timeout:";
            // 
            // txtBatteryIdleTimeout
            // 
            this.txtBatteryIdleTimeout.Enabled = false;
            this.txtBatteryIdleTimeout.Location = new System.Drawing.Point(141, 10);
            this.txtBatteryIdleTimeout.Name = "txtBatteryIdleTimeout";
            this.txtBatteryIdleTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtBatteryIdleTimeout.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.Text = "Bat. Idle Timeout:";
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(81, 168);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(72, 26);
            this.btnGetInfo.TabIndex = 29;
            this.btnGetInfo.Text = "Get Info";
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // frmTimerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnGetInfo);
            this.Controls.Add(this.txtBatteryResumeTimeout);
            this.Controls.Add(this.txtACResumeTimeout);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWakeIdleTimeout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtACIdleTimeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBatteryIdleTimeout);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "frmTimerInfo";
            this.Text = "frmTimerInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBatteryResumeTimeout;
        private System.Windows.Forms.TextBox txtACResumeTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWakeIdleTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtACIdleTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBatteryIdleTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetInfo;
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