using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace BatInfoLight
{

    
    public partial class Form1 : Form
    {
        #region member variables, constants, structs and API imports

        // create queue for keeping track of near-past measurements
        public class Measurement
        {
            public int Current;
            public uint Time;
			public uint Temp;
			public byte Life;

			public Measurement()
			{
				Current = 0;
				Time = 0;
				Temp = 0;
				Life = 0;
			}

			public Measurement (int current)
			{
				Current = current;
				Time = 0;
				Temp = 0;
				Life = 0;
			}

        };

		// filter fft stuff
		private int AvgLookAhead = 5;
		private int CompLookAhead = 5;
		private int CompThreshold = 10;
		double[] values;
		double[] avg;
		double[] compressed;
		double[] fft;

		// Stuff for the database
		Database dataBase;
		private string DatabaseName = "/Temp/database.txt";

        // Stuff for the power measurements
        private const int QueueCapacity = 500;
        private int MaxMeasurements = 500000;
        //private Queue<int> Measurements = new Queue<int>(QueueCapacity);
		private Queue<Measurement> Measurements = new Queue<Measurement>(QueueCapacity);
		private Measurement M;
        private int NumStarts = 0;
        private int MeasureCount = 0;

        // stuff for the energy measurements
		private MeasureClass Measurer = new MeasureClass();

        private int TimerType = 2;  // 0 = timer, 1=while loop, 2=child thread

        // create variables for timer use
        private int Interval = 100;             // default to 1.5 seconds
        private int TotalTime = 20000;          // default to 20 seconds
		private int Range = 40;
        private int NumRuns = 1;               // default to 1 run
        private uint StartTime = 0;
        private string BaseName = "PowerLog";   // default to PowerLog
        private string OldState = "";
        private string NewState = "";
        System.Windows.Forms.Timer LongTimer;

        // for system.timers.timer use
        private System.Threading.Timer ThreadTimer;
        private AutoResetEvent autoEvent;
        private TimerCallback cb;
        private System.Threading.Timer LongThreadTimer;
        private AutoResetEvent autoEvent1;
        private TimerCallback LongCb;
        delegate void buttonDelegate();

        // variables for the loop/timer use
        private static bool ContinueLoop = false;
        private int SleepTime = 0;

        // variables for thread use
        Thread MeasureThread;

        // For setting thread priority
        private bool SetPriority = false;
        private int Priority = 248;

        // power manager constants
        private const uint POWER_FORCE =             0x00001000;
        private const uint PPN_UNATTENDEDMODE =      0x0003;
        private const uint POWER_STATE_ON =          0x00010000;     // on state
        private const uint POWER_STATE_OFF =         0x00020000;     // no power, full off
        private const uint POWER_STATE_CRITICAL =    0x00040000;     // critical off
        private const uint POWER_STATE_BOOT =        0x00080000;     // boot state
        private const uint POWER_STATE_IDLE =        0x00100000;     // idle state
        private const uint POWER_STATE_SUSPEND =     0x00200000;     // suspend state
        private const uint POWER_STATE_UNATTENDED =  0x00400000;     // Unattended state.
        private const uint POWER_STATE_RESET =       0x00800000;     // reset state
        private const uint POWER_STATE_USERIDLE =    0x01000000;     // user idle state
        private const uint POWER_STATE_BACKLIGHTON = 0x02000000;     // device scree backlight on
        private const uint POWER_STATE_PASSWORD =    0x10000000;     // This state is password protected.

		// the API imports for retrieving power information and system time
		#region API imports
        [DllImport("coredll")]
        public static extern uint GetTickCount();

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetSystemPowerState(
            string pBuffer, int Length, ref int Flags);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern uint SetSystemPowerState(
            string psState, uint StateFlags, uint Options);

        [DllImport("coredll.dll", SetLastError = true)]    
        private static extern bool PowerPolicyNotify(
            int Message, int Data);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool CeSetThreadPriority(
            IntPtr hThread, int nPriority);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int CeGetThreadPriority(
            IntPtr hThread);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool CeSetThreadQuantum(
            IntPtr hThread, int dwTime);

        [DllImport("thread_handle.dll")]
        private static extern IntPtr GetCurrentThreadHandle();
		#endregion

		// for hardware button detection
        myMessageWindow messageWindow;

        #endregion

        public Form1()
        {
            InitializeComponent();

            // attempt to set thread priority
            Thread.CurrentThread.Priority = ThreadPriority.Normal;  // above.normal
            //IntPtr handle = GetCurrentThreadHandle();
            //bool result = CeSetThreadPriority(handle, Priority);
            //if (!result)
            //    MessageBox.Show("fail");

            // Stuff for the hardware button message handling
            this.messageWindow = new myMessageWindow(this);
            RegisterHKeys.RegisterRecordKey(this.messageWindow.Hwnd);

            // set up threading timers
            cb = new TimerCallback(TimerTick);
            autoEvent = new AutoResetEvent(false);
            ThreadTimer = new System.Threading.Timer(cb, autoEvent, Timeout.Infinite, Interval);
            
            //LongCb = new TimerCallback(MeasureDone);
            //autoEvent1 = new AutoResetEvent(false);
            //LongThreadTimer = new System.Threading.Timer(LongCb, autoEvent1, Timeout.Infinite, TotalTime);
            LongTimer = new System.Windows.Forms.Timer();
            LongTimer.Interval = TotalTime;
            LongTimer.Tick += new EventHandler(MeasureDone);
            LongTimer.Enabled = false;

            // write default timer values to the text boxes
            txtInterval.Text = Interval.ToString();
            txtTotalTime.Text = (TotalTime/1000).ToString();
            txtRuns.Text = NumRuns.ToString();
            txtBaseName.Text = BaseName;
            txtTimerType.Text = TimerType.ToString();
            txtSleepTime.Text = SleepTime.ToString();

			// set up database and auto-import
			dataBase = new Database();
			dataBase.ReadTableFromFile(DatabaseName);

        }

		private void StartMeasuring()
		{
			// start the long timer
			LongTimer.Enabled = true;

			switch (TimerType)
			{
				case 0:
					// Start the Timers
					StartTimers();
					break;
				case 1:
					// start loop
					StartLoop();
					break;
				case 2:
					// start thread
					StartThread();
					break;
			}


		}

		private void StopMeasuring()
		{
			switch (TimerType)
			{
				case 0:
					// Start the Timers
					StopTimers();
					break;
				case 1:
					// start loop
					StopLoop();
					break;
				case 2:
					// start thread
					StopThread();
					break;
			}
		}

        private void StartTimers()
        {
            // get the current timer values
            Interval = System.Convert.ToInt32(txtInterval.Text);
           
            // insert a '0' measurement to signal starting new run
			//Measurements.Enqueue(0);
			//M = new Measurement(0);
			M = Measurer.TakeMeasurement(true);
			M.Current = 0;
			Measurements.Enqueue(M);
			Measurer.ResetTick(GetTickCount());

            // decrement number of runs so we can keep track
            NumRuns--;
            NumStarts++;

            // reset the interval and allow the timer object to start
            ThreadTimer.Change(Interval - 35, Interval - 35);
        }

        private void StartLoop()
        {
            ContinueLoop = true;

            // insert a '0' measurement to signal starting new run
            //Measurements.Enqueue(0);
			//M = new Measurement(0);
			M = Measurer.TakeMeasurement(true);
			M.Current = 0;
			Measurements.Enqueue(M);
			Measurer.ResetTick(GetTickCount());
            MeasureThread = new Thread(new ThreadStart(LoopMeasure));
            MeasureThread.IsBackground = true;
            MeasureThread.Start();
            NumRuns--;
            NumStarts++;
        }

        private void StartThread()
        {
            ContinueLoop = true;

            // insert a '0' measurement to signal starting new run
            //Measurements.Enqueue(0);
			//M = new Measurement(0);
			M = Measurer.TakeMeasurement(true);
			M.Current = 0;
			Measurements.Enqueue(M);
			Measurer.ResetTick(GetTickCount());
            MeasureThread = new Thread(new ThreadStart(ThreadMeasure));
            MeasureThread.IsBackground = true;
            MeasureThread.Start();
            NumRuns--;
            NumStarts++;
        }

        private void StopTimers()
        {
            // change timer to be inactive
            ThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);

        }

        private void StopLoop()
        {
            ContinueLoop = false;
            MeasureThread.Abort();
        }

        private void StopThread()
        {
            ContinueLoop = false;
            MeasureThread.Abort();
        }

        // This function is called by the long timer when the entire
        // measuring period is up.  It will stop the timers and
        // start another run if any are remaining.
        private void MeasureDone(object src, EventArgs e)
        {
            // change timer to be inactive
            //LongThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            LongTimer.Enabled = false;
            SetPriority = false;

            // stop appropriate timing method
			StopMeasuring();

            // if we have another run left, start again
            if (NumRuns > 0)
            {
                StartMeasuring();
            }
            else
            {
                // change system power state back if needed
                if (!NewState.Equals(OldState) && !NewState.Equals(""))
                {
                    bool success = SetSysPowerState(OldState);
                    if (!success)
                    {
                        // bad stuff
                    }
                    else
                        NewState = "";

                }

                // display number of measurements
                MessageBox.Show(MeasureCount.ToString());

                // since this method runs on a separate thread than the form
                // we need to use a delegate method to reset the button text to "Start"
                buttonDelegate d = new buttonDelegate(ResetButtonText);
                btnStartStop.Invoke(d);
            }
        }

        private void ThreadMeasure()
        {
            if (!SetPriority)
            {
                // attempt to set the thread priority to normal
                Thread.CurrentThread.Priority = ThreadPriority.Normal;  // above.normal
                //IntPtr handle = GetCurrentThreadHandle();
                //bool result = CeSetThreadPriority(handle, Priority);
                //if (!result)
                //    MessageBox.Show("fail");
                SetPriority = true;
            }

            // run until parent thread stops
            while (ContinueLoop)
            {
                Thread.Sleep(Interval - 50);
                TakeMeasurements();
            }
            //MessageBox.Show("Threaddone");
        }

        private void LoopMeasure()
        {

            if (!SetPriority)
            {
                // attempt to set the thread priority to above normal
                Thread.CurrentThread.Priority = ThreadPriority.Normal;  // normal
                //IntPtr handle = GetCurrentThreadHandle();
                //bool result = CeSetThreadPriority(handle, Priority);
                //if (!result)
                //    MessageBox.Show("fail");
                SetPriority = true;
            }

            uint Tick, prevTick = GetTickCount();
            
            // run loop until stopped
            while (ContinueLoop)
            {
                Tick = GetTickCount();
				uint Diff = Tick - prevTick;
                if (Math.Abs(Diff - Interval) < Range)
                {
                    TakeMeasurements();
                    
                    prevTick = Tick;
                }
                Thread.Sleep(SleepTime);
                Application.DoEvents();
            }
            MessageBox.Show("Loopdone");
        }

        private void TimerTick(object src)
        {

            TakeMeasurements();

        }

        private void TakeMeasurements()
        {
            /*
            if (!SetPriority)
            {
                // attempt to set the thread priority to above normal
                IntPtr handle = GetCurrentThreadHandle();
                bool result = CeSetThreadPriority(handle, Priority);
                if (!result)
                    MessageBox.Show("fail");
                SetPriority = true;
            }
            */

            // just take one battery measurement
			//int m = Measurer.TakeMeasurement();
            //if (m!= 0)
            //    Measurements.Enqueue(m);
			M = Measurer.TakeMeasurement();
			if (M.Current != 0)
				Measurements.Enqueue(M);

            MeasureCount++;
        }

        // this function just provides a method to reset the button text
        // so we can call it using a delegate from other threads
        private void ResetButtonText()
        {
            btnStartStop.Text = "Start";
        }

        // This function will return the name of the current system
        // power state.
        private string GetSysPowerState()
        {
            // create string buffer to receive power state name
            string StateName = "";
            for (int i = 0; i < 255; i++)
            {
                StateName += " ";
            }
            int Flags = 0;
            int result = GetSystemPowerState(StateName, StateName.Length, ref Flags);

            if (result == 0)
            {
                //display the state name (w/o trailing null character)
                StateName = StateName.Substring(0, StateName.IndexOf(' ') - 1);
                return StateName.Substring(0,StateName.Length);
            }
            else
            {
                // display error code
                string tmp = "Error occurred: " + result.ToString();
                return tmp;
            }
        }

        // This function will try to force the device to transition to
        // a new system power state.  True is returned if no error
        // codes are received from SetSystemPowerState(), false otherwise.
        private bool SetSysPowerState(string state)
        {
            uint Flag = POWER_STATE_ON;     // default value
            string temp = state.ToLower();

            // determine which flag to pass into SetSystemPowerState
            switch (state)
            {
                case "unattended":
                    Flag = POWER_STATE_SUSPEND;
                    break;
                case "useridle":
                    Flag = POWER_STATE_USERIDLE;
                    break;
                case "on":
                    break;
                case "screenoff":
                    Flag = POWER_STATE_IDLE;
                    break;
                case "password":
                    Flag = POWER_STATE_PASSWORD;
                    break;
                case "backlighton":
                    Flag = POWER_STATE_BACKLIGHTON;
                    break;
            }
                    
            uint result = SetSystemPowerState(null, Flag, POWER_FORCE);
            if (result != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            StartStop();
        }

        private void StartStop()
        {
            if (btnStartStop.Text.Equals("Start"))
            {
                // Get the value here so it is not overwritten each time
                // in StartTimers
                NumRuns = System.Convert.ToInt32(txtRuns.Text);
                

                // if we need to change system power state, do it
                NewState = txtState.Text;
                if (!NewState.Equals(""))
                {
                    OldState = GetSysPowerState();
                    bool success = SetSysPowerState(NewState);
                    if (!success)
                    {
                        NewState = "";
                        return;
                    }
                }

                // update timer properties and sleep time
                TotalTime = 1000 * System.Convert.ToInt32(txtTotalTime.Text);
                LongTimer.Interval = TotalTime;
                SleepTime = System.Convert.ToInt32(txtSleepTime.Text);
                //LongThreadTimer.Change(TotalTime, Timeout.Infinite);
                MeasureCount = 0;

                // call the appropriate function given the kind of timing
                // we want (loop, timer object, thread)
                TimerType = System.Convert.ToInt32(txtTimerType.Text);
                btnStartStop.Text = "Stop";
				StartMeasuring();
            }
            else
            {
				LongTimer.Enabled = false;
				SetPriority = false;

                // call the appropriate stop function
                switch (TimerType)
                {
                    case 0:
                        // Stop the timers
                        StopTimers();
                        break;
                    case 1:
                        // stop loop
                        StopLoop();
                        break;
                    case 2:
                        // stop thread
                        StopThread();
                        break;
                }

                // change system power state back if needed
                if (!NewState.Equals(OldState) && !NewState.Equals(""))
                {
                    bool success = SetSysPowerState(OldState);
                    if (!success)
                    {
                        // bad stuff
                    }
                    else
                        NewState = "";
                }

                btnStartStop.Text = "Start";
            }
        }


        // This function saves the energy measurements from memory
        // to a text log file
        private void btnSave_Click(object sender, EventArgs e)
        {
			Save();
        }

		private void Save()
		{
			// make sure that the measurements are not running
			bool RestartTimers = false;
			if (btnStartStop.Text.Equals("Stop"))
			{
				RestartTimers = true;
				StopTimers();
			}

			// see if queue is empty
			if (Measurements.Count < 1)
			{
				MessageBox.Show("List is empty");
				if (RestartTimers)
					StartTimers();
				return;
			}

			// Save to text files
			SaveLog(Measurements);

			// restart timers if necessary
			if (RestartTimers)
				StartTimers();
		}

        private void SaveLog(Queue<Measurement> M)
        {
            // check to see if log already exists
            // craft the destination filename accordingly
            BaseName = txtBaseName.Text;
            string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string FileName = folder + "/logs/" + BaseName;
            int FileNum = 0;
            string tmpFileName= "";
            for (; FileNum < 100; FileNum++)
            {
                bool exists = false;
                if (FileNum < 10)
                    exists = System.IO.File.Exists(FileName + "0" + FileNum.ToString() + ".txt");
                else
                    exists = System.IO.File.Exists(FileName + FileNum.ToString() + ".txt");
                if (!exists)
                    break;
            }
            // create new file for each measurements
            bool FirstZero = false;
            for (int j = 0; j < NumStarts; j++)
            {
                if ((FileNum + j) < 10)
                    tmpFileName = FileName + "0" + (FileNum + j).ToString() + ".txt";
                else
                    tmpFileName = FileName + (FileNum + j).ToString() + ".txt";

                // open the text file for saving
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tmpFileName))
                {
                    try
                    {
                        while (true)
                        {
                            //int tmp = M.Dequeue();
							Measurement tmp = M.Dequeue();
                            // determine if it's the start of the run or not
                            //if (tmp != 0)
                            //    sw.Write(tmp.ToString() + " ");
							if (tmp.Current != 0)
								sw.Write(tmp.Current.ToString() + " - " + tmp.Time.ToString() + "\n");
							else
							{
								// if it's the first zero ignore, otherwise break
								// to end the file
								if (!FirstZero)
									FirstZero = true;
								else
									break;

								// write the temp and battery life info
								sw.Write("T:" + tmp.Temp.ToString() + ",B:" + tmp.Life.ToString());
							}
                        }

                    }
                    catch (Exception Exc)
                    {
                        // dequeue will throw exception when empty, so do
                        // nothing I guess
                        

                    }
                }
            }
            MessageBox.Show("Done saving logs.");
            
        }

		public void TestFilter()
		{
			TextReader rdr = File.OpenText("/Temp/source2.txt");
			Parser p = new Parser(rdr);
			values = p.ReadFloats();
			rdr.Close();

			// send through moving average
			//avg = Filter.AvgFilter(ref values, 5);
			//compressed = Filter.Compress(ref avg, 5, 10);
			fft = values;
			// fft
			Fourier.FFT(ref fft, (ulong)fft.Length, 1);
			MessageBox.Show("done");

		}

		public void TestMatch()
		{
			double[] t1, t2, t3;
			TextReader rdr;
			Parser p;

			// read file 1
			rdr = File.OpenText("/Temp/1.txt");
			p = new Parser(rdr);
			t1 = p.ReadFloats();
			rdr.Close();

			// read file 2
			rdr = File.OpenText("/Temp/2.txt");
			p = new Parser(rdr);
			t2 = p.ReadFloats();
			rdr.Close();

			// read file 3
			rdr = File.OpenText("/Temp/3.txt");
			p = new Parser(rdr);
			t3 = p.ReadFloats();
			rdr.Close();

			// try matching
			double dOneTwo, dOneThree, dTwoThree;
			dOneTwo = dataBase.ComputeDistance(t1, t2);
			dOneThree = dataBase.ComputeDistance(t1, t3);
			dTwoThree = dataBase.ComputeDistance(t2, t3);

		}

		public void TestDatabase()
		{
			// add two entries
			/*
			Database d = new Database();
			double[] sig = {3.2, 4.5, 6.0}, fft = {1.1, 2.2, 3.3};

			d.AddEntry("blah", sig, fft);
			d.AddEntry("blah2", sig, fft);
			d.WriteTableToFile("/Temp/database.txt");
			d.RemoveEntry("blah");
			d.RemoveEntry("blah2");

			d.ReadTableFromFile("/Temp/database.txt");
			*/

		}

        private void btnExit_Click(object sender, EventArgs e)
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
                    StartStop();
                    //MessageBox.Show("Button 1 pressed!");
                    break;
                case (int)KeysHardware.Hardware2:
					TestMatch();
                    //MessageBox.Show("Button 2 pressed!");
                    break;
                case (int)KeysHardware.Hardware3:
					TestDatabase();
                    //MessageBox.Show("Button 3 pressed!");
                    break;
                case (int)KeysHardware.Hardware4:
                    //MessageBox.Show("Button 4 pressed!");
                    break;
            }

        }

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void mnuClearMem_Click(object sender, EventArgs e)
		{
			// clear the queue contents
			Measurements.Clear();
			NumStarts = 0;
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			// exit the application
			Application.Exit();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			
		}

		private void mnuSaveToFile_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void mnuSaveToDatabase_Click(object sender, EventArgs e)
		{
			/*
			double[] result;
			// generate a filtered/compressed signal for all of the measurements
			// in the current set, and then add it to the database
			Queue<double>[] Data = new Queue<double>[NumStarts];
			for (int i = 0; i < NumStarts; i++)
			{
				Data[i] = new Queue<double>();
			}

			int MeasureNum = 0, shortestMeasurement = 100000, tmpNum;
			bool firstZero = false;
			for (int i = 0; i < NumStarts; i++)
			{
				while (true)
				{
					if (Measurements.Count > 0)
						tmpNum = Measurements.Dequeue();
					else
						break;
					// see if it's a transition between measurements
					if (tmpNum == 0)
					{
						if (!firstZero)
							firstZero = true;
						else
						{
							MeasureNum++;
							break;
						}
					}
					else
						Data[MeasureNum].Enqueue(tmpNum);
				}
				// determine longest measurement on the way
				if (Data[MeasureNum - 1].Count < shortestMeasurement)
					shortestMeasurement = Data[MeasureNum - 1].Count;
			}

			// generate one averaged dataset for all the measurements
			result = new double[shortestMeasurement];
			for (int i = 0; i < shortestMeasurement; i++)
			{
				double avg = 0.0;
				for (int j = 0; j < NumStarts; j++)
				{
					avg += Data[j].Dequeue();
				}
				avg /= NumStarts;
				result[i] = avg;
			}

			// perform running average, compression and fft
			result = Filter.AvgFilter(ref result, AvgLookAhead);
			result = Filter.Compress(ref result, CompLookAhead, CompThreshold);
			*/
			int numSamples;
			double[] result = GenerateSignature(out numSamples);
			double[] fft = result;
			Fourier.FFT(ref fft, (ulong)fft.Length, 1);

			// add to database
			uint temp = Measurements.Peek().Temp;
			byte batt = Measurements.Peek().Life;
			dataBase.AddEntry(txtBaseName.Text, result, fft, numSamples, temp, batt);
			//dataBase.AddEntry(txtBaseName.Text, result, fft, numSamples);
			

		}

		private double[] GenerateSignature(out int numSamples)
		{
			double[] result = { 0.0 };
			numSamples = 0;
			if (NumStarts == 0 || Measurements.Count == 0)
				return result;

			// generate a filtered/compressed signal for all of the measurements
			// in the current set, and then add it to the database
			
			Queue<double>[] Data = new Queue<double>[NumStarts];
			//int[] MeasureCopy = new int[Measurements.Count];
			Measurement[] MeasureCopy = new Measurement[Measurements.Count];
			Measurements.CopyTo(MeasureCopy, 0);
			for (int i = 0; i < NumStarts; i++)
			{
				Data[i] = new Queue<double>();
			}

			int MeasureNum = 0, shortestMeasurement = 100000, tmpNum;
			bool firstZero = false;
			int index = 0;
			for (int i = 0; i < MeasureCopy.Length; i++)
			{
				//tmpNum = MeasureCopy[i];
				tmpNum = MeasureCopy[i].Current;
				if (tmpNum == 0)
				{
					if (!firstZero)
						firstZero = true;
					else
					{
						MeasureNum++;
						// determine longest measurement on the way
						if (Data[MeasureNum - 1].Count < shortestMeasurement)
							shortestMeasurement = Data[MeasureNum - 1].Count;
					}
				}
				else
					Data[MeasureNum].Enqueue(tmpNum);
			}
			// check one last time for shortest measurement
			if (Data[MeasureNum].Count < shortestMeasurement)
				shortestMeasurement = Data[MeasureNum].Count;
			/*
			for (int i = 0; i < NumStarts; i++)
			{
				while (index < MeasureCopy.Length)
				{
					if (MeasureCopy.Length > 0)
						tmpNum = MeasureCopy[index++];
					else
						break;
					// see if it's a transition between measurements
					if (tmpNum == 0)
					{
						if (!firstZero)
							firstZero = true;
						else
						{
							MeasureNum++;
							break;
						}
					}
					else
						Data[MeasureNum].Enqueue(tmpNum);
				}
				// determine longest measurement on the way
				if (Data[MeasureNum - 1].Count < shortestMeasurement)
					shortestMeasurement = Data[MeasureNum - 1].Count;
			}
			*/
			// generate one averaged dataset for all the measurements
			result = new double[shortestMeasurement];
			for (int i = 0; i < shortestMeasurement; i++)
			{
				double avg = 0.0;
				for (int j = 0; j < NumStarts; j++)
				{
					avg += Data[j].Dequeue();
				}
				avg /= NumStarts;
				result[i] = avg;
			}
			numSamples = shortestMeasurement;

			// perform running average, compression and fft
			result = Filter.AvgFilter(ref result, AvgLookAhead);
			result = Filter.Compress(ref result, CompLookAhead, CompThreshold);

			return result;

		}


		private void mnuDataExport_Click(object sender, EventArgs e)
		{
			dataBase.WriteTableToFile(DatabaseName);
		}

		private void mnuDataImport_Click(object sender, EventArgs e)
		{
			dataBase.ClearEntries();
			dataBase.ReadTableFromFile(DatabaseName);
		}

		private void mnuDataExport_Click_1(object sender, EventArgs e)
		{

		}

		private void mnuMatch_Click(object sender, EventArgs e)
		{
			int numSamples;
			double[] result = GenerateSignature(out numSamples);

			string match = dataBase.Match(result, numSamples);
			MessageBox.Show(match);

		}

	}


	// *** Parser class to read numbers from a file ***
	public class Parser
	{
		static Regex spaces = new Regex(@"\s+");
		TextReader rdr;

		public Parser(TextReader tr)
		{
			rdr = tr;
		}

		public string[] ReadStrings()
		{
			string line = rdr.ReadLine();
			if (line == null)
				return null;
			return spaces.Split(line);
		}

		public double[] ReadFloats()
		{
			string[] fields = ReadStrings();
			if (fields == null)
				return null;
			int istart = (fields[0].Length == 0) ? 1 : 0;
			double[] obj = new double[fields.Length - istart - 1];
			for (int i = 0; i < obj.Length; i++)
			{
				try
				{
					obj[i] = double.Parse(fields[i + istart]);
				}
				catch
				{
					obj[i] = 0;
				}
			}
			return obj;
		}
	}

}