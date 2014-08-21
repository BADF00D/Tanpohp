using System;

namespace Tanpohp.Extensions
{
    public static class NumberExtension
    {
        /// <summary>
        /// Determines if given value is odd number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is odd, false otherwise.</returns>
        public static bool IsOdd(this Int32 value)
        {
            return value % 2 == 1;
        }

        /// <summary>
        /// Determines if given value is odd number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is odd, false otherwise.</returns>
        public static bool IsOdd(this Int16 value)
        {
            return value % 2 == 1;
        }

        /// <summary>
        /// Determines if given value is odd number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is odd, false otherwise.</returns>
        public static bool IsOdd(this Int64 value)
        {
            return value % 2 == 1;
        }

        /// <summary>
        /// Determines if given value is even number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsEven(this Int32 value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines if given value is even number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsEven(this Int16 value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines if given value is even number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsEven(this Int64 value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines if given value is power of two or 2^x with x as natural number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsPowerOfTwo(this Int32 value)
        {
            var copy = value;
            return ((copy >> 1) << 1) == value;
        }

        /// <summary>
        /// Determines if given value is power of two or 2^x with x as natural number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsPowerOfTwo(this Int16 value)
        {
            var copy = value;
            return ((copy >> 1) << 1) == value;
        }

        /// <summary>
        /// Determines if given value is power of two or 2^x with x as natural number.
        /// </summary>
        /// <param name="value">Value to evaluate.</param>
        /// <returns>True if number is even, false otherwise.</returns>
        public static bool IsPowerOfTwo(this Int64 value)
        {
            var copy = value;
            return ((copy >> 1) << 1) == value;
        }
    }
}
