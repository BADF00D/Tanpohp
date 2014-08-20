#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Tanpohp.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Walks through the sequence and calls action with each item in it.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="sequence">Sequence to walk through.</param>
        /// <param name="action">Action to perform on each item.</param>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }
    }
}
