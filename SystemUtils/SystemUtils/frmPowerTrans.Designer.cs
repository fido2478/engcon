namespace SystemUtils
{
    partial class frmPowerTrans
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
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.btnStartStop = new System.Windows.Forms.Button();
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
            this.menuItem3.Text = "Power Drain";
            this.menuItem3.Click += new System.EventHandler(this.ShowPowerDrain);
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
            // lstInfo
            // 
            this.lstInfo.Location = new System.Drawing.Point(0, 0);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(240, 156);
            this.lstInfo.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(87, 187);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(60, 31);
            this.btnStartStop.TabIndex = 1;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // frmPowerTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.lstInfo);
            this.Menu = this.mainMenu1;
            this.Name = "frmPowerTrans";
            this.Text = "PowerTrans";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstInfo;
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
    }
}