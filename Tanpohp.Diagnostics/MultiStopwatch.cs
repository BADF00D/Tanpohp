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
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <returns>The specified stopwatch.</returns>
		public Stopwatch this[String identifier]
		{
			get
			{
				if (!_stopwatches.ContainsKey(identifier.GetHashCode())) return null;
				return _stopwatches[identifier.GetHashCode()];
			}
			set
			{
				if (value == null) throw new NullReferenceException("Value is null.");
				_stopwatches[identifier.GetHashCode()] = value;
			}
		}


		/// <summary>
		/// Gets the total elapsed time measured by the current instance.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <returns>A read-only TimeSpan representing the total elapsed time measured by the current instance.</returns>
		/// /// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public TimeSpan Elapsed(String identifier)
		{
			var key = identifier.GetHashCode();
			if (!_stopwatches.ContainsKey(key)) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			return stopwatch.Elapsed;
		}

		/// <summary>
		/// Gets a value indicating whether the Stopwatch timer is running.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <returns>true if the Stopwatch instance is currently running and measuring elapsed time for an interval; otherwise, false.</returns>
		public bool IsRunning(String identifier)
		{
			var id = identifier.GetHashCode();
			if (!_stopwatches.ContainsKey(id)) return false;
			return _stopwatches[id].IsRunning;
		}

		/// <summary>
		/// Starts, or resumes, measuring elapsed time for an interval.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch and is used as output text.</param>
		public Stopwatch Start(String identifier)
		{
			var hash = identifier.GetHashCode();
			var stopwatch = _stopwatches.ContainsKey(hash) ? _stopwatches[hash] : new Stopwatch();
			if(stopwatch.IsRunning)
			{
				throw new ArgumentException("A stopwatch with this id exists already. Use Stop() first or Restart() instead.");
			}
			
			stopwatch.Start();
			_stopwatches[hash] = stopwatch;
			return stopwatch;
		}

		/// <summary>
		/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public Stopwatch Restart(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			stopwatch.Reset();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>
		/// Stops measuring elapsed time for an interval.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public Stopwatch Stop(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			stopwatch.Stop();
			return stopwatch;
		}

		/// <summary>
		/// Stops time interval measurement and resets the elapsed time to zero.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public Stopwatch Reset(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
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
		/// <param name="identifier">Identifing the used Stopwatch and is used as output text.</param>
		public void StartPrint(String identifier)
		{
			Start(identifier);
			_output.WriteLine(String.Format("Start   :{0}", identifier));
		}

		/// <summary>
		/// Stops measuring elapsed time for an interval. After all ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public void StopPrint(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var sw = _stopwatches[identifier.GetHashCode()];
			sw.Stop();
			_output.WriteLine(String.Format("Stop    ({0:00000}ms): {1:G}", sw.ElapsedMilliseconds, identifier));
		}

		/// <summary>
		/// Stops time interval measurement and resets the elapsed time to zero. After all ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public void ResetPrint(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			stopwatch.Stop();
			_output.WriteLine(String.Format("Reset   ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, identifier));
			stopwatch.Reset();
		}

		/// <summary>
		/// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time. ElapsedMilliseconds is printed out.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public void RestartPrint(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			_output.WriteLine(String.Format("Restart ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, identifier));
			stopwatch.Stop();
			stopwatch.Reset();
			stopwatch.Start();
		}

		/// <summary>
		/// Prints out the current time in ms.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public long ElapsedMillisecoundsPrint(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			_output.WriteLine(String.Format("Current ({0:00000}ms): {1}", stopwatch.ElapsedMilliseconds, identifier));
			return stopwatch.ElapsedMilliseconds;
		}

		/// <summary>
		/// Gets the total elapsed time measured by the current instance, in timer ticks and prints it out.
		/// </summary>
		/// <param name="identifier">Identifing the used Stopwatch.</param>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		/// <exception cref="ArgumentException">Thrown if stopwatch with given identifier does not exists.</exception>
		public long ElapsedTicksPrint(String identifier)
		{
			if (!_stopwatches.ContainsKey(identifier.GetHashCode())) throw new ArgumentException("Stopwatch does not exists.");
			var stopwatch = _stopwatches[identifier.GetHashCode()];
			_output.WriteLine(String.Format("Current ({0:00000}ms): {1}", stopwatch.ElapsedTicks, identifier));
			return stopwatch.ElapsedTicks;
		}
	}
}