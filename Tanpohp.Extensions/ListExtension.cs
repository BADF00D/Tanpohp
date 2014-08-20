#region usings

using System.Collections.Generic;
using ScreenDimmer.Annotations;

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
    }
}
