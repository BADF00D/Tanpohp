using System.Linq;
using NUnit.Framework;
using Tanpohp.Data.Collections;

namespace Tanpohp.DataTest.Collections
{
    [TestFixture]
    public class CacheTest
    {
        private ICache<int, string> _cache;

        private const string Value1 = "Value1", Value2 = "Value2", Value3 = "Value3";

        [SetUp]
        public void Setup()
        {
            _cache = new Cache<int, string>(16) {{1, Value1}, {2, Value2}};
        }

        [Test]
        public void TestAddValue3WithKey3()
        {
            var key = 0;
            var value = string.Empty;
            var invoker = new object();
            _cache.ItemAdded += (sender, args) =>
                                    {
                                        key = args.Identifier;
                                        value = args.Item;
                                        invoker = sender;
                                    };
            _cache.Add(3, Value3);
            Assert.AreEqual(3, _cache.Count);
            Assert.AreEqual(3, key);
            Assert.AreEqual(Value3, value);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        [ExpectedException]
        public void TestAdd2()
        {
            _cache.Add(1, Value1);
        }

        [Test]
        public void TestRemove1()
        {
            _cache.Remove(1);
            Assert.AreEqual(1, _cache.Count);
            Assert.IsTrue(_cache.Contains(2));
        }

        [Test]
        public void TestRemove2()
        {
            Assert.IsFalse(_cache.Remove(3));
        }

        [Test]
        public void TestRemove3()
        {
            var key = 0;
            var value = string.Empty;
            var invoker = new object(); 
            _cache.ItemRemoved += (sender, args) =>
                                      {
                                          key = args.Identifier;
                                          value = args.Item;
                                          invoker = sender;
                                      };
            _cache.Remove(1);
            Assert.AreEqual(1, key);
            Assert.AreEqual(Value1, value);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        public void TestUpdate1()
        {
            _cache.Update(1, Value2);
            Assert.AreEqual(Value2, _cache[1]);
        }

        [Test]
        [ExpectedException]
        public void TestUpdate2()
        {
            _cache.Update(10, Value2);
        }

        [Test]
        public void TestUpdate3()
        {
            var key = 0;
            string oldValue = string.Empty, newValue = string.Empty;
            var invoker = new object();
            _cache.ItemUpdated += (sender, args) =>
            {
                key = args.Identifier;
                oldValue = args.OldItem;
                newValue = args.NewItem;
                invoker = sender;
            };
            _cache.Update(1, Value2);
            Assert.AreEqual(1, key);
            Assert.AreEqual(Value1, oldValue);
            Assert.AreEqual(Value2, newValue);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        public void TestBatchUpdateCompleted()
        {
            var wasInvoke = false;
            var invoker = new object();
            _cache.BatchUpdateCompleted += (sender, args) =>
                                               {
                                                   wasInvoke = true;
                                                   invoker = sender;
                                               };
            _cache.InvokeUpdateBatchCompleted();
            Assert.AreEqual(true, wasInvoke);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        public void TestEnumerable()
        {
            var cacheAsList = _cache.ToList();

            Assert.IsTrue(cacheAsList.Contains(Value1));
            Assert.IsTrue(cacheAsList.Contains(Value2));
        }


        [Test]
        public void TestClear1()
        {
            var wasInvoked = false;
            _cache.ItemRemoved += (sender, args) =>
                                      {
                                          wasInvoked = true;
                                      };
            
            _cache.Clear(false);

            Assert.AreEqual(0, _cache.Count);
            Assert.IsFalse(wasInvoked);
        }

        [Test]
        public void TestClear2()
        {
            var invoker = new object();
            var key = 0;
            var value = string.Empty;
            var wasInvoked = false;
            _cache.ItemRemoved += (sender, args) =>
            {
                key = args.Identifier;
                value = args.Item;
                invoker = sender;
                wasInvoked = true;
            };

            _cache.Clear();

            Assert.IsTrue(wasInvoked);
            Assert.AreEqual(0, _cache.Count);
            Assert.AreNotEqual(0, key);
            Assert.AreNotEqual(string.Empty, value);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        public void TestCleared()
        {
            var invoker = new object();
            var wasInvoked = false;
            _cache.Cleared += (sender, args) =>
            {
                invoker = sender;
                wasInvoked = true;
            };

            _cache.Clear();

            Assert.IsTrue(wasInvoked);
            Assert.AreEqual(_cache, invoker);
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(_cache.Contains(1));
            Assert.IsTrue(_cache.Contains(2));
            Assert.IsFalse(_cache.Contains(3));
        }

        [Test]
        public void TestTryGet1()
        {
            string value;
            Assert.IsTrue(_cache.TryGet(1, out value));
            Assert.AreEqual(Value1, value);
        }

        [Test]
        public void TestTryGet2()
        {
            string value;
            Assert.IsFalse(_cache.TryGet(10, out value));
            Assert.AreEqual(null, value);
        }

        [Test]
        public void TestTryGetKey1()
        {
            int key;
            Assert.IsTrue(_cache.TryGetKey(Value1, out key));
            Assert.AreEqual(1, key);
        }

        [Test]
        public void TestTryGetKey2()
        {
            var key = 29;
            Assert.IsFalse(_cache.TryGetKey(Value3, out key));
            Assert.AreEqual(0, key);
        }
    }
}
