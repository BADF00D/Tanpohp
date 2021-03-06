﻿#region usings

using System;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class DoubleExtensionTest
    {
        [Test]
        public void TestInRange1()
        {
            const int lower = 1;
            const int upper = 2;
            const double value = 2.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(false, inRange);
        }

        [Test]
        public void TestInRange2()
        {
            const int lower = 1;
            const int upper = 2;
            const double value = 1.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(true, inRange);
        }

        [Test]
        public void TestInRange3()
        {
            const int lower = -1;
            const int upper = 2;
            const double value = 1.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(true, inRange);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FailInRange3()
        {
            const int lower = 3;
            const int upper = 2;
            const double value = 1.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(true, inRange);
        }

        [Test]
        public void TestToRadian1()
        {
            Assert.AreEqual(Math.PI, 180.0.ToRadian());
        }

        [Test]
        public void TestToDegree1()
        {
            Assert.AreEqual(180.0, Math.PI.ToDegree());
        }
    }
}
