using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace SystemUtils
{
    public partial class frmPowerTrans : Form
    {
        // variable for handles and stuff
        PowerNotifications pn = null;
        System.Windows.Forms.Timer timer1 = null;
        Messages MessageList = null;

        // This function will check the MessageList object
        // for new messages to display.
        private void ReadText(object sender, EventArgs e)
        {
            // grab all the available messages from the messagelist
            while (MessageList.Count() > 0)
            {
                string curMessage = MessageList.ReadMessage();
                lstInfo.Items.Add(curMessage);
            }
        }

        public frmPowerTrans()
        {
            InitializeComponent();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // do stuff depending on the text
            switch (btnStartStop.Text)
            {
                case "Start":
                    {
                        // create a new Messages object and pass it into the PowerNotifications
                        // constructor
                        MessageList = new Messages();

                        // create the powernotifications object
                        pn = new PowerNotifications(ref MessageList);
                        pn.Start();

                        // start a timer to monitor the messages queue
                        if (timer1 == null)
                            timer1 = new System.Windows.Forms.Timer();
                        timer1.Interval = 250;
                        timer1.Tick += new EventHandler(ReadText);
                        timer1.Enabled = true;
                        btnStartStop.Text = "Stop";
                    }
                    break;
                case "Stop":
                    // stop the thread
                    pn.Stop();
                    lstInfo.Items.Add("(" + System.DateTime.Now.ToString("hh:mm:ss") + ") stopping loop");
                    timer1.Enabled = false;
                    btnStartStop.Text = "Start";
                    break;
                default:
                    break;
            }

        }

        #region form selection handlers
        private void ShowPowerInfo(object sender, EventArgs e)
        {
            // create a new PowerInfo form if necessary
            if (frmMain.PIForm == null)
                frmMain.PIForm = new frmPowerInfo();
            frmMain.PIForm.Show();
        }

        private void ShowSelector(object sender, EventArgs e)
        {
            frmMain.MainForm.Show();
        }

        private void ShowRegInfo(object sender, EventArgs e)
        {
            if (frmMain.RIForm == null)
                frmMain.RIForm = new frmRegInfo();
            frmMain.RIForm.Show();
        }

        private void ShowPowerDrain(object sender, EventArgs e)
        {
            if (frmMain.PDForm == null)
                frmMain.PDForm = new frmPowerDrain();
            frmMain.PDForm.Show();
        }

        private void ShowBatteryInfo(object sender, EventArgs e)
        {
            if (frmMain.BIForm == null)
                frmMain.BIForm = new frmBatteryInfo();
            frmMain.BIForm.Show();
        }

        private void ShowTimerInfo(object sender, EventArgs e)
        {
            if (frmMain.TIForm == null)
                frmMain.TIForm = new frmTimerInfo();
            frmMain.TIForm.Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            frmMain.MainForm.Close();
        }
        #endregion

    }

    // This class handles interfacing with the Power Manager and
    // reading messages from the message queue established with
    // the PM.  Information is passed to the form through the MessageList
    // object.
    public class PowerNotifications
    {
        IntPtr ptr = IntPtr.Zero;
        IntPtr ptr2 = IntPtr.Zero;
        Thread t = null;
        bool done = false;
        Messages MessageList = null;

        #region Constants
        public const uint PBT_TRANSITION = 0x00000001;  // broadcast specifying system power state transition
        public const uint PBT_RESUME = 0x00000002;  // broadcast notifying a resume, specifies previous state
        public const uint PBT_POWERSTATUSCHANGE = 0x00000004;  // power supply switched to/from AC/DC
        public const uint PBT_POWERINFOCHANGE = 0x00000008;  // some system power status field has changed
        public const uint PBT_OEMBASE = 0x00010000;

        public const uint POWER_NOTIFY_ALL = (uint)0xFFFFFFFF;
        public uint POWER_STATE(uint f) { return (f) & 0xFFFF0000; }   // power state mask
        public const uint POWER_STATE_ON = 0x00010000;         // on state
        public const uint POWER_STATE_OFF = 0x00020000;         // no power, full off
        public const uint POWER_STATE_CRITICAL = 0x00040000;         // critical off
        public const uint POWER_STATE_BOOT = 0x00080000;         // boot state
        public const uint POWER_STATE_IDLE = 0x00100000;         // idle state
        public const uint POWER_STATE_SUSPEND = 0x00200000;         // suspend state
        public const uint POWER_STATE_RESET = 0x00800000;         // reset state
        #endregion

        #region API imports
        [DllImport("coredll.dll")]
        private static extern IntPtr RequestPowerNotifications(IntPtr hMsgQ, uint Flags);

        [DllImport("coredll.dll")]
        private static extern bool StopPowerNotifications(IntPtr hMsgQ);

        [DllImport("coredll.dll")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, int wait);

        [DllImport("coredll.dll")]
        private static extern IntPtr CreateMsgQueue(string name, ref MsgQOptions options);

        [DllImport("coredll.dll")]
        private static extern bool CloseMsgQueue(IntPtr handle);

        [DllImport("coredll.dll")]
        private static extern bool ReadMsgQueue(IntPtr hMsgQ, byte[] lpBuffer, uint cbBufSize, ref uint lpNumRead, int dwTimeout, ref uint pdwFlags);
        #endregion

        public PowerNotifications()
        {
            // set up the Message queue options, create the queue
            // and create a background thread to monitor it
            MsgQOptions options = new MsgQOptions();
            options.dwFlags = 0;
            options.dwMaxMessages = 20;
            options.cbMaxMessage = 10000;
            options.bReadAccess = true;
            options.dwSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(options);
            ptr = CreateMsgQueue("Test", ref options);
            RequestPowerNotifications(ptr, 0xFFFFFFFF);
            t = new Thread(new ThreadStart(DoWork));
        }

        // Overloaded constructor that accepts a reference to the
        // Messages object created by the form.
        public PowerNotifications(ref Messages mList)
        {
            // assign the messages object
            MessageList = mList;

            // set up the Message queue options, create the queue
            // and create a background thread to monitor it
            MsgQOptions options = new MsgQOptions();
            options.dwFlags = 0;
            options.dwMaxMessages = 20;
            options.cbMaxMessage = 10000;
            options.bReadAccess = true;
            options.dwSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(options);
            ptr = CreateMsgQueue("Test", ref options);
            IntPtr ptr2 = RequestPowerNotifications(ptr, 0xFFFFFFFF);
            if (ptr2.Equals(IntPtr.Zero))
            {
                // error occurred
                MessageList.AddMessage("Error with RPN");
            }

            t = new Thread(new ThreadStart(DoWork));
        }

        public void Start()
        {
            t.Start();      // start the background thread
        }

        public void Stop()
        {
            done = true;

            //close the message queue and stop notifications
            CloseMsgQueue(ptr);
            StopPowerNotifications(ptr2);

            t.Abort();
        }

        // This is the entry point for the background thread which
        // monitors the message queue established with the power manager
        private void DoWork()
        {
            // create a read buffer
            byte[] buf = new byte[10000];
            uint nRead = 0, flags = 0, res = 0;

            MessageList.AddMessage("starting loop");

            try
            {
                while (!done)
                {
                    res = WaitForSingleObject(ptr, 1000);   // blocking call
                    if (res == 0)
                    {
                        // read the message from the queue
                        ReadMsgQueue(ptr, buf, (uint)buf.Length, ref nRead, -1, ref flags);

                        // Parse the information from the message
                        uint flag = ConvertByteArray(buf, 4);
                        uint qMessage = ConvertByteArray(buf, 0);
                        uint len = ConvertByteArray(buf, 8);     // length of SystemPowerState field?
                        string SysPowerState = "";

                        if (len != 0 && qMessage != 8)
                        {
                            SysPowerState = Encoding.Unicode.GetString(buf, 12, (int)(len - 1));
                        }

                        string msg = null;

                        // determine the type of event from the message field
                        switch (qMessage)
                        {
                            case PBT_TRANSITION:
                                msg = "Trans: ";
                                msg += SysPowerState + ",";
                                break;
                            case PBT_RESUME:
                                //msg = "Resume: ";
                                //msg += SysPowerState + ",";
                                break;
                            case PBT_POWERSTATUSCHANGE:
                                msg = "Power status chg: ";
                                break;
                            default:
                                msg = "";
                                break;
                        }

                        // determine the state flag contents
                        switch (flag)
                        {
                            case 65536:
                                msg += "Pw On";
                                break;
                            case 131072:
                                msg += "Pw Off";
                                break;
                            case 262144:
                                msg += "Pw Crit";
                                break;
                            case 524288:
                                msg += "Pw Boot";
                                break;
                            case 1048576:
                                msg += "Pw Idle";
                                break;
                            case 2097152:
                                msg += "Pw Susp";
                                break;
                            case 8388608:
                                msg += "Pw Reset";
                                break;
                            case 0:
                                // non power transition messages are ignored
                                //msg += "State unspc";
                                break;
                            default:
                                msg += "Ukwn Flag: " + flag;
                                break;
                        }

                        if (msg != null)
                        {
                            // write the message to the Message List
                            MessageList.AddMessage(msg);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (!done)
                {
                    // write the exception out
                    MessageList.AddMessage("Got exception: " + ex.ToString());
                }
            }
        }

        // helper function to conver a byte array to a uint
        uint ConvertByteArray(byte[] array, int offset)
        {
            uint res = 0;
            res += array[offset];
            res += array[offset + 1] * (uint)0x100;
            res += array[offset + 2] * (uint)0x10000;
            res += array[offset + 3] * (uint)0x1000000;
            return res;
        }

        #region Struct definitions
        [StructLayout(LayoutKind.Sequential)]
        public struct MsgQOptions
        {
            public uint dwSize;
            public uint dwFlags;
            public uint dwMaxMessages;
            public uint cbMaxMessage;
            public bool bReadAccess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POWER_BROADCAST
        {
            public int Message;    // one of PBT_Xxx
            public int Flags;      // one of POWER_STATE_Xxx
            public int Length;     // byte count of data starting at SystemPowerStateName
            public char SystemPowerState;    // variable length field, must be smaller than MAX_PATH + 1
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct POWER_BROADCAST_POWER_INFO
        {
            uint dwBatteryLifeTime;
            uint dwBatteryFullLifeTime;
            uint dwBackupBatteryLifeTime;
            uint dwBackupBatteryFullLifeTime;
            byte bACLineStatus;
            byte bBatteryFlag;
            byte bBatteryLifePercent;
            byte bBackupBatteryFlag;
            byte bBackupBatteryLifePercent;
        };
        #endregion

    }

    // This class only serves the purpose of acting as a message queue for the form1
    // and PowerNotifications class, so that the two objects can communicate.
    public class Messages
    {
        private Queue<string> MessageList;

        public Messages()
        {
            // instantiate the queue
            MessageList = new Queue<string>();
        }

        // return the first message from the queue
        public string ReadMessage()
        {
            // return the first available message string
            if (MessageList.Count > 0)
            {
                string curMessage = MessageList.Dequeue();
                return curMessage;
            }

            return "";
        }

        // add a message to the end of the queue
        public void AddMessage(string Message)
        {
            // add the string to the queue
            if (!Message.Equals(""))
            {
                // get the current time for a timestamp
                string time = "(" + System.DateTime.Now.ToString("hh:mm:ss") + ")";

                MessageList.Enqueue(time + " " + Message);
            }
        }

        public int Count()
        {
            return MessageList.Count;
        }

    }
}