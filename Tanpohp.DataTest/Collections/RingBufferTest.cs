#region usings

using System;
using NUnit.Framework;
using Tanpohp.Data.Collections;

#endregion

namespace Tanpohp.DataTest.Collections
{
    [TestFixture]
    public class RingBufferTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _buffer = new RingBuffer<string>(3) {Value1, Value2};
        }

        #endregion

        private IRingBuffer<string> _buffer;

        private const string Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4";

        [Test]
        public void TestAdd1()
        {
            _buffer.Add(Value3);
            Assert.AreEqual(Value3, _buffer[0]);
            Assert.AreEqual(3, _buffer.Count);
            Assert.AreEqual(3, _buffer.Capacity);
        }

        [Test]
        public void TestAdd2WithOverflow()
        {
            _buffer.Add(Value3);
            _buffer.Add(Value4);
            Assert.AreEqual(Value4, _buffer[0]);
            Assert.AreEqual(Value3, _buffer[1]);
            Assert.AreEqual(Value2, _buffer[2]);
        }

        [Test]
        public void TestAdd3WithMassiveOverflow()
        {
            _buffer.Add(Value4);
            for (var i = 0; i < 100; i++)
            {
                _buffer.Add(Value1);
                _buffer.Add(Value2);
                _buffer.Add(Value3);
            }

            Assert.AreEqual(Value3, _buffer[0]);
            Assert.AreEqual(Value2, _buffer[1]);
            Assert.AreEqual(Value1, _buffer[2]);
        }

        [Test]
        public void TestClear1()
        {
            _buffer.Clear();
            Assert.AreEqual(0, _buffer.Count);
        }

        [Test]
        public void TestClear2()
        {
            _buffer.Clear();
            Assert.AreEqual(null, _buffer[0]);
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(_buffer.Contains(Value1));
            Assert.IsTrue(_buffer.Contains(Value2));
            Assert.IsFalse(_buffer.Contains(Value3));
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void TestContrcutor()
        {
            new RingBuffer<int>(0);
        }

        [Test]
        public void TestIndexer()
        {
            _buffer.Add(Value3);
            Assert.AreEqual(Value3, _buffer[0]);
            Assert.AreEqual(Value2, _buffer[1]);
            Assert.AreEqual(Value1, _buffer[2]);
        }
    }
}