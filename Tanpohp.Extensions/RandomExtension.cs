#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Tanpohp.Extensions
{
    public static class RandomExtension
    {
        /// <summary>
        /// Returns a random item of given paramter.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="random">Random instance.</param>
        /// <param name="paramter">Parameters a random one should be choosen off.</param>
        /// <returns></returns>
        public static T RandomOf<T>(this Random random, params T[] paramter)
        {
            return random.RandomOf((ICollection<T>)paramter);
        }

        /// <summary>
        /// Returns a random item of given collection.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="random">Random instance.</param>
        /// <param name="collection">Collection a random item should be choosen off.</param>
        /// <returns></returns>
        public static T RandomOf<T>(this Random random, ICollection<T> collection)
        {
            return collection.ItemAt(random.Next(collection.Count));
        }
    }
}