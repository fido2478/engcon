using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BatInfoLight
{
	public class MeasureClass
	{
		uint lastTick = 0;

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
			public int BatteryCurrent;
			public uint BatteryAverageCurrent;
			public uint BatteryAverageInterval;
			public uint BatterymAHourConsumed;
			public uint BatteryTemperature;
			public uint BackupBatteryVoltage;
			public byte BatteryChemistry;
		}

		private SYSTEM_POWER_STATUS_EX2 status;
		private uint DataSize = 0;

		public MeasureClass()
		{
			// prepare for measuring
			status = new SYSTEM_POWER_STATUS_EX2();
			DataSize = (uint)Marshal.SizeOf(status);
		}

		// this function takes the measurement
		/*
		public int TakeMeasurement()
		{
			uint result = GetSystemPowerStatusEx2(out status, DataSize, true);
			if (DataSize == result)
				return status.BatteryCurrent;
			else
				return 0;
		}
		*/

		public Form1.Measurement TakeMeasurement()
		{
			Form1.Measurement M = new Form1.Measurement();
			uint result = GetSystemPowerStatusEx2(out status, DataSize, true);
			if (DataSize == result)
				M.Current = status.BatteryCurrent;
			else
				M.Current = 0;

			//uint curTick = Form1.GetTickCount();
			//if (lastTick == 0)
				M.Time = 0;
			//else
			//M.Time = curTick - lastTick;

			//lastTick = curTick;
			return M;

		}

		// this function will also take a measurement but will get the temperature and
		// battery life (%) as well.
		public Form1.Measurement TakeMeasurement(bool getTemp)
		{
			Form1.Measurement M = new Form1.Measurement();
			uint result = GetSystemPowerStatusEx2(out status, DataSize, true);
			if (DataSize == result)
				M.Current = status.BatteryCurrent;
			else
				M.Current = 0;

			//uint curTick = Form1.GetTickCount();
			//if (lastTick == 0)
			M.Time = 0;
			M.Temp = status.BatteryTemperature;
			M.Life = status.BatteryLifePercent;
			//else
			//M.Time = curTick - lastTick;

			//lastTick = curTick;
			return M;
		}

		public void ResetTick(uint tick)
		{
			lastTick = tick;
		}
	}
}
