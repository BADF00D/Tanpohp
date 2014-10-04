using System;
namespace Tanpohp.Extensions
{
    public static class ComparableExtension
    {
        /// <summary>
        /// Determines weather item (e.g. a number) is in between lower and upper, or equals one of them.
        /// </summary>
        /// <typeparam name="T">Generic paramter that implements IComparable<T>.</typeparam>
        /// <param name="actual">Actual value to test.</param>
        /// <param name="lower">Lower bound.</param>
        /// <param name="upper">Upper bound.</param>
        /// <returns></returns>
        public static bool InRangeOf<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        }

        /// <summary>
        /// Determines weather item (e.g. a number) is smaller then lower or bigger then upper.
        /// </summary>
        /// <typeparam name="T">Generic paramter that implements IComparable<T>.</typeparam>
        /// <param name="actual">Actual value to test.</param>
        /// <param name="lower">Lower bound.</param>
        /// <param name="upper">Upper bound.</param>
        /// <returns></returns>
        public static bool NotInRangeOf<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return !InRangeOf(actual, lower, upper);
        }
    }
}
