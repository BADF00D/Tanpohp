using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tanpohp.Extensions;

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class ComparableExtensionTest
    {
        [Test]
        public void IsInRangeWithIntTest0()
        {
            Assert.IsTrue(0.InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithIntTest1()
        {
            Assert.IsTrue(1.InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithIntTest2()
        {
            Assert.IsFalse(2.InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithIntTest3()
        {
            Assert.IsFalse((-2).InRangeOf(-1, 1));
        }

        [Test]
        public void IsInRangeWithDoubleTest0()
        {
            Assert.IsTrue((0d).InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithDoubleTest1()
        {
            Assert.IsTrue(1d.InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithDoubleTest2()
        {
            Assert.IsFalse(2d.InRangeOf(-1, 1));
        }
        [Test]
        public void IsInRangeWithDoubleTest3()
        {
            Assert.IsFalse((-2d).InRangeOf(-1, 1));
        }
    }
}
