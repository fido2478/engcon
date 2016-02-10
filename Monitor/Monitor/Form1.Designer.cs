namespace Monitor
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
			this.txtUB = new System.Windows.Forms.TextBox();
			this.txtLB = new System.Windows.Forms.TextBox();
			this.txtDisplacement = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnStartStop = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.cboMonitorType = new System.Windows.Forms.ComboBox();
			this.lstResults = new System.Windows.Forms.ListBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Exit";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// txtUB
			// 
			this.txtUB.Location = new System.Drawing.Point(98, 14);
			this.txtUB.Name = "txtUB";
			this.txtUB.Size = new System.Drawing.Size(70, 21);
			this.txtUB.TabIndex = 0;
			// 
			// txtLB
			// 
			this.txtLB.Location = new System.Drawing.Point(98, 41);
			this.txtLB.Name = "txtLB";
			this.txtLB.Size = new System.Drawing.Size(70, 21);
			this.txtLB.TabIndex = 1;
			// 
			// txtDisplacement
			// 
			this.txtDisplacement.Location = new System.Drawing.Point(98, 68);
			this.txtDisplacement.Name = "txtDisplacement";
			this.txtDisplacement.Size = new System.Drawing.Size(70, 21);
			this.txtDisplacement.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(50, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 20);
			this.label1.Text = "UB:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(50, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 20);
			this.label2.Text = "LB:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(50, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 20);
			this.label3.Text = "Disp:";
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(78, 215);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(65, 25);
			this.btnStartStop.TabIndex = 8;
			this.btnStartStop.Text = "Start";
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(42, 98);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 19);
			this.label4.Text = "Type:";
			// 
			// cboMonitorType
			// 
			this.cboMonitorType.Items.Add("B+Disp");
			this.cboMonitorType.Items.Add("B+Rel");
			this.cboMonitorType.Location = new System.Drawing.Point(98, 95);
			this.cboMonitorType.Name = "cboMonitorType";
			this.cboMonitorType.Size = new System.Drawing.Size(70, 22);
			this.cboMonitorType.TabIndex = 13;
			// 
			// lstResults
			// 
			this.lstResults.Location = new System.Drawing.Point(36, 130);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(157, 58);
			this.lstResults.TabIndex = 18;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(199, 144);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(33, 20);
			this.btnClear.TabIndex = 23;
			this.btnClear.Text = "Clr";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.lstResults);
			this.Controls.Add(this.cboMonitorType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDisplacement);
			this.Controls.Add(this.txtLB);
			this.Controls.Add(this.txtUB);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Monitor";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.TextBox txtUB;
        private System.Windows.Forms.TextBox txtLB;
        private System.Windows.Forms.TextBox txtDisplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboMonitorType;
		private System.Windows.Forms.ListBox lstResults;
		private System.Windows.Forms.Button btnClear;
    }
}

