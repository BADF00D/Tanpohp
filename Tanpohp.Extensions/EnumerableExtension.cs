#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tanpohp.Annotations.Resharper;

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
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> sequence, [NotNull]Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }
        /// <summary>
        /// Checks whether source contains a element the condition (evaluator) matches.
        /// This method is an alias for extension method Any.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="predicate">Predicate that should match.</param>
        /// <returns>True if there is an item that matches predicate.</returns>
        [DebuggerStepThrough]
        public static bool Contains<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            return source.Any(item => predicate(item));
        }

        /// <summary>
        /// Converts enumerable into a string by walking over it and calling propertySelector on each item.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="sequence">Sequence to walk through.</param>
        /// <param name="propertySelector">Function that converts item T into a its string representation.</param>
        /// <param name="seperator">Seperator between each item in final string.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ItemsToString<T>(
            this IEnumerable<T> sequence, Func<T, string> propertySelector, string seperator = ";")
        {
            var stringBuilder = new StringBuilder(200);
            foreach (var item in sequence)
            {
                stringBuilder.Append(propertySelector(item));
                stringBuilder.Append(seperator);
            }
            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts enumerable into a string by walking over it and calling ToString on each item.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="sequence">Sequence to walk through.</param>
        /// <param name="seperator">Seperator between each item in final string.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ItemsToString<T>(this IEnumerable<T> sequence, string seperator = ";")
        {
            return ItemsToString(sequence, i => i.ToString(), seperator);
        }

        /// <summary>
        /// Converts a enumerable to a HashSet.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="source">Source enumeation that should be used.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Groups all items from source into a dictionary, where all items with the same key are stored in a list.
        /// </summary>
        /// <typeparam name="TKey">Type that should be key in resulting dictionary.</typeparam>
        /// <typeparam name="TSource">Type of item in source.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="keySelector">Selector for the key that is invoked in each item.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Dictionary<TKey, List<TSource>> ToDictionaryGroupedBy<TKey, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<TSource>>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (result.ContainsKey(key) == false)
                    result.Add(key, new List<TSource>());

                result[key].Add(item);
            }

            return result;
        }

        /// <summary>
        /// Checks whether each item only occure one within the collection using the default equality operator to compare values.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <returns>True if collection only contains unique items, false otherwise.</returns>
        /// <remarks>Remember to correctly overwrite Equals and GetHashCode methods of T</remarks>
        [DebuggerStepThrough]
        public static bool ContainsOnlyUniqueItems<T>(this IEnumerable<T> source)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return enumerable.Distinct().Count() == enumerable.Count();
        }

        /// <summary>
        /// Takes the first 'count' elements from the sequence.
        /// </summary>
        /// <typeparam name="T">Type of the sequence items.</typeparam>
        /// <param name="sequence">Sequence to take items from.</param>
        /// <param name="count">Maximum number of items to take start from sequence. If squence contains less then 'count' items, it returns the whole sequence as is.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> TakeFirst<T>(this IEnumerable<T> sequence, int count)
        {
            if (count < 0) throw new ArgumentException("Invalid count of elements. Parameter count should be biggern or equals then 0.");
            foreach (var item in sequence)
            {
                if (count-- > 0) yield return item;
                else break;
            }
            if(count > 0) throw new IndexOutOfRangeException("Count is bigger then number of eleemtns in sequence.");
        }

        /// <summary>
        /// Takes the last 'count' elements from the sequence.
        /// </summary>
        /// <typeparam name="T">Type of the sequence items.</typeparam>
        /// <param name="sequence">Sequence to take items from.</param>
        /// <param name="count">Maximum number of items to take from end sequence. If squence contains less then 'count' items, it returns the whole sequence as is.</param>
        /// <param name="preserveOrder">True items have same order as they came in.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> sequence, int count, bool preserveOrder = false)
        {
            var result = TakeFirst(sequence.Reverse(), count);

            return preserveOrder ? result.Reverse() : result;
        }

        /// <summary>
        /// Alias for !sequence.Any()
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="sequence">Source sequence.</param>
        /// <returns>True if source is empty, false otherwise.</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> sequence)
        {
            return !sequence.Any();
        }

        /// <summary>
        /// Enumerates through the sequence and return element a specified position.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="sequence">Source sequnce.</param>
        /// <param name="position">Zero based postion of item to return.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T ItemAt<T>(this IEnumerable<T> sequence, int position)
        {
            if(position < 0) throw new ArgumentException("Parameter postion must be bigger or equals then 0.");
            try
            {
                return sequence.Skip(position).First();
            }
            catch (InvalidOperationException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks if none of the given elements satisfy the condition.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="sequence">Sequence to check.</param>
        /// <param name="condition">Condition to check.</param>
        /// <returns>True if no element satisfy the condition, false otherwise.</returns>
        [DebuggerStepThrough]
        public static bool Non<T>(this IEnumerable<T> sequence, Func<T, bool> condition )
        {
            return !sequence.Any(condition);
        }
    }
}
