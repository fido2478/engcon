using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;


namespace Monitor
{
    #region Hardware Button Stuff
    // global enums for hardware button stuff
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        Modkeyup = 0x1000,
    }

    public enum KeysHardware : int
    {
        Hardware1 = 193,
        Hardware2 = 194,
        Hardware3 = 195,
        Hardware4 = 196,
        Hardware5 = 197
    }
    #endregion

    public partial class Form1 : Form
    {
        #region variables and stuff

        // variables for Displacement calculation
        private int UpperBound = 180;
        private int LowerBound = 70;
        private int SampleTime = 0;
		private int Displacement = 200;
        private int MonitorType = 0;    // 0 for B+Disp, 1 for B+rel
		private Random RandNum;
		static private int SampleCount = 0;

        // stuff for sample comparison
        private int CurSample = 0;
        private double ThreshHold = 0.1;
		private int[] ExpectedValues = { 180, 250 };
		private uint prevTickCount;
		private uint curTickCount;

        // timer stuff
        Timer LongTimer;
        Timer ShortTimer;

        // instance of Measurement class
        Measurement M;
        #endregion

        // stuff for hardware button detection
        myMessageWindow messageWindow;

		[DllImport("coredll")]
		private static extern uint GetTickCount();

        public Form1()
        {
            InitializeComponent();

            // Stuff for the hardware button message handling
            this.messageWindow = new myMessageWindow(this);
            RegisterHKeys.RegisterRecordKey(this.messageWindow.Hwnd);

            // set up timer
            LongTimer = new Timer();
			LongTimer.Tick += new EventHandler(BaseTime);
            ShortTimer = new Timer();
			ShortTimer.Tick += new EventHandler(TakeSample);

			// set up random number generator
			RandNum = new Random();

            // display default values into the text boxes
            txtUB.Text = UpperBound.ToString();
            txtLB.Text = LowerBound.ToString();
			txtDisplacement.Text = Displacement.ToString();

            // set default type to base + displacement
            cboMonitorType.SelectedIndex = 0;

            // instantiate measurement class
            M = new Measurement();

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        // Function to handle the button presses after
        // the message handling
        public void ButtonPressed(int button)
        {
            switch (button)
            {
                case (int)KeysHardware.Hardware1:
                    // simulate the start button being pressed
                    MessageBox.Show("Pressed");
                    //MessageBox.Show("Button 1 pressed!");
                    break;
                case (int)KeysHardware.Hardware2:
                    //MessageBox.Show("Button 2 pressed!");
                    break;
                case (int)KeysHardware.Hardware3:
                    //MessageBox.Show("Button 3 pressed!");
                    break;
                case (int)KeysHardware.Hardware4:
                    //MessageBox.Show("Button 4 pressed!");
                    break;
            }

        }

		// This function handles whatever should happen at the base times.
		// If we're in Base+Displacement then this function will calculate
		// the next sample time and update the ShortTimer.  If we're in
		// Base+Relative then this function isn't used.
		public void BaseTime(object obj, EventArgs e)
		{
			// reset short timer to tick at the correct sample time
			SampleTime = RandNum.Next(LowerBound, UpperBound);
			ShortTimer.Enabled = false;
			ShortTimer.Interval = SampleTime;
			ShortTimer.Enabled = true;
			SampleCount = 0;
		}

		// This function handles whatever should happen at sample time.
		// If we're in Base+displacement then this function only takes measurements
		// If we're in Base+relative this function will take a sample, 
		// raise a flag if appropriate and then calculate the next sample time.
		public void TakeSample(object obj, EventArgs e)
		{
			if (SampleCount == 0)
			{
				// take measurement and check for out of bounds
				CurSample = M.TakeMeasurement();
				curTickCount = GetTickCount();
				//string res = CurSample.ToString() + " (" + (curTickCount - prevTickCount).ToString() + ")";
				//AddResult(res);

				foreach (int Avg in ExpectedValues)
				{
					if ((CurSample / Avg - 1) > ThreshHold)
					{
						//raise flag
						AddResult("Threshhold broken.");
					}
				}
				SampleCount++;
			}

			if (MonitorType == 1)
			{
				// calculate next sample time
				SampleTime = RandNum.Next(LowerBound, UpperBound);
				ShortTimer.Enabled = false;
				ShortTimer.Interval = SampleTime;
				ShortTimer.Enabled = true;
				SampleCount = 0;
			}
			else if (MonitorType == 0)
			{
				// prevent more measurements until the next time
				ShortTimer.Enabled = false;
			}

			prevTickCount = curTickCount;

		}

		// add object to listbox and make sure that listbox
		// scrolls down as items are added
		private void AddResult(object obj)
		{
			int Index = lstResults.Items.Add(obj.ToString());
			lstResults.SelectedIndex = Index;
			lstResults.SelectedIndex = -1;

		}


        private void Form1_Load(object sender, EventArgs e)
        {

        }

		private void btnStartStop_Click(object sender, EventArgs e)
		{
			if (btnStartStop.Text.Equals("Start"))
			{
				// update UB and LB
				UpperBound = System.Convert.ToInt32(txtUB.Text);
				LowerBound = System.Convert.ToInt32(txtLB.Text);
				Displacement = System.Convert.ToInt32(txtDisplacement.Text);

				// determine which type of monitoring we want
				MonitorType = cboMonitorType.SelectedIndex;

				switch (MonitorType)
				{
					case 0:     // base + disp
						// set up timers
						LongTimer.Interval = Displacement;
						LongTimer.Enabled = true;
						break;

					case 1:		// base + rel
						// set up timers
						SampleTime = RandNum.Next(LowerBound, UpperBound);
						ShortTimer.Interval = SampleTime;
						ShortTimer.Enabled = true;
						break;
				}

				btnStartStop.Text = "Stop";
			}
			else if (btnStartStop.Text.Equals("Stop"))
			{
				// stop monitoring
				LongTimer.Enabled = false;
				ShortTimer.Enabled = false;

				btnStartStop.Text = "Start";
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			lstResults.Items.Clear();
		}

    }

    #region Hardware button stuff
    // ***************  Classes for the hardware button detection *********
    // ***************                                            *********

    public class myMessageWindow : MessageWindow
    {

        public const int WM_HOTKEY = 0x0312;
        Form1 example;
        public myMessageWindow(Form1 example)
        {
            this.example = example;
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case WM_HOTKEY:
                    example.ButtonPressed(msg.WParam.ToInt32());
                    return;
            }
            base.WndProc(ref msg);
        }
    }

    public class RegisterHKeys
    {
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
        IntPtr hWnd, // handle to window
        int id, // hot key identifier
        KeyModifiers Modifiers, // key-modifier options
        int key //virtual-key code
        );

        [DllImport("coredll.dll")]
        private static extern bool UnregisterFunc1(KeyModifiers
        modifiers, int keyID);

        public static void RegisterRecordKey(IntPtr hWnd)
        {
            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware1);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware1, KeyModifiers.Windows, (int)KeysHardware.Hardware1);


            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware2);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware2, KeyModifiers.Windows, (int)KeysHardware.Hardware2);

            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware3);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware3, KeyModifiers.Windows, (int)KeysHardware.Hardware3);

            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware4);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware4, KeyModifiers.Windows, (int)KeysHardware.Hardware4);
        }
    }

    #endregion

    #region Measurement class
    public class Measurement
    {
        // dll import for measurement function
        [DllImport("coredll")]
        private static extern uint GetSystemPowerStatusEx2(out SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus,
            uint dwLen, bool fUpdate);

        // struct for power measurements
        private struct SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
            public uint BatteryVoltage;
            public int  BatteryCurrent;
            public uint BatteryAverageCurrent;
            public uint BatteryAverageInterval;
            public uint BatterymAHourConsumed;
            public uint BatteryTemperature;
            public uint BackupBatteryVoltage;
            public byte BatteryChemistry;
        }

        private SYSTEM_POWER_STATUS_EX2 status;
        private uint DataSize = 0;

        public Measurement()
        {
            // prepare for measuring
            status = new SYSTEM_POWER_STATUS_EX2();
            DataSize = (uint)Marshal.SizeOf(status);


        }

        // this function takes the measurement
        public int TakeMeasurement()
        {
            uint result = GetSystemPowerStatusEx2(out status, DataSize, true);
            if (DataSize == result)
                return status.BatteryCurrent;
            else
                return 0;
        }
    }

    #endregion

}