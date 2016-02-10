using System;
using System.Collections.Generic;
using System.Text;

namespace BatInfoLight
{
	
	/// <summary> 
	/// Replaces data[0..2*nn-1] by its discrete Fourier transform, if isign is input 
	/// as 1; or replaces data[0..2*nn-1] by nn times its inverse discrete Fourier  
	/// transform, if isign is input as -1. data is a complex array of length nn or,  
	/// equivalently, a real array of length 2*nn. nn MUST be an integer power of 2  
	/// (this is not checked for!). 

	public static class Fourier
	{
		private static double[] PadArray(double[] data, int size, out int newSize)
		{
			// get next power of 2 up from size
			int power = (int)Math.Ceiling(Math.Log(size) / Math.Log(2));

			// allocate new array and copy elements over
			double[] newData = new double[1 << power];
			for (int i = 0; i < size; i++)
			{
				newData[i] = data[i];
			}

			// pad with zeros
			for (int i = (1 << power) - 1; i >= size; i--)
			{
				newData[i] = 0;
			}

			newSize = 1 << power;
			return newData;
		}

		// this function will take an array of doubles and return an array
		// which is twice the size but split into (real,complex) pairs to prepare
		// for the fft.
		private static double[] PrepareArray(double[] data)
		{
			double[] newData = new double[2 * data.Length];

			for (int i = 0; i < data.Length; i++)
			{
				newData[2 * i] = data[i];
				newData[2 * i + 1] = 0;
			}

			return newData;
		}

		//data -> float array that represent the array of complex samples
		//number_of_complex_samples -> number of samples (N^2 order number) 
		//isign -> 1 to calculate FFT and -1 to calculate Reverse FFT
		public static void FFT(ref double[] data, ulong number_of_complex_samples, int isign)
		{
			// prepare the array first by making sure it has size of a power of 2
			// and that the odd indexes have value zero (complex part)
			data = PrepareArray(data);
			if (Math.IEEERemainder((Math.Log(data.Length) / Math.Log(2)), 1) != 0)
			{
				// pad the array with 0's
				int newSize = 0;
				data = PadArray(data, data.Length, out newSize);
				number_of_complex_samples = (ulong)(data.Length / 2);
			}
			else
				number_of_complex_samples = (ulong)(data.Length / 2);

			//variables for trigonometric recurrences
			ulong n, mmax, m, j, istep, i;
			double wtemp, wr, wpr, wpi, wi, theta, tempr, tempi;
			const double pi = 3.14159;

			//the complex array is real+complex so the array 
			//as a size n = 2* number of complex samples
			// real part is the data[index] and 
			//the complex part is the data[index+1]
			n = number_of_complex_samples * 2;

			//binary inversion (note that the indexes 
			//start from 0 witch means that the
			//real part of the complex is on the even-indexes 
			//and the complex part is on the odd-indexes
			j = 0;
			for (i = 0; i < n / 2; i += 2)
			{
				if (j > i)
				{
					//swap the real part
					SWAP(data[j], data[i]);
					//swap the complex part
					SWAP(data[j + 1], data[i + 1]);
					// checks if the changes occurs in the first half
					// and use the mirrored effect on the second half
					if ((j / 2) < (n / 4))
					{
						//swap the real part
						SWAP(data[(n - (i + 2))], data[(n - (j + 2))]);
						//swap the complex part
						SWAP(data[(n - (i + 2)) + 1], data[(n - (j + 2)) + 1]);
					}
				}
				m = n / 2;
				while (m >= 2 && j >= m)
				{
					j -= m;
					m = m / 2;
				}
				j += m;
			}

			//Danielson-Lanzcos routine 
			mmax = 2;
			//external loop
			while (n > mmax)
			{
				istep = mmax << 1;
				theta = isign * (2 * pi / mmax);
				wtemp = Math.Sin(0.5 * theta);
				wpr = -2.0 * wtemp * wtemp;
				wpi = Math.Sin(theta);
				wr = 1.0;
				wi = 0.0;
				//internal loops
				for (m = 1; m < mmax; m += 2)
				{
					for (i = m; i <= n; i += istep)
					{
						j = i + mmax;
						tempr = wr * data[j - 1] - wi * data[j];
						tempi = wr * data[j] + wi * data[j - 1];
						data[j - 1] = data[i - 1] - tempr;
						data[j] = data[i] - tempi;
						data[i - 1] += tempr;
						data[i] += tempi;
					}
					wr = (wtemp = wr) * wpr - wi * wpi + wr;
					wi = wi * wpr + wtemp * wpi + wi;
				}
				mmax = istep;
			}
		}

		public static void SWAP(double a, double b)
		{
			double tmp = a;
			a = b;
			b = tmp;
		}

		public static void four1(ref double[] data, int nn, int isign) // nn was ulong
		{
			// make sure the array has size of a power of two
			if (Math.IEEERemainder((Math.Log(nn) / Math.Log(2)), 1) != 0)
			{
				// pad the array with 0's
				int newSize = 0;
				data = PadArray(data, nn, out newSize);
				nn = newSize / 2;
			}
			else
				nn = nn / 2;

			int n, mmax, m, j, istep, i;		// was type ulong
			double wtemp, wr, wpr, wpi, wi, theta;
			double tempr, tempi;

			n = nn << 1;
			j = 1;
			for (i = 1; i < n; i += 2)
			{
				if (j > i)
				{
					tempr = data[j - 1];
					data[j - 1] = data[i - 1];
					data[i - 1] = tempr;		// SWAP (data[j],data[i]);
					tempi = data[j];
					data[j] = data[i];
					data[i] = tempi;			// SWAP(data[j+1],data[i+1]); 
				}
				m = n >> 1;
				while (m >= 2 && j > m)
				{
					j -= m;
					m >>= 1;
				}
				j += m;
			}
			mmax = 2;
			while (n > mmax)
			{

				istep = mmax << 1;
				theta = isign * (6.28318530717959 / mmax);
				wtemp = Math.Sin(0.5 * theta);
				wpr = -2.0 * wtemp * wtemp;
				wpi = Math.Sin(theta);
				wr = 1.0;
				wi = 0.0;
				for (m = 1; m < mmax; m += 2)
				{

					for (i = m; i <= n; i += istep)
					{
						j = i + mmax;
						tempr = wr * data[j - 1] - wi * data[j];
						tempi = wr * data[j] + wi * data[j - 1];
						data[j - 1] = data[i - 1] - tempr;
						data[j] = data[i] - tempi;
						data[i - 1] += tempr; data[i] += tempi;
					}
					wr = (wtemp = wr) * wpr - wi * wpi + wr;
					wi = wi * wpr + wtemp * wpi + wi;
				}
				mmax = istep;
			}
		}
	}
}
