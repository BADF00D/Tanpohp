#region usings

using System;
using NUnit.Framework;
using Tanpohp.Data;

#endregion

namespace Tanpohp.DataTest
{
    [TestFixture]
    public class AbstractPoolTest
    {
        private IPool<object> _pool;

        [SetUp]
        public void SetUp()
        {
            _pool = new DummyPool(2);
        }

        [Test]
        public void PoolEmptyEvent1()
        {
            var wasPoolEmptyCalled = false;
            _pool.PoolEmpty += (sender, args) => wasPoolEmptyCalled = true;

            _pool.RetrieveInstance();
            _pool.RetrieveInstance();

            Assert.IsFalse(wasPoolEmptyCalled);
        }
        [Test]
        public void PoolEmptyEvent2()
        {
            var wasPoolEmptyCalled = false;
            _pool.PoolEmpty += (sender, args) => wasPoolEmptyCalled = true;

            _pool.RetrieveInstance();
            _pool.RetrieveInstance();
            _pool.RetrieveInstance();

            Assert.IsTrue(wasPoolEmptyCalled);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(2, _pool.Count);
            _pool.RetrieveInstance();
            Assert.AreEqual(1, _pool.Count);
            _pool.RetrieveInstance();
            Assert.AreEqual(0, _pool.Count);
            _pool.RetrieveInstance();
            Assert.AreEqual(0, _pool.Count);
        }

        [Test]
        public void TestClear1()
        {
            _pool.Clear(0);

            Assert.AreEqual(0, _pool.Count);
        }

        [Test]
        public void TestClear2()
        {
            _pool.Clear(1);

            Assert.AreEqual(1, _pool.Count);
        }

        [Test]
        public void TestClear3()
        {
            var initialCount = _pool.Count;

            _pool.Clear(10);

            Assert.AreEqual(initialCount, _pool.Count);
        }

        [Test]
        public void TestFill1()
        {
            _pool.Clear(0);
            _pool.FillPoolWith(3);
            Assert.AreEqual(3, _pool.Count);
        }

        [Test]
        public void TestFillDuringRetrieve()
        {
            _pool.Clear(0);
            _pool.PoolEmpty += (sender, args) => _pool.FillPoolWith(3);
            _pool.RetrieveInstance();

            const int expected = 2; //3 during fill minus one that was retrieved
            Assert.AreEqual(expected, _pool.Count);
        }

        [Test]
        public void TestNoFillDuringRetrieveOnClearedPool()
        {
            _pool.Clear(0);
            _pool.RetrieveInstance();

            Assert.AreEqual(0, _pool.Count);
        }

        [Test]
        public void TestRelease()
        {
            _pool.ReleaseInstance(new object());

            Assert.AreEqual(3, _pool.Count);
        }

        public class DummyPool : AbstractPool<Object>
        {
            public DummyPool(uint initialPoolSize)
                : base(initialPoolSize)
            {
            }

            protected override object CreateInstance()
            {
                return new object();
            }

            protected override void ClearInstance(object instance)
            {
            }
        }
    }
}
