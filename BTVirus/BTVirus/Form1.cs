using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using BTAccess;
using Microsoft.WindowsMobile.PocketOutlook;

namespace BTVirus
{
    public partial class Form1 : Form
    {
        #region Member Variables
        // List to keep track of devices sending files to
        private Queue<BtDevice> Devices;
        private Queue<string> DeviceNames;
        BT BlueTooth;
        byte[] FileData;
        int FileSize = 15000;
        string FileToSend = "/readme.txt";
        string MMSFileToSend = "/warn.txt";
        string ExtToFind = ".sis";
        private delegate void MethodInvoker();
		private bool BTActive;

        // sequence definitions for the different attack types
        const int SeqLength = 5;
        int[] CommWarriorSeq = { 3, 5, 6, 4, 0 };
        int[] CabirSeq = { 2, 3, 5, 6, 0 };
        int[] MabirSeq = { 3, 4, 5, 6, 0 };
        int[] LascoSeq = { 2, 3, 5, 6, 7 };
        int[] CurSeq;

        #endregion

        private void AddResult(string s)
        {
            int nIndex = lstResults.Items.Add(s);

            //Keep last-added item visible at bottom of listbox
            lstResults.SelectedIndex = nIndex;
            lstResults.SelectedIndex = -1;
        }

        // this method carries out the attack sequence
        private void Attack()
        {
            // make sure sequence was set to something non-zero
            if (CurSeq[0] == 0)
                return;

			// connect to stack
			BlueTooth.Connect();
			BTActive = true;

            for (int i = 0; i < SeqLength; i++)
            {
                int Operation = CurSeq[i];
                switch (Operation)
                {
                    case 2:
                        AddResult("Displaying");
                        Display();
                        break;
                    case 3:
                        AddResult("Creating Files");
                        CreateFiles();
                        break;
                    case 4:
                        AddResult("Sending MMS");
                        SendMMS();
                        break;
                    case 5:
                        AddResult("Scanning");
                        BlueTooth.Scan(out Devices, out DeviceNames);
                        break;
                    case 6:
                        AddResult("Sending Files");
                        SendFiles();
                        break;
                    case 7:
                        AddResult("Searching for Files");
                        SearchFiles();
                        break;
                    default:
                    // stuff
                        break;
                }
            }

            AddResult("Done");
			BlueTooth.Finish();
			BTActive = false;
        }

        // this method will display a message on the device and keep
        // executing (by creating a new thread just for the messagebox call)
        private void Display()
        {
            Thread t = new Thread(new ThreadStart(ShowMessage));
            t.Start();
        }

        private void ShowMessage()
        {
            MessageBox.Show("Install Lasco?");

        }

        // this method will Create a few files on the device to mimic a virus
        // creating system files
        private void CreateFiles()
        {
            string fileName = "blah.out";
            FileStream outFile;
            outFile = File.OpenWrite(fileName);

            outFile.Write(FileData, 0, FileSize);
            outFile.Close();
        }

        // this method just sends a smaller text file than SendFiles
        private void SendMMS()
        {
            //BlueTooth.SendFiles(MMSFileToSend);

			// search through the contact list to simulate looking for phone numbers to send to
			// Write to file
			string outFile = "/contacts.txt";
			TextWriter t = new StreamWriter(outFile);
			OutlookSession outLook = new OutlookSession();
			foreach (Contact c in outLook.Contacts.Items)
			{
				t.WriteLine(c.FileAs);
			}
			t.Close();

        }

        // this method will send 'FileToSend' to all the devices
        // in the queue
        private void SendFiles()
        {
            BlueTooth.SendFiles(FileToSend);
        }

        // this method will search the system for all files of extension ExtToFind
        private void SearchFiles()
        {
            Queue<string> FileList = new Queue<string>();
            DirectoryInfo dir = new DirectoryInfo(@"\");
            getDirsFiles(dir, FileList);

        }

        //this is the recursive function
        private void getDirsFiles(DirectoryInfo d, Queue<String> list)
        {
            //create an array of files using FileInfo object
            FileInfo[] files;
            //get all files for the current directory
            files = d.GetFiles("*.*");

            //iterate through the directory and find files of
            // interest
            foreach (FileInfo file in files)
            {
                //get details of each file using file object
                String fileName = file.FullName;
                if (fileName.EndsWith(ExtToFind))
                    list.Enqueue(fileName);
            }

            //get sub-folders for the current directory
            DirectoryInfo[] dirs = d.GetDirectories("*.*");

            // recurse through subdirectories
            foreach (DirectoryInfo dir in dirs)
            {
                getDirsFiles(dir, list);
            }

        }


        public Form1()
        {
            InitializeComponent();

			
        }

        private void Form1_Load(object sender, EventArgs e)
        {

			BlueTooth = new BT(lstResults);

			//Define general event handlers only once in the demo app
			BtStack.BtDevFound += new BtStack.BtDevFoundHandler(BlueTooth.OnDevFound);
			BtStack.BtConnStatus += new BtStack.BtConnStatusHandler(BlueTooth.OnConnStatus);
			BtStack.BtSendFileComplete += new BtStack.BtSendFileCompleteHandler(BlueTooth.OnSendFileComplete);
			//BtStack.BtSendFileProgress += new BtStack.BtSendFileProgressHandler(BlueTooth.OnSendFileProgress);
			BtStack.BtRecvFileComplete += new BtStack.BtRecvFileCompleteHandler(BlueTooth.OnRecvFile);
			BtStack.BtBcComplete += new BtStack.BtBcCompleteHandler(BlueTooth.OnBcComplete);
			BtStack.BtSearchComplete += new BtStack.BtSearchCompleteHandler(BlueTooth.OnSearchComplete);

            // write the fileData buffer
            FileData = new byte[FileSize];
            for (int i = 0; i < FileSize; i++)
            {
                FileData[i] = (byte)(i % 256);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
			if (BTActive)
				BlueTooth.Finish();
			//BlueTooth.Disconnect();
            this.Close();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text.Equals("Start"))
            {
                // figure out which type to run
                string Type = cboAttackType.Text;
                if (Type.Equals("CommWarrior"))
                {
                    CurSeq = CommWarriorSeq;

                }
                else if (Type.Equals("Cabir"))
                {
                    CurSeq = CabirSeq;
                }
                else if (Type.Equals("Mabir"))
                {
                    CurSeq = MabirSeq;
                }
                else if (Type.Equals("Lasco"))
                {
                    CurSeq = LascoSeq;
                }
                else
                {
                    // bad stuff
                }

                btnStartStop.Text = "Running";
                // start the attack
                Attack();
                btnStartStop.Text = "Start";
            }
            else
            {
                // stop scanning
                BlueTooth.ScanToggle(false);
            }
        }

        //----------------------------------------------------------
        // Do cleanup when form/app is closed
        //----------------------------------------------------------
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //Remove event handlers
            BtStack.BtDevFound -= new BtStack.BtDevFoundHandler(BlueTooth.OnDevFound);
			BtStack.BtConnStatus -= new BtStack.BtConnStatusHandler(BlueTooth.OnConnStatus);
			BtStack.BtSendFileComplete -= new BtStack.BtSendFileCompleteHandler(BlueTooth.OnSendFileComplete);
			//BtStack.BtSendFileProgress -= new BtStack.BtSendFileProgressHandler(BlueTooth.OnSendFileProgress);
			BtStack.BtRecvFileComplete -= new BtStack.BtRecvFileCompleteHandler(BlueTooth.OnRecvFile);
			BtStack.BtBcComplete -= new BtStack.BtBcCompleteHandler(BlueTooth.OnBcComplete);
			BtStack.BtSearchComplete -= new BtStack.BtSearchCompleteHandler(BlueTooth.OnSearchComplete);

            //Disconnect from stack
            //btStack.StopDeviceSearch();
            //btStack.Disconnect();

            base.OnClosing(e);
        }
        


    }



    // ***  Bluetooth class for all bluetooth-specific stuff
    public class BT
    {
        // member objects
        private BtStack btStack;
        private BtDevice curDevice;
        private Queue<BtDevice> Devices;
        private Queue<string> DeviceNames;
        private ListBox lstResults;
        private bool SearchComplete;
		private bool SendComplete;

        public BT()
        {
            try
            {
                //Get new Stack object
                btStack = new BtStack();

                //Connect to bluetooth radio
                //ShowFcnResult("StackConnect", btStack.Connect());

                Devices = new Queue<BtDevice>();
                DeviceNames = new Queue<string>();
                SearchComplete = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);

            }

        }

        public BT(ListBox lb)
        {
            lstResults = lb;
            try
            {
                //Get new Stack object
                btStack = new BtStack();

                //Connect to bluetooth radio
                //ShowFcnResult("StackConnect", btStack.Connect());

                Devices = new Queue<BtDevice>();
                DeviceNames = new Queue<string>();

                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Application.Exit();

            }

        }

        private void ShowFcnResult(string fcn, eBTRC rc)
        {
            string s;
            s = String.Format("{0}-->{1}",
                fcn,
                rc == eBTRC.BT_OK ? "BT_OK" :
                rc == eBTRC.BT_FAIL ? "BT_FAIL" :
                rc == eBTRC.BT_DEVICE_NOTAVAIL ? "BT_DEVICE_NOTAVAIL" :
                rc == eBTRC.BT_SVC_NOT_SUPPORTED ? "BT_SVC_NOT_SUPPORTED" :
                rc == eBTRC.BT_SVC_NOT_UNIQUE ? "BT_SVC_NOT_UNIQUE" :
                                                 "unknown rc");
            AddResult(s);
			if (fcn.Equals("SendFile") && rc != eBTRC.BT_OK)
				SendComplete = true;
        }

        private void AddResult(string s)
        {
            int nIndex = lstResults.Items.Add(s);

            //Keep last-added item visible at bottom of listbox
            lstResults.SelectedIndex = nIndex;
            lstResults.SelectedIndex = -1;
        }

        public void Scan(out Queue<BtDevice> DeviceList, out Queue<string> DeviceNameList)
        {
            // start the search
            SearchComplete = false;
            btStack.StartDeviceSearch();

            // wait until the search is done
            while (!SearchComplete)
                Application.DoEvents();

            // return the list of found devices
            DeviceList = Devices;
            DeviceNameList = DeviceNames;
        }

        // this method just provides a way for a caller to 
        // stop the search and disconnect in case the program
        // is closing
        public void Finish()
        {
            btStack.StopDeviceSearch();
            btStack.Disconnect();
        }

		// this method attempts to connect to the bluetooth stack again
		public void Connect()
		{
			ShowFcnResult("StackConnect", btStack.Connect());

			// clear the queue
			Devices.Clear();
			DeviceNames.Clear();
		}

		public void Disconnect()
		{
			btStack.Disconnect();
		}

        //----------------------------------------------------------
        // DeviceFound handler
        //----------------------------------------------------------
        public void OnDevFound(BtDevice device)
        {
            // add device to list if not already contained
            if (DeviceNames.Contains(device.DeviceName))
                return;
            AddResult(String.Format("Adding '{0}' to list", device.DeviceName));
            Devices.Enqueue(device);
            DeviceNames.Enqueue(device.DeviceName);
        }

        // Function to send file to all devices in the queue
        public void SendFiles(string filename)
        {
            if (Devices.Count < 1)
                return;

            BtDevice curDevice;
            while (Devices.Count > 0)
            {
                curDevice = Devices.Dequeue();
                DeviceNames.Dequeue();
				SendComplete = false;
				AddResult(String.Format("Sending '{0}'", filename));
				ShowFcnResult("SendFile", curDevice.SendFile(filename));
				while (!SendComplete)
					Application.DoEvents();


            }

            AddResult("Done sending files.");
        }

        //----------------------------------------------------------
        // ConnectionStatusChanged handler
        //----------------------------------------------------------
        public void OnConnStatus(ConnStatus connStatus)
        {
            //Show connect-status, and if connected show port we're using too
            AddResult(String.Format("  >Status: {0}{1}",
                connStatus.Status == eBTCONN_STATUS.BT_CONNECTION_COMPLETE ? "Connected" :
                connStatus.Status == eBTCONN_STATUS.BT_CONNECTION_LOST ? "Lost connection" :
                                                                           "Connection failed",

                connStatus.Status == eBTCONN_STATUS.BT_CONNECTION_COMPLETE ?
                                  "(" + connStatus.COMPort + ")" : ""));
        }

        //----------------------------------------------------------
        // SendFileComplete handler
        //----------------------------------------------------------
        public void OnSendFileComplete(eBTRC rc)
        {
            eBTRC i = rc;

            AddResult(String.Format("  >SendFile: {0}",
                rc == eBTRC.BT_FTP_OPEN_FAILED ? "open failed" :
                rc == eBTRC.BT_FTP_SEND_ERROR ? "send error" :
                rc == eBTRC.BT_FTP_CLOSE_FAILED ? "close failed" :
                rc == eBTRC.BT_OK ? "completed" :
                                                "unknown error"));

			SendComplete = true;


        }

        //----------------------------------------------------------
        // SendFileProgress Handler - indicates bytes sent so far
        //----------------------------------------------------------
        public void OnSendFileProgress(Int32 CurrentBytes, Int32 TotalBytes)
        {
            string s = String.Format("  >Sent {0} of {1} bytes", CurrentBytes, TotalBytes);
            AddResult(s);
        }

        //----------------------------------------------------------
        // RecvFileComplete Handler - indicates we received a file
        //----------------------------------------------------------
        public void OnRecvFile(RecvFileInfo info)
        {
            MessageBox.Show(String.Format("Received file '{0}' from {1}",
                                          info.FileName, info.DeviceName),
                            "FILE ALERT!", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
        }

        //----------------------------------------------------------
        // BusinessCardComplete Handler
        //----------------------------------------------------------
        public void OnBcComplete(eBTRC rc)
        {
            AddResult(String.Format("  >BusCard operation {0}",
                rc == eBTRC.BT_OK ? "succeeded" : "failed"));
        }

        //----------------------------------------------------------
        // SearchComplete Handler
        //----------------------------------------------------------
        public void OnSearchComplete()
        {
            AddResult("  >Search operation complete");
            SearchComplete = true;

        }

        // Function to start/stop the search
        // mode==true -> start
        // mode==false -> stop
        public void ScanToggle(bool mode)
        {
            if (mode)
            {
                //ShowFcnResult("StartSearch", btStack.StartDeviceSearch());
            }
            else
            {
                btStack.StopDeviceSearch();
                AddResult("Stopped searching for devices");
            }
        }
    }

}