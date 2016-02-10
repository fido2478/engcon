namespace SystemUtils
{
    partial class frmPowerInfo
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
            this.btnSetSystemState = new System.Windows.Forms.Button();
            this.radD2 = new System.Windows.Forms.RadioButton();
            this.radD4 = new System.Windows.Forms.RadioButton();
            this.radD3 = new System.Windows.Forms.RadioButton();
            this.btnListDevices = new System.Windows.Forms.Button();
            this.lstDeviceList = new System.Windows.Forms.ListBox();
            this.radD1 = new System.Windows.Forms.RadioButton();
            this.radD0 = new System.Windows.Forms.RadioButton();
            this.txtSystemInfo = new System.Windows.Forms.TextBox();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDeviceInfo = new System.Windows.Forms.TextBox();
            this.btnSetDevicePower = new System.Windows.Forms.Button();
            this.btnGetSystemState = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetDevicePower = new System.Windows.Forms.Button();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.menuItem2.Text = "Power Trans";
            this.menuItem2.Click += new System.EventHandler(this.ShowPowerTrans);
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
            this.menuItem6.Click += new System.EventHandler(this.ShowTimerIinfo);
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
            // btnSetSystemState
            // 
            this.btnSetSystemState.Location = new System.Drawing.Point(141, 242);
            this.btnSetSystemState.Name = "btnSetSystemState";
            this.btnSetSystemState.Size = new System.Drawing.Size(96, 22);
            this.btnSetSystemState.TabIndex = 43;
            this.btnSetSystemState.Text = "Set System PS";
            this.btnSetSystemState.Click += new System.EventHandler(this.btnSetSystemState_Click);
            // 
            // radD2
            // 
            this.radD2.Location = new System.Drawing.Point(155, 100);
            this.radD2.Name = "radD2";
            this.radD2.Size = new System.Drawing.Size(39, 16);
            this.radD2.TabIndex = 42;
            this.radD2.Text = "D2";
            // 
            // radD4
            // 
            this.radD4.Location = new System.Drawing.Point(199, 78);
            this.radD4.Name = "radD4";
            this.radD4.Size = new System.Drawing.Size(39, 16);
            this.radD4.TabIndex = 41;
            this.radD4.Text = "D4";
            // 
            // radD3
            // 
            this.radD3.Location = new System.Drawing.Point(199, 56);
            this.radD3.Name = "radD3";
            this.radD3.Size = new System.Drawing.Size(39, 16);
            this.radD3.TabIndex = 40;
            this.radD3.Text = "D3";
            // 
            // btnListDevices
            // 
            this.btnListDevices.Location = new System.Drawing.Point(155, 165);
            this.btnListDevices.Name = "btnListDevices";
            this.btnListDevices.Size = new System.Drawing.Size(69, 21);
            this.btnListDevices.TabIndex = 39;
            this.btnListDevices.Text = "List";
            this.btnListDevices.Click += new System.EventHandler(this.btnListDevices_Click);
            // 
            // lstDeviceList
            // 
            this.lstDeviceList.Location = new System.Drawing.Point(5, 100);
            this.lstDeviceList.Name = "lstDeviceList";
            this.lstDeviceList.Size = new System.Drawing.Size(130, 86);
            this.lstDeviceList.TabIndex = 38;
            this.lstDeviceList.SelectedIndexChanged += new System.EventHandler(this.lstDeviceList_SelectedIndexChanged);
            // 
            // radD1
            // 
            this.radD1.Location = new System.Drawing.Point(155, 78);
            this.radD1.Name = "radD1";
            this.radD1.Size = new System.Drawing.Size(39, 16);
            this.radD1.TabIndex = 37;
            this.radD1.Text = "D1";
            // 
            // radD0
            // 
            this.radD0.Checked = true;
            this.radD0.Location = new System.Drawing.Point(155, 56);
            this.radD0.Name = "radD0";
            this.radD0.Size = new System.Drawing.Size(39, 16);
            this.radD0.TabIndex = 36;
            this.radD0.Text = "On";
            // 
            // txtSystemInfo
            // 
            this.txtSystemInfo.Enabled = false;
            this.txtSystemInfo.Location = new System.Drawing.Point(2, 209);
            this.txtSystemInfo.Multiline = true;
            this.txtSystemInfo.Name = "txtSystemInfo";
            this.txtSystemInfo.Size = new System.Drawing.Size(133, 56);
            this.txtSystemInfo.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.Text = "System Info:";
            // 
            // txtDeviceInfo
            // 
            this.txtDeviceInfo.Enabled = false;
            this.txtDeviceInfo.Location = new System.Drawing.Point(5, 49);
            this.txtDeviceInfo.Multiline = true;
            this.txtDeviceInfo.Name = "txtDeviceInfo";
            this.txtDeviceInfo.Size = new System.Drawing.Size(130, 45);
            this.txtDeviceInfo.TabIndex = 34;
            // 
            // btnSetDevicePower
            // 
            this.btnSetDevicePower.Location = new System.Drawing.Point(155, 30);
            this.btnSetDevicePower.Name = "btnSetDevicePower";
            this.btnSetDevicePower.Size = new System.Drawing.Size(69, 20);
            this.btnSetDevicePower.TabIndex = 33;
            this.btnSetDevicePower.Text = "Set Power";
            this.btnSetDevicePower.Click += new System.EventHandler(this.btnSetDevicePower_Click);
            // 
            // btnGetSystemState
            // 
            this.btnGetSystemState.Location = new System.Drawing.Point(141, 209);
            this.btnGetSystemState.Name = "btnGetSystemState";
            this.btnGetSystemState.Size = new System.Drawing.Size(98, 25);
            this.btnGetSystemState.TabIndex = 32;
            this.btnGetSystemState.Text = "Get System PS";
            this.btnGetSystemState.Click += new System.EventHandler(this.btnGetSystemState_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.Text = "Device Info:";
            // 
            // btnGetDevicePower
            // 
            this.btnGetDevicePower.Location = new System.Drawing.Point(155, 3);
            this.btnGetDevicePower.Name = "btnGetDevicePower";
            this.btnGetDevicePower.Size = new System.Drawing.Size(70, 21);
            this.btnGetDevicePower.TabIndex = 31;
            this.btnGetDevicePower.Text = "Get Power";
            this.btnGetDevicePower.Click += new System.EventHandler(this.btnGetDevicePower_Click);
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(56, 3);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(71, 21);
            this.txtDeviceName.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 18);
            this.label1.Text = "Device:";
            // 
            // frmPowerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnSetSystemState);
            this.Controls.Add(this.radD2);
            this.Controls.Add(this.radD4);
            this.Controls.Add(this.radD3);
            this.Controls.Add(this.btnListDevices);
            this.Controls.Add(this.lstDeviceList);
            this.Controls.Add(this.radD1);
            this.Controls.Add(this.radD0);
            this.Controls.Add(this.txtSystemInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDeviceInfo);
            this.Controls.Add(this.btnSetDevicePower);
            this.Controls.Add(this.btnGetSystemState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetDevicePower);
            this.Controls.Add(this.txtDeviceName);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "frmPowerInfo";
            this.Text = "frmPowerInfo";
            this.Load += new System.EventHandler(this.frmPowerInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSetSystemState;
        private System.Windows.Forms.RadioButton radD2;
        private System.Windows.Forms.RadioButton radD4;
        private System.Windows.Forms.RadioButton radD3;
        private System.Windows.Forms.Button btnListDevices;
        private System.Windows.Forms.ListBox lstDeviceList;
        private System.Windows.Forms.RadioButton radD1;
        private System.Windows.Forms.RadioButton radD0;
        private System.Windows.Forms.TextBox txtSystemInfo;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeviceInfo;
        private System.Windows.Forms.Button btnSetDevicePower;
        private System.Windows.Forms.Button btnGetSystemState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetDevicePower;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label label1;
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