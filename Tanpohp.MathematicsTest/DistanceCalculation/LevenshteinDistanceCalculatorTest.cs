#region usings

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Extensions;
using Tanpohp.Mathematics.DistanceCalculation;

#endregion

namespace Tanpohp.MathematicsTest.DistanceCalculation
{
	[TestFixture]
	internal class LevenshteinDistanceCalculatorTest
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			_calculator = new LevenshteinDistanceCalculator();
		}

		#endregion

		private IStringDistanceCalculator _calculator;

		private static IEnumerable<Tuple<string, string, int>> GetExpectedResults()
		{
			return new List<Tuple<string, string, int>>
			       	{
			       		new Tuple<string, string, int>("test", "test", 0),
			       		new Tuple<string, string, int>("", "", 0),
			       		new Tuple<string, string, int>("1", "2", 1),
			       		new Tuple<string, string, int>("Sam", "Samantha", 5),
			       		new Tuple<string, string, int>("Samantha", "Sam", 5),
			       	};
		}

		[Test]
		public void TestWithPredefinedResults()
		{
			var expectedResults = GetExpectedResults();

			foreach (var expectedResult in expectedResults)
			{
				var distance = _calculator.CalculateDistance(expectedResult.Item1, expectedResult.Item2);
				Assert.AreEqual(expectedResult.Item3, distance, "'{0}'-'{1}'".FormatWith(expectedResult.Item1, expectedResult.Item2));
			}
		}
	}
}