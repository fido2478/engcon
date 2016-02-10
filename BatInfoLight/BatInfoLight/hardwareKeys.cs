using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;

namespace BatInfoLight
{
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
}
