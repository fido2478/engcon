using System;
using System.Collections.Generic;
using System.Text;

namespace BatInfoLight
{
	public class Filter
	{
		private double[] Result;
		//private int k;

		public Filter(ref Queue<int> d)
		{
		}

		public static double StandardDeviation(double[] data)
		{

			double ret = 0;
			double DataAverage = 0;
			double TotalVariance = 0;
			double Max = 0;

			try
			{

				Max = data.Length;

				if (Max == 0) { return ret; }

				DataAverage = Mean(ref data);

				for (int i = 0; i < Max; i++)
				{
					TotalVariance += Math.Pow(data[i] - DataAverage, 2);
				}

				ret = Math.Sqrt(TotalVariance / Max);

			}
			catch (Exception) { throw; }
			return ret;
		}

		// this function applies a moving average filter to the data
		// and returns it as an array
		public static double[] AvgFilter(ref Queue<int> Data, int k)
		{
			double[] ArrData = { 0.0 };
			return AvgFilter(ref ArrData, k);
		}

		// this function applies a moving average filter to the data
		// and returns it as an array
		public static double[] AvgFilter(ref double[] Data, int k)
		{
			double[] Result;

			int length = Data.GetLength(0);
			double avg = 0;
			Result = new double[length - 2 * k];

			if ((2 * k + 1) > length)
				return Result;

			// iterate through and apply filter
			for (int i = k; i < (length - k); i++)
			{
				avg = 0;
				for (int j = i - k; j <= (i + k) && j < length; j++)
				{
					avg += Data[j];
				}

				Result[i - k] = avg / (2 * k + 1);
			}

			return Result;
		}

		// this function will compress the array
		// l is length of lookahead window, k is threshhold
		public static double[] Compress(ref double[] data, int l, int k)
		{
			if (data.Length < l)
				return data;

			double[] chunk = new double[l];
			List<double> dest = new List<double>();
			int length = data.Length;

			for (int i = 0; i < (length - l); i += l)
			{
				// grab next chunk
				for (int j = i; (j - i) < l; j++)
				{
					chunk[j - i] = data[j];
				}

				// calculate stddev, if stddev < k then add mean
				// to the compressed array, else just add the chunk
				double stdDev = StandardDeviation(chunk);
				if (stdDev < k)
				{
					// calculate mean
					double mean = Mean(ref chunk);
					dest.Add(mean);
				}
				else
					dest.AddRange(chunk);

			}

			return dest.ToArray();

		}

		// this just calculates arithmetic mean
		public static double Mean(ref double[] data)
		{
			double result = 0;
			for (int i = 0; i < data.Length; i++)
				result += data[i];

			return (result / data.Length);
		}
	}
}
