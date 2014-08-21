#region usings

using System;
using System.Linq;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class EnumerableExtensionTest
    {
        [Test]
        public void TestForEach()
        {
            var sequence = new [] {1, 2, 3};

            var sum = 0;
            sequence.ForEach(i => sum += i);
            Assert.AreEqual(6, sum);
        }

        [Test]
        public void TestContains()
        {
            var sequence = new [] {1, 2, 3};
            
            Assert.IsTrue(sequence.Contains(i => i==1));
        }

        [Test]
        public void TestItemsToString1()
        {
            var sequence = new[] { 1, 2, 3 };

            var result = sequence.ItemsToString(i => "" + i, "?");

            Assert.AreEqual("1?2?3", result);
        }

        [Test]
        public void TestItemsToString2()
        {
            var sequence = new int[0];

            var result = sequence.ItemsToString(i => "" + i, "?");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TestItemsToString3()
        {
            var sequence = new[] { 1, 2, 3 };

            var result = sequence.ItemsToString("x");

            Assert.AreEqual("1x2x3", result);
        }

        [Test]
        public void TestItemsToString4()
        {
            var sequence = new int [0];

            var result = sequence.ItemsToString("x");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TestToHashSet1()
        {
            var sequence = new[] { 1, 2, 3 };

            var set = sequence.ToHashSet();

            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(3));
            Assert.AreEqual(3, set.Count);
        }

        [Test]
        public void TestToHashSet2()
        {
            var sequence = new[] { 1, 2, 3,1 };

            var set = sequence.ToHashSet();

            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(3));
            Assert.AreEqual(3, set.Count);
        }

        [Test]
        public void TestToDictionaryGroupedBy()
        {
            var sequence = new[]
                               {
                                   new Dummy("Paul", 23), new Dummy("Pauline", 22), new Dummy("Eric", 20),
                                   new Dummy("Berta", 23)
                               };

            var personGroupedByAge = sequence.ToDictionaryGroupedBy(person => person.Age);

            Assert.AreEqual(3, personGroupedByAge.Count);
            Assert.AreEqual(2, personGroupedByAge[23].Count);
            Assert.AreEqual(1, personGroupedByAge[22].Count);
            Assert.AreEqual(1, personGroupedByAge[20].Count);
        }

        [Test]
        public void TestContainsOnlyUniqueItems1()
        {
            var sequence = new[]
                               {
                                   new Dummy("Paul", 23), new Dummy("Pauline", 22), new Dummy("Eric", 20),
                                   new Dummy("Berta", 23)
                               };

            Assert.IsTrue(sequence.ContainsOnlyUniqueItems());
        }

        [Test]
        public void TestContainsOnlyUniqueItems2()
        {
            var sequence = new[]
                               {
                                   new Dummy("Paul", 23), new Dummy("Pauline", 22), new Dummy("Eric", 20),
                                   new Dummy("Paul", 23)
                               };

            Assert.IsFalse(sequence.ContainsOnlyUniqueItems());
        }

        [Test]
        public void TestTakeFirst1()
        {
            var sequence = new[] {1, 2, 3, 4, 5, 6};

            var items = sequence.TakeFirst(2).ToList();
            Assert.AreEqual(2, items.Count());
            Assert.AreEqual(1, items[0]);
            Assert.AreEqual(2, items[1]);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FailTestTakeFirst2()
        {
            var sequence = new[] { 1 };

            sequence.TakeFirst(2).ToList();
        }

        [Test]
        public void TestTakeLast1()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            var items = sequence.TakeLast(2, true).ToList();
            Assert.AreEqual(2, items.Count());
            Assert.AreEqual(5, items[0]);
            Assert.AreEqual(6, items[1]);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FailTestTakeLast()
        {
            var sequence = new[] { 1 };

            sequence.TakeLast(2).ToList();
        }

        [Test]
        public void TestIsEmpty1()
        {
            var sequence = new[] { 1 };

            Assert.IsFalse(sequence.IsEmpty());
        }

        [Test]
        public void TestIsEmpty2()
        {
            var sequence = new int[0];

            Assert.IsTrue(sequence.IsEmpty());
        }

        [Test]
        public void TestItemAt1()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            var items = sequence.ItemAt(3);
            Assert.AreEqual(4, items);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FailItemAt1()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            var items = sequence.ItemAt(-3);
            Assert.AreEqual(4, items);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FailItemAt2()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            var items = sequence.ItemAt(10);
            Assert.AreEqual(4, items);
        }

        [Test]
        public void TestNon1()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(sequence.Non(i=>i == 10));
        }
        [Test]
        public void TestNon2()
        {
            var sequence = new[] { 1, 2, 3, 4, 5, 6 };

            Assert.IsFalse(sequence.Non(i => i == 3));
        }

        private class Dummy
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public Dummy(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (Dummy)) return false;
                return Equals((Dummy) obj);
            }

            public bool Equals(Dummy other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.Name, Name) && other.Age == Age;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Age; ;
                }
            }
        }
    }
}
