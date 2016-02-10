namespace SystemUtils
{
    partial class frmRegInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegKey = new System.Windows.Forms.TextBox();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.treRegInfo = new System.Windows.Forms.TreeView();
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(100, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.Text = "Key:";
            // 
            // txtRegKey
            // 
            this.txtRegKey.Location = new System.Drawing.Point(16, 23);
            this.txtRegKey.Name = "txtRegKey";
            this.txtRegKey.Size = new System.Drawing.Size(200, 21);
            this.txtRegKey.TabIndex = 1;
            this.txtRegKey.Text = "System\\CurrentControlSet\\Control\\Power\\State";
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(85, 53);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(65, 25);
            this.btnGetInfo.TabIndex = 3;
            this.btnGetInfo.Text = "Get Info";
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // treRegInfo
            // 
            this.treRegInfo.Location = new System.Drawing.Point(0, 84);
            this.treRegInfo.Name = "treRegInfo";
            this.treRegInfo.Size = new System.Drawing.Size(237, 181);
            this.treRegInfo.TabIndex = 4;
            // 
            // frmRegInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.treRegInfo);
            this.Controls.Add(this.btnGetInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRegKey);
            this.Menu = this.mainMenu1;
            this.Name = "frmRegInfo";
            this.Text = "frmRegInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegKey;
        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.TreeView treRegInfo;
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