using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Tanpohp.Diagnostics;

namespace Tanpohp.DiagnosticsTest
{
	[TestFixture]
	public class MultiStopwatchTest
	{
		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void ElapsedWithExistingStopwatch()
		{
			var msw = new MultiStopwatch();
			msw.Start("test");

			var elapsedTimeSpan = msw.Elapsed("test");

			Assert.IsTrue(elapsedTimeSpan.Ticks>=0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ElapsedWithNotExistingStopwatch()
		{
			var msw = new MultiStopwatch();

			msw.Elapsed("test");
		}

		[Test]
		public void IsRunningWithExistingStopwatch()
		{
			var msw = new MultiStopwatch();
			msw.Start("test");

			Assert.IsTrue(msw.IsRunning("test"));
		}

		[Test]
		public void IsRunningWithNotExistingStopwatch()
		{
			var msw = new MultiStopwatch();

			Assert.IsFalse(msw.IsRunning("test"));
		}

		[Test]
		public void StartSingleStopwatch()
		{
			var msw = new MultiStopwatch();
			var sw = msw.Start("test");

			Assert.IsTrue(sw.IsRunning);
		}

		[Test]
		public void StartTwoStopwatchedWithSameIdWhereFirstIsStopped()
		{
			var msw = new MultiStopwatch();
			var sw = msw.Start("test");
			sw.Stop();
			msw.Start("test");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StartTwoStopwatchedWithSameIdWhereFirstIsRunning()
		{
			var msw = new MultiStopwatch();
			var sw = msw.Start("test");
			msw.Start("test");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StartTwoStopwatchedWithSameIdBuildOfTwoString()
		{
			var msw = new MultiStopwatch();
			msw.Start("test");

			var stringBuilder = new StringBuilder();
			stringBuilder.Append("te");
			stringBuilder.Append("st");
			msw.Start(stringBuilder.ToString());
		}

		[Test]
		public void RestartExisting()
		{
			var msw = new MultiStopwatch();
			msw.Start("test");
			msw.Restart("test");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void RestartNotExisting()
		{
			var msw = new MultiStopwatch();
			msw.Restart("test");
		}

		[Test]
		public void StopExistingThatRuns()
		{
			var msw = new MultiStopwatch();
			var start = msw.Start("test");
			var stop = msw.Stop("test");

			Assert.AreEqual(start, stop);
			Assert.IsFalse(stop.IsRunning);
		}

		[Test]
		public void StopExistingThatDoesNotRuns()
		{
			var msw = new MultiStopwatch();
			var start = msw.Start("test");
			start.Stop();
			var stop = msw.Stop("test");

			Assert.AreEqual(start, stop);
			Assert.IsFalse(stop.IsRunning);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StopNotExisting()
		{
			var msw = new MultiStopwatch();
			
			msw.Stop("test");
		}

		[Test]
		public void RestartExistingThatRuns()
		{
			var msw = new MultiStopwatch();
			msw.Start("test");
			Thread.Sleep(100);
			var elapsedBeforeRestart = msw.Elapsed("test");
			msw.Restart("test");
			var elapsedAfterRestart = msw.Elapsed("test");
			//this fails if Restart and Elapsed takes longer then 100ms, which can habben while debugging.
			Assert.Less(elapsedAfterRestart, elapsedBeforeRestart);
		}
	}
}
