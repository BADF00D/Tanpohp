#region usings

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class ListExtensionTest
    {
        [Test]
        public void TestAddRange1()
        {
            IList<int> sequence = new List<int>();

            sequence.AddRange(new[] {1, 2, 3});

            Assert.AreEqual(3, sequence.Count);
            Assert.IsTrue(sequence.Contains(1));
            Assert.IsTrue(sequence.Contains(2));
            Assert.IsTrue(sequence.Contains(3));
        }

        [Test]
        public void TestPopFirst1()
        {
            IList<int> sequence = new List<int>(new []{1,2,3,4,5,6});

            var firstItem = sequence.PopFirst();

            Assert.AreEqual(1, firstItem);
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPopFirst2()
        {
            IList<int> sequence = new List<int>();

            sequence.PopFirst();
        }

        [Test]
        public void TestPopLast1()
        {
            IList<int> sequence = new List<int>(new[] { 1, 2, 3, 4, 5, 6 });

            var firstItem = sequence.PopLast();

            Assert.AreEqual(6, firstItem);
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPopLast2()
        {
            IList<int> sequence = new List<int>();

            sequence.PopLast();
        }
    }
}
