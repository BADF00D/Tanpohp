#region usings

using System;
using System.Collections.Generic;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Adds values to given list. Port AddRange from List to IList
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List the item from value should be added to.</param>
        /// <param name="values">Values that should be added to list.</param>
        public static void AddRange<T>(this IList<T> list, [NotNull]IEnumerable<T> values)
        {
            if (list is List<T>) (list as List<T>).AddRange(values);
            else values.ForEach(list.Add);
        }

        /// <summary>
        /// Removes last item and returns it.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Item at last postion.</returns>
        /// <exception cref="ArgumentException">Thrown is list contains no elements.</exception>
        public static T PopLast<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new ArgumentException("List it empty.");
            var indexOfLast = list.Count - 1;
            var lastItem = list[indexOfLast];
            list.RemoveAt(indexOfLast);

            return lastItem;
        }

        /// <summary>
        /// Removes frist item and returns it.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Item at first postion.</returns>
        /// /// <exception cref="ArgumentException">Thrown is list contains no elements.</exception>
        public static T PopFirst<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new ArgumentException("List it empty.");
            var lastItem = list[0];
            list.RemoveAt(0);

            return lastItem;
        }
    }
}
