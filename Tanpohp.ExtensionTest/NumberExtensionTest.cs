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

        [Test]
        public void ClampByte()
        {
            Assert.AreEqual(3,((byte)3).Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerByte()
        {
            Assert.AreEqual(1, ((byte)0).Clamp(1, 2));
            Assert.AreEqual(20, ((byte)10).Clamp(20, 30));
        }

        [Test]
        public void ClampToUpperByte()
        {
            Assert.AreEqual(2, ((byte)3).Clamp(1, 2));
            Assert.AreEqual(30, ((byte)40).Clamp(20, 30));
        }

        [Test]
        public void ClampShort()
        {
            Assert.AreEqual(3, ((short)3).Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerShort()
        {
            Assert.AreEqual(1, ((short)0).Clamp(1, 2));
            Assert.AreEqual(20, ((short)10).Clamp(20, 30));
            Assert.AreEqual(-20, ((short)-30).Clamp(-20, -10));
        }

        [Test]
        public void ClampToUpperShort()
        {
            Assert.AreEqual(2, ((short)3).Clamp(1, 2));
            Assert.AreEqual(30, ((short)40).Clamp(20, 30));
            Assert.AreEqual(-20, ((short)-10).Clamp(-30, -20));
        }

        [Test]
        public void ClampInt()
        {
            Assert.AreEqual(3, 3.Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerInt()
        {
            Assert.AreEqual(1, 0.Clamp(1, 2));
            Assert.AreEqual(20, 10.Clamp(20, 30));
            Assert.AreEqual(-20, (-30).Clamp(-20, -10));
        }

        [Test]
        public void ClampToUpperInt()
        {
            Assert.AreEqual(2, (3).Clamp(1, 2));
            Assert.AreEqual(30, 40.Clamp(20, 30));
            Assert.AreEqual(-20, (-10).Clamp(-30, -20));
        }

        [Test]
        public void ClampLong()
        {
            Assert.AreEqual(3, 3L.Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerLong()
        {
            Assert.AreEqual(1, 0L.Clamp(1, 2));
            Assert.AreEqual(20, 10L.Clamp(20, 30));
            Assert.AreEqual(-20, (-30L).Clamp(-20, -10));
        }

        [Test]
        public void ClampToUpperLong()
        {
            Assert.AreEqual(2, (3L).Clamp(1, 2));
            Assert.AreEqual(30, 40L.Clamp(20, 30));
            Assert.AreEqual(-20, (-10L).Clamp(-30, -20));
        }

        [Test]
        public void ClampFloat()
        {
            Assert.AreEqual(3, 3f.Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerFloat()
        {
            Assert.AreEqual(1, 0f.Clamp(1, 2));
            Assert.AreEqual(20, 10f.Clamp(20, 30));
            Assert.AreEqual(-20, (-30f).Clamp(-20, -10));
        }

        [Test]
        public void ClampToUpperFloat()
        {
            Assert.AreEqual(2, (3f).Clamp(1, 2));
            Assert.AreEqual(30, 40f.Clamp(20, 30));
            Assert.AreEqual(-20, (-10f).Clamp(-30, -20));
        }

        [Test]
        public void ClampDouble()
        {
            Assert.AreEqual(3, 3d.Clamp(2, 4));
        }

        [Test]
        public void ClampToLowerDouble()
        {
            Assert.AreEqual(1, 0d.Clamp(1, 2));
            Assert.AreEqual(20, 10d.Clamp(20, 30));
            Assert.AreEqual(-20, (-30d).Clamp(-20, -10));
        }

        [Test]
        public void ClampToUpperDouble()
        {
            Assert.AreEqual(2, (3d).Clamp(1, 2));
            Assert.AreEqual(30, 40d.Clamp(20, 30));
            Assert.AreEqual(-20, (-10d).Clamp(-30, -20));
        }
    }
}
