#region usings

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    /// <summary>
    /// This test uses Random class and therefor might fail if implementation of Random changed. 
    /// </summary>
    [TestFixture]
    public class RandomExtensionTest
    {
        [Test]
        public void RandomOfCollection()
        {
            var random = new Random(0);
            var collection = new[] {0, 1, 2, 3, 4, 5};
            var set = new HashSet<int>();
            for (var i = 0; i < 10000; i++)
            {
                set.Add(random.RandomOf(collection));
            }
            Assert.AreEqual(6, set.Count);
        }

        [Test]
        public void RandomOfParameter()
        {
            var random = new Random(0);
            var set = new HashSet<int>();
            for (var i = 0; i < 10000; i++)
            {
                set.Add(random.RandomOf(0, 1, 2, 3, 4, 5));
            }
            Assert.AreEqual(6, set.Count);
        }
    }
}