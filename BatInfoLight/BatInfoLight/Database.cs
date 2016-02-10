using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BatInfoLight
{
	public class Database
	{
		private int NumInitialElts =	64;
		private int MatchThreshold =	20;		// change
		private int NumEntries;
		private double SampleDiffThreshold = 0.15;
		private double MaxSamples = 200;		// for 20 seconds

		// struct to hold each entry
		public class SignatureEntry
		{
			public string Name;
			public double[] Signal;
			public double[] FFT;
			public int NumSamples;
			public uint Temperature;
			public byte BattLife;

			public SignatureEntry()
			{
			}

			public SignatureEntry(string name, double[] signal, double[] fft, int numSamples)
			{
				Name = name;
				Signal = signal;
				FFT = fft;
				NumSamples = numSamples;
			}

			public SignatureEntry(string name, double[] signal, double[] fft, int numSamples, uint temperature, byte battLife)
			{
				Name = name;
				Signal = signal;
				FFT = fft;
				NumSamples = numSamples;
				Temperature = temperature;
				BattLife = battLife;
			}

		}

		private List<SignatureEntry> Table;

		public Database()
		{
			// initialize the table
			Table = new List<SignatureEntry>(NumInitialElts);
			NumEntries = 0;
		}

		// this function will add an entry to the table and return its index
		public int AddSignature(SignatureEntry entry)
		{
			Table.Add(entry);
			NumEntries++;
			return Table.Count - 1;
		}

		// this function will create a new entry and add it to the list then
		// return its index
		public int AddEntry(string name, double[] signal, double[] fft, int numSamples, uint temperature, byte battLife)
		{
			SignatureEntry entry = new SignatureEntry(name, signal, fft, numSamples, temperature, battLife);
			Table.Add(entry);
			NumEntries++;
			return Table.Count - 1;

		}

		public int AddEntry(string name, double[] signal, double[] fft, int numSamples)
		{
			SignatureEntry entry = new SignatureEntry(name, signal, fft, numSamples);
			Table.Add(entry);
			NumEntries++;
			return Table.Count - 1;

		}

		// this function searches the table for a signature with Name = identifier
		// and changes the signal or fft array based on field.  (field==0 -> update Signal).
		public void UpdateSignature(string identifier, double[] data, int field)
		{
			int index = -1;

			// look for entry with Name == identifier
			for (int i = 0; i < Table.Count; i++)
			{
				if (Table[i].Name.Equals(identifier))
				{
					index = i;
					break;
				}
			}

			if (index == -1)
				return;

			// update field
			if (field == 0)
				Table[index].Signal = data;
			else
				Table[index].FFT = data;
		}

		// this function will search for an entry in the table and if it exists, remove it
		public void RemoveEntry(string identifier)
		{
			for (int i = 0; i < Table.Count; i++)
			{
				if (Table[i].Name.Equals(identifier))
					Table.RemoveAt(i);
			}
			NumEntries--;
		}

		// this function will clear the table
		public void ClearEntries()
		{
			Table.Clear();
		}
		// this function will write the database to file
		// if successful then function returns true
		public bool WriteTableToFile(string filename)
		{
			if (filename.Equals(""))
				return false;

			// copy old database to keep backup
			if (File.Exists(filename))
				File.Copy(filename, filename + ".bak", true);

			// open the file for write access
			TextWriter t = new StreamWriter(filename);

			foreach (SignatureEntry e in Table)
			{
				// write name
				t.Write(e.Name + ";" + e.NumSamples.ToString() + ";");

				// write batt temp and life percent
				t.Write(e.Temperature.ToString() + ";" + e.BattLife.ToString() + ";");

				// write signal data
				for (int i = 0; i < e.Signal.Length; i++)
				{
					if (i == e.Signal.Length - 1)
						t.Write(e.Signal[i].ToString());
					else
						t.Write(e.Signal[i].ToString() + ",");
				}
				t.Write(";");

				// write fft data
				for (int i = 0; i < e.FFT.Length; i++)
				{
					if (i == e.FFT.Length - 1)
						t.Write(e.FFT[i].ToString());
					else
						t.Write(e.FFT[i].ToString() + ",");
				}
				t.Write(";");
				t.Write("\r\n");
			}
			t.Close();

			return true;
		}

		public bool ReadTableFromFile(string filename)
		{
			// make sure file exists first
			if (!File.Exists(filename))
				return false;

			// attempt to open the file
			using (TextReader t = new StreamReader(filename))
			{

				// clear table first
				Table.Clear();

				string line;
				string[] Fields, data;
				double[] numData;
				int i;

				SignatureEntry e;
				while ((line = t.ReadLine()) != null)
				{
					e = new SignatureEntry();

					// parse the line
					Fields = line.Split(';');
					if (Fields.Length < 3)
						continue;

					// get the name
					e.Name = Fields[0];

					// grab the number of samples
					e.NumSamples = System.Convert.ToInt32(Fields[1]);

					// get temp and batt life
					e.Temperature = System.Convert.ToUInt32(Fields[2]);
					e.BattLife = System.Convert.ToByte(Fields[3]);


					// grab the signal data
					data = Fields[4].Split(',');
					numData = new double[data.Length];
					i = 0;
					foreach (string d in data)
					{
						if (!d.Equals(""))
							numData[i++] = System.Convert.ToDouble(d);
					}
					e.Signal = numData;

					// grab the fft data
					data = Fields[5].Split(',');
					numData = new double[data.Length];
					i = 0;
					foreach (string d in data)
					{
						if (!d.Equals(""))
							numData[i++] = System.Convert.ToDouble(d);
					}
					e.FFT = numData;

					// add to the database
					AddSignature(e);
				}
			}

			return true;
		}


		// ****************************************
		// *********** Helper Functions ***********
		// ****************************************
		// this function will compute the chi-squared distance of two entries
		public double ComputeDistance(double[] entry1, double[] entry2)
		{
			int minLength = (entry1.Length < entry2.Length) ? entry1.Length : entry2.Length;
			double distance = 0;

			for (int i = 0; i < minLength; i++)
			{
				if ((entry1[i] != -entry2[i]) && !(entry1[i]==0 && entry2[i] == 0) )
					distance += Math.Pow((entry1[i] - entry2[i]), 2) / (entry1[i] + entry2[i]);
			}

			return Math.Abs(distance);
		}

		// this function will try to match the given signal with one from the existing
		// table.  It will return the name of the best match.
		public string Match(double[] signal, int numSamples)
		{
			double minDistance = 100000, sampleDiff;
			string bestMatch = "";
			foreach (SignatureEntry e in Table)
			{
				// check to make sure the number of pre-compression samples is close enough
				sampleDiff = Math.Abs((numSamples / MaxSamples) - (e.NumSamples / MaxSamples));
				if (sampleDiff > SampleDiffThreshold)
					continue;

				// compare battery temperature

				// compare battery life

				// compute and compare chi-squared distance
				double dist = ComputeDistance(signal, e.Signal);
				if (dist < minDistance)
				{
					minDistance = dist;
					bestMatch = e.Name;
				}
			}

			return bestMatch;
		}

		// this function will try to match the given fft with one from the existing
		// table.  It will return the name of the best match
		public string MatchFFT(double[] fft)
		{
			double minDistance = 100000;
			string bestMatch = "";
			foreach (SignatureEntry e in Table)
			{
				double dist = ComputeDistance(fft, e.FFT);

				if (dist < minDistance)
				{
					minDistance = dist;
					bestMatch = e.Name;
				}
			}

			return bestMatch;
		}

		// this is a public accessor for the match threshold value
		public void SetMatchThreshold(int newThreshold)
		{
			MatchThreshold = newThreshold;
		}
	}
}
