﻿using System;
using System.Text;
using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Simply wrapps string.Format method.
        /// </summary>
        /// <param name="format">String with format.</param>
        /// <param name="parameter">Arrya with parameter.</param>
        /// <returns></returns>
        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] parameter)
        {
            return string.Format(format, parameter);
        }

        /// <summary>
        /// This method tries to find the n-th occurance (zero based) of the given string match. 
        /// </summary>
        /// <param name="string">String to search in.</param>
        /// <param name="match">String to find.</param>
        /// <param name="occurance">Occurance the index should be searched.</param>
        /// <param name="index">Resulting (zero based)index if return is true.</param>
        /// <returns>True, if index could be found, false otherwise.</returns>
        public static bool TryFindNthIndexOf(this string @string, string match, uint occurance, out int index)
        {
            index = 0;
            var i = 1;
            while(i <= occurance && (index = @string.IndexOf(match, index +1)) != -1)
            {
                if (i == occurance) return true;
                i++;
            }
            return false;
        }

        /// <summary>
        /// Removes circle, square and curly brackets from given string. 
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Contens of source string without brackets.</returns>
        public static string RemoveBraces(this string source)
        {
            return RemoveChar(source, c => c.IsBracket());
        }

        /// <summary>
        /// Removes all digis from string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns></returns>
        public static string RemoveDigit(this string source)
        {
            return RemoveChar(source, c => char.IsDigit(c));
        }

        /// <summary>
        /// Removes all characters that satisfy predicate. 
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="shouldBeRemoved">Predicate to satisfy.</param>
        /// <returns></returns>
        public static string RemoveChar(this string source, Predicate<char> shouldBeRemoved)
        {
            var result = new StringBuilder(source.Length);
            source.ToCharArray().ForEach(c =>{
                                                 if (!shouldBeRemoved(c)) result.Append(c);
                                             });

            return result.ToString();
        }

        /// <summary>
        /// Removes all letters from source string. 
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns></returns>
        public static string RemoveLetter(this string source)
        {
            return RemoveChar(source, c => char.IsLetter(c));
        }
    }
}
