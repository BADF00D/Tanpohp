using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanpohp.Extensions
{
    public static class CharExtension
    {
        /// <summary>
        /// Checks whether given char is a circle, curly or square bracket.
        /// </summary>
        /// <param name="char">Char to check.</param>
        /// <returns></returns>
        public static bool IsBracket(this char @char)
        {
            return @char == '(' || @char == ')' || @char == '{' || @char == '}' || @char == '[' || @char == ']';
        } 

        /// <summary>
        /// Checks whether give char is a curly bracket ("{" or"}").
        /// </summary>
        /// <param name="char">Char to check.</param>
        /// <returns></returns>
        public static bool IsCurlyBracket(this char @char)
        {
            return @char == '{' || @char == '}';
        }

        /// <summary>
        /// Checks whether give char is a square bracket ("[" or"]").
        /// </summary>
        /// <param name="char">Char to check.</param>
        /// <returns></returns>
        public static bool IsSquareBracket(this char @char)
        {
            return @char == '[' || @char == ']';
        }

        /// <summary>
        /// Checks whether give char is a circle bracket ("(" or")").
        /// </summary>
        /// <param name="char">Char to check.</param>
        /// <returns></returns>
        public static bool IsCircleBracket(this char @char)
        {
            return @char == '(' || @char == ')';
        }
    }
}
