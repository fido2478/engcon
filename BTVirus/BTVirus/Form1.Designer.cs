namespace BTVirus
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
			this.lstResults = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboAttackType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnStartStop = new System.Windows.Forms.Button();
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
			// lstResults
			// 
			this.lstResults.Location = new System.Drawing.Point(25, 65);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(180, 72);
			this.lstResults.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 19);
			this.label1.Text = "Attack:";
			// 
			// cboAttackType
			// 
			this.cboAttackType.Items.Add("CommWarrior");
			this.cboAttackType.Items.Add("Cabir");
			this.cboAttackType.Items.Add("Mabir");
			this.cboAttackType.Items.Add("Lasco");
			this.cboAttackType.Location = new System.Drawing.Point(57, 9);
			this.cboAttackType.Name = "cboAttackType";
			this.cboAttackType.Size = new System.Drawing.Size(148, 22);
			this.cboAttackType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 19);
			this.label2.Text = "Output:";
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(83, 143);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(68, 32);
			this.btnStartStop.TabIndex = 4;
			this.btnStartStop.Text = "Start";
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboAttackType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstResults);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "BTVirus";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAttackType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartStop;
    }
}

