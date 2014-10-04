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

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static byte Clamp(this byte value, byte minValue, byte maxValue)
        {
            if(maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static short Clamp(this short value, short minValue, short maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static long Clamp(this long value, long minValue, long maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static double Clamp(this double value, double minValue, double maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }

        /// <summary>
        /// Clamps value inbetween given bounds.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minValue">Lower bound.</param>
        /// <param name="maxValue">Upper bound.</param>
        /// <returns></returns>
        public static float Clamp(this float value, float minValue, float maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentOutOfRangeException("Given bounds are wrong.");

            if (value < minValue) return minValue;
            return value > maxValue ? maxValue : value;
        }
    }
}
