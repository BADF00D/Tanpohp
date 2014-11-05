using System;
using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Mathematics.DistanceCalculation
{
	/// <summary>
	/// Calculate distance as between two strings as defined by Levenshtein.
	/// </summary>
	/// <remarks>Adapted algorithm from here: http://www.dotnetperls.com/levenshtein</remarks>
	public class LevenshteinDistanceCalculator : IStringDistanceCalculator
	{
		public int CalculateDistance([NotNull]string first, [NotNull]string second)
		{
			var lentghOfFirst = first.Length;
			var lengthOfSecond = second.Length;
			var costs = new int[lentghOfFirst + 1, lengthOfSecond + 1];

			if (lentghOfFirst == 0) return lengthOfSecond;
			if (lengthOfSecond == 0) return lentghOfFirst;

			for (var i = 0; i <= lentghOfFirst; costs[i, 0] = i++) ;
			for (var j = 0; j <= lengthOfSecond; costs[0, j] = j++) ;

			for (var i = 1; i <= lentghOfFirst; i++)
			{
				for (var j = 1; j <= lengthOfSecond; j++)
				{
					var cost = (second[j - 1] == first[i - 1]) ? 0 : 1;
					costs[i, j] = Math.Min(Math.Min(costs[i - 1, j] + 1, costs[i, j - 1] + 1), costs[i - 1, j - 1] + cost);
				}
			}

			return costs[lentghOfFirst, lengthOfSecond];
		}
	}
}