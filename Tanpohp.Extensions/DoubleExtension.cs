#region usings

using System;

#endregion

namespace Tanpohp.Extensions
{
    public static class DoubleExtension
    {
        /// <summary>
        /// Determines whether value is in between a specific range defined by [lower, upper].
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="lower">Lower border of range.</param>
        /// <param name="upper">Upper order of range.</param>
        /// <returns></returns>
        public static bool InRange(this double value, double lower, double upper)
        {
            if (lower > upper) throw new ArgumentException("Lower has to be lower or equals to upper.");
            return value.CompareTo(lower) != -1 && value.CompareTo(upper) != 1;
        }
    }
}
