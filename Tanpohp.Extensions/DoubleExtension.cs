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

        /// <summary>
        /// Convert given degree to radian.
        /// </summary>
        /// <param name="degree">Degree to convert.</param>
        /// <returns></returns>
        public static double ToRadian(this double degree)
        {
            return degree * Math.PI / 180.0;
        }

        /// <summary>
        /// Convert given radian to degree.
        /// </summary>
        /// <param name="radian">Radian to convert.</param>
        /// <returns></returns>
        public static double ToDegree(this double radian)
        {
            return radian * 180.0 / Math.PI;
        } 
    }
}
