#region usings

using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Data.Collections;

#endregion

namespace Tanpohp.DataTest.Collections
{
    [TestFixture]
    public class NotifyChangedCollectionTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _collection = new NotifyChangedCollection<int>(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        #endregion

        private INotifyChangedCollection<int> _collection;

        [Test]
        public void MultipleAdd()
        {
            var addedItems = new List<int>();
            _collection.ItemAdded += (sender, args) => addedItems.Add(args.Item);

            _collection.Add(10);
            _collection.Add(11);
            _collection.Add(12);

            Assert.AreEqual(10, addedItems[0]);
            Assert.AreEqual(11, addedItems[1]);
            Assert.AreEqual(12, addedItems[2]);
        }

        [Test]
        public void MultipleAddAndRemove()
        {
            var addedItem = 0;
            var removedItem = 0;
            _collection.ItemAdded += (sender, args) => { addedItem = args.Item; };
            _collection.ItemRemoved += (sender, args) => { removedItem = args.Item; };

            _collection.Add(10);
            _collection.Remove(10);

            Assert.AreEqual(10, addedItem);
            Assert.AreEqual(10, removedItem);
        }

        [Test]
        public void SingleAdd()
        {
            var addedItem = 0;
            _collection.ItemAdded += (sender, args) => { addedItem = args.Item; };

            _collection.Add(10);
            Assert.AreEqual(10, addedItem);
        }

        [Test]
        public void Count()
        {
            Assert.AreEqual(10, _collection.Count);
            _collection.Add(10);
            Assert.AreEqual(11, _collection.Count);
            _collection.Add(10);
            Assert.AreEqual(12, _collection.Count);
            _collection.Add(10);
            Assert.AreEqual(13, _collection.Count);
            _collection.Add(10);
            Assert.AreEqual(14, _collection.Count);

            _collection.Remove(10);
            Assert.AreEqual(13, _collection.Count);

            _collection.Clear();
            Assert.AreEqual(0, _collection.Count);
        }

        [Test]
        public void Contains()
        {
            Assert.IsTrue(_collection.Contains(0));
            Assert.IsTrue(_collection.Contains(1));
            Assert.IsTrue(_collection.Contains(2));
            Assert.IsTrue(_collection.Contains(3));
            Assert.IsTrue(_collection.Contains(4));
            Assert.IsTrue(_collection.Contains(5));
        }

        [Test]
        public void CopyTo()
        {
            var array = new int[15];
            _collection.CopyTo(array, 5);
            Assert.AreEqual(0, array[5]);
            Assert.AreEqual(1, array[6]);
            Assert.AreEqual(2, array[7]);
            Assert.AreEqual(3, array[8]);
            Assert.AreEqual(4, array[9]);
        }
    }
}