#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class NumberExtensionTest
    {
        [Test]
        public void TestIsEvenWithShort1()
        {
            Assert.IsTrue(((short)2).IsEven());
        }
        [Test]
        public void TestIsEvenWithShort2()
        {
            Assert.IsFalse(((short)3).IsEven());
        }
        [Test]
        public void TestIsEvenWithShort3()
        {
            Assert.IsTrue(((short)0).IsEven());
        }

        [Test]
        public void TestIsEvenWithInt1()
        {
            Assert.IsTrue((2).IsEven());
        }
        [Test]
        public void TestIsEvenWithInt2()
        {
            Assert.IsFalse((3).IsEven());
        }
        [Test]
        public void TestIsEvenWithInt3()
        {
            Assert.IsTrue((0).IsEven());
        }

        [Test]
        public void TestIsEvenWithLong1()
        {
            Assert.IsTrue(2L.IsEven());
        }
        [Test]
        public void TestIsEvenWithLong2()
        {
            Assert.IsFalse(3L.IsEven());
        }
        [Test]
        public void TestIsEvenWithLong3()
        {
            Assert.IsTrue(0L.IsEven());
        }

        [Test]
        public void TestIsOddWithShort1()
        {
            Assert.IsFalse(((short)2).IsOdd());
        }
        [Test]
        public void TestIsOddWithShort2()
        {
            Assert.IsTrue(((short)3).IsOdd());
        }
        [Test]
        public void TestIsOddWithShort3()
        {
            Assert.IsFalse(((short)0).IsOdd());
        }

        [Test]
        public void TestIsOddWithInt1()
        {
            Assert.IsFalse((2).IsOdd());
        }
        [Test]
        public void TestIsOddWithInt2()
        {
            Assert.IsTrue((3).IsOdd());
        }
        [Test]
        public void TestIsOddWithInt3()
        {
            Assert.IsFalse((0).IsOdd());
        }

        [Test]
        public void TestIsOddWithLong1()
        {
            Assert.IsFalse(2L.IsOdd());
        }
        [Test]
        public void TestIsOddWithLong2()
        {
            Assert.IsTrue(3L.IsOdd());
        }
        [Test]
        public void TestIsOddWithLong3()
        {
            Assert.IsFalse(0L.IsOdd());
        }

        [Test]
        public void TestIsPowerOfTwoWithShort16()
        {
            Assert.IsTrue(((short)16).IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithShort15()
        {
            Assert.IsFalse(((short)15).IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithShortZero()
        {
            Assert.IsTrue(((short)0).IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithInt16()
        {
            Assert.IsTrue(16.IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithInt15()
        {
            Assert.IsFalse(15.IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithIntZero()
        {
            Assert.IsTrue(0.IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithLong16()
        {
            Assert.IsTrue(16L.IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithLong15()
        {
            Assert.IsFalse(15L.IsPowerOfTwo());
        }

        [Test]
        public void TestIsPowerOfTwoWithLongZero()
        {
            Assert.IsTrue(0L.IsPowerOfTwo());
        }
    }
}
