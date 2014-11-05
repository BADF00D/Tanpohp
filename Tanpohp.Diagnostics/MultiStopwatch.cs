#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#endregion

namespace Tanpohp.Diagnostics
{
	public class MultiStopwatch
	{
		/// <summary>
		/// Dictonary storing all stopwatches.
		/// </summary>
		private readonly Dictionary<int, Stopwatch> _stopwatches;

		/// <summary>
		/// Output where "Print"-methodes write data.
		/// </summary>
		private TextWriter _output = Console.Out;

		/// <summary>
		/// Initializes a new instance of the MultiStopwatch class with 16 Stopwatches.
		/// </summary>
		public MultiStopwatch()
		{
			_stopwatches = new Dictionary<int, Stopwatch>(16);
		}

		/// <summary>
		/// Initializes a new instance of the MultiStopwatch class.
		/// </summary>
		/// <param name="outputWriter"></param>
		public MultiStopwatch(TextWriter outputWriter)
			: this()
		{
			OutputWriter = outputWriter;
		}

		public TextWriter OutputWriter
		{
			get { return _output; }
			set
			{
				if (value == null) throw new NullReferenceException("OutputWriter instance is null.");
				_output = value;
			}
		}

		/// <summary>
		/// Returns a specific Stopwatch if it exists.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>The specified stopwatch.</returns>
		public Stopwatch this[String s]
		{
			get
			{
				if (!_stopwatches.ContainsKey(s.GetHashCode())) return null;
				return _stopwatches[s.GetHashCode()];
			}
			set
			{
				if (value == null) throw new NullReferenceException("Value is null.");
				_stopwatches[s.GetHashCode()] = value;
			}
		}


		/// <summary>
		/// Gets the total elapsed time measured by the current instance.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>A read-only TimeSpan representing the total elapsed time measured by the current instance.</returns>
		public TimeSpan Elapsed(String s)
		{
			var stopwatch = _stopwatches[s.GetHashCode()];
			if (stopwatch == null) throw new Exception("Stopwatch does not exists.");
			return stopwatch.Elapsed;
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in milliseconds.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		public long ElapsedMillisecounds(String s)
		{
			var stopwatch = _stopwatches[s.GetHashCode()];
			if (stopwatch == null) throw new Exception("Stopwatch does not exists.");
			return stopwatch.ElapsedMilliseconds;
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in timer ticks.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		public long ElapsedTicks(String s)
		{
			var stopwatch = _stopwatches[s.GetHashCode()];
			if (stopwatch == null) throw new Exception("Stopwatch does not exists.");
			return stopwatch.ElapsedTicks;
		}

		/// <summary>
		/// Gets a value indicating whether the Stopwatch timer is running.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>true if the Stopwatch instance is currently running and measuring elapsed time for an interval; otherwise, false.</returns>
		public bool IsRunning(String s)
		{
			var stopwatch = _stopwatches[s.GetHashCode()];
			if (stopwatch == null) throw new Exception("Stopwatch does not exists.");
			return stopwatch.IsRunning;
		}

		/// <summary>
		/// Starts, or resumes, measuring elapsed time for an interval.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch and is used as output text.</param>
		public Stopwatch Start(String s)
		{
			var hash = s.GetHashCode();
			var stopwatch = _stopwatches.ContainsKey(hash) ? _stopwatches[hash] : new Stopwatch();
			stopwatch.Start();
			_stopwatches[s.GetHashCode()] = stopwatch;
			return stopwatch;
		}

		/// <summary>
		/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public Stopwatch Restart(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			stopwatch.Reset();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>
		/// Stops measuring elapsed time for an interval.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public Stopwatch Stop(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			stopwatch.Stop();
			return stopwatch;
		}

		/// <summary>
		/// Stops time interval measurement and resets the elapsed time to zero.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public Stopwatch Reset(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			stopwatch.Reset();
			return stopwatch;
		}

		/// <summary>
		/// Returns a String that represents the current Object. (Inherited from Object.)
		/// </summary>
		/// <returns>A String that represents the current Object.</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();

			foreach (var kv in _stopwatches)
			{
				sb.AppendFormat("{%s} ", kv.Value.ToString());
			}
			return sb.ToString();
		}

		/// <summary>
		/// Starts, or resumes, measuring elapsed time for an interval. After all ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch and is used as output text.</param>
		public void StartPrint(String s)
		{
			Start(s);
			_output.WriteLine(String.Format("Start   :{0}", s));
		}

		/// <summary>
		/// Stops measuring elapsed time for an interval. After all ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public void StopPrint(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var sw = _stopwatches[s.GetHashCode()];
			sw.Stop();
			_output.WriteLine(String.Format("Stop    ({0:00000}ms): {1:G}", sw.ElapsedMilliseconds, s));
		}

		/// <summary>
		/// Stops time interval measurement and resets the elapsed time to zero. After all ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public void ResetPrint(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			stopwatch.Stop();
			_output.WriteLine(String.Format("Reset   ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, s));
			stopwatch.Reset();
		}

		/// <summary>
		/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time. ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		public void RestartPrint(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			_output.WriteLine(String.Format("Restart ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, s));
			stopwatch.Stop();
			stopwatch.Reset();
			stopwatch.Start();
		}

		/// <summary>
		/// Prints out the current time in ms.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		public long ElapsedMillisecoundsPrint(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			_output.WriteLine(String.Format("Current ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, s));
			return stopwatch.ElapsedMilliseconds;
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in timer ticks and prints it out.
		/// </summary>
		/// <param name="s">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		public long ElapsedTicksPrint(String s)
		{
			if (!_stopwatches.ContainsKey(s.GetHashCode())) throw new Exception("Stopwatch does not exists.");
			var stopwatch = _stopwatches[s.GetHashCode()];
			_output.WriteLine(String.Format("Current ({0:00000}ms): {1}", stopwatch.ElapsedTicks, s));
			return stopwatch.ElapsedTicks;
		}
	}
}