#region usings

using System;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class DateTimeExtensionTest
    {
        [Test]
        public static void IsFutureWithNow()
        {
            Assert.IsFalse(DateTime.Now.IsFuture());
        }

        [Test]
        public static void IsFutureWithYear3000()
        {
            Assert.IsTrue((new DateTime(3000, 1, 1)).IsFuture());
        }

        [Test]
        public static void IsFutureWithYear1000()
        {
            Assert.IsFalse((new DateTime(1000, 1, 1)).IsFuture());
        }

        [Test]
        public static void IsPastTest0()
        {
            Assert.IsFalse(DateTime.Now.IsPast());
        }

        [Test]
        public static void IsPastTest1()
        {
            Assert.IsFalse((new DateTime(3000, 1, 1)).IsPast());
        }

        [Test]
        public static void IsPastTest2()
        {
            Assert.IsTrue((new DateTime(1000, 1, 1)).IsPast());
        }
    }
}