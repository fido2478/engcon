namespace BatInfoLight
{
    partial class Form1
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
			this.mnuSaveToFile = new System.Windows.Forms.MenuItem();
			this.mnuSaveToDatabase = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuDataExport = new System.Windows.Forms.MenuItem();
			this.mnuDataImport = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuClearMem = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.mnuMatch = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInterval = new System.Windows.Forms.TextBox();
			this.btnStartStop = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTotalTime = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtRuns = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtState = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtBaseName = new System.Windows.Forms.TextBox();
			this.txtTimerType = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSleepTime = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.mnuMatch);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.mnuClearMem);
			this.menuItem1.MenuItems.Add(this.mnuExit);
			this.menuItem1.Text = "Menu";
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.mnuSaveToFile);
			this.menuItem2.MenuItems.Add(this.mnuSaveToDatabase);
			this.menuItem2.Text = "Save";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// mnuSaveToFile
			// 
			this.mnuSaveToFile.Text = "To File";
			this.mnuSaveToFile.Click += new System.EventHandler(this.mnuSaveToFile_Click);
			// 
			// mnuSaveToDatabase
			// 
			this.mnuSaveToDatabase.Text = "To Database";
			this.mnuSaveToDatabase.Click += new System.EventHandler(this.mnuSaveToDatabase_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.mnuDataExport);
			this.menuItem4.MenuItems.Add(this.mnuDataImport);
			this.menuItem4.Text = "Database";
			// 
			// mnuDataExport
			// 
			this.mnuDataExport.Text = "Export";
			this.mnuDataExport.Click += new System.EventHandler(this.mnuDataExport_Click);
			// 
			// mnuDataImport
			// 
			this.mnuDataImport.Text = "Import";
			this.mnuDataImport.Click += new System.EventHandler(this.mnuDataImport_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "-";
			// 
			// mnuClearMem
			// 
			this.mnuClearMem.Text = "Clear Mem";
			this.mnuClearMem.Click += new System.EventHandler(this.mnuClearMem_Click);
			// 
			// mnuExit
			// 
			this.mnuExit.Text = "Exit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuMatch
			// 
			this.mnuMatch.Text = "Match";
			this.mnuMatch.Click += new System.EventHandler(this.mnuMatch_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 20);
			this.label1.Text = "Interval (ms):";
			// 
			// txtInterval
			// 
			this.txtInterval.Location = new System.Drawing.Point(83, 37);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new System.Drawing.Size(57, 21);
			this.txtInterval.TabIndex = 1;
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(83, 153);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(66, 26);
			this.btnStartStop.TabIndex = 2;
			this.btnStartStop.Text = "Start";
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 19);
			this.label2.Text = "Total time (s):";
			// 
			// txtTotalTime
			// 
			this.txtTotalTime.Location = new System.Drawing.Point(83, 9);
			this.txtTotalTime.Name = "txtTotalTime";
			this.txtTotalTime.Size = new System.Drawing.Size(57, 21);
			this.txtTotalTime.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 20);
			this.label4.Text = "Runs:";
			// 
			// txtRuns
			// 
			this.txtRuns.Location = new System.Drawing.Point(83, 65);
			this.txtRuns.Name = "txtRuns";
			this.txtRuns.Size = new System.Drawing.Size(57, 21);
			this.txtRuns.TabIndex = 20;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 95);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 20);
			this.label5.Text = "State:";
			// 
			// txtState
			// 
			this.txtState.Location = new System.Drawing.Point(83, 95);
			this.txtState.Name = "txtState";
			this.txtState.Size = new System.Drawing.Size(57, 21);
			this.txtState.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 125);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 22);
			this.label3.Text = "Log Name:";
			// 
			// txtBaseName
			// 
			this.txtBaseName.Location = new System.Drawing.Point(83, 126);
			this.txtBaseName.Name = "txtBaseName";
			this.txtBaseName.Size = new System.Drawing.Size(124, 21);
			this.txtBaseName.TabIndex = 30;
			// 
			// txtTimerType
			// 
			this.txtTimerType.Location = new System.Drawing.Point(150, 37);
			this.txtTimerType.Name = "txtTimerType";
			this.txtTimerType.Size = new System.Drawing.Size(62, 21);
			this.txtTimerType.TabIndex = 36;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(148, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.Text = "Type:";
			// 
			// txtSleepTime
			// 
			this.txtSleepTime.Location = new System.Drawing.Point(150, 92);
			this.txtSleepTime.Name = "txtSleepTime";
			this.txtSleepTime.Size = new System.Drawing.Size(57, 21);
			this.txtSleepTime.TabIndex = 42;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(150, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(46, 17);
			this.label7.Text = "Sleep:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtSleepTime);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtTimerType);
			this.Controls.Add(this.txtBaseName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtState);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtRuns);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtTotalTime);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.txtInterval);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Battery Info";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterval;
		private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRuns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBaseName;
        private System.Windows.Forms.TextBox txtTimerType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSleepTime;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuClearMem;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem mnuSaveToFile;
		private System.Windows.Forms.MenuItem mnuSaveToDatabase;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuDataExport;
		private System.Windows.Forms.MenuItem mnuDataImport;
		private System.Windows.Forms.MenuItem mnuMatch;
    }
}

