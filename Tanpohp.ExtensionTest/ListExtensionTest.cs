#region usings

using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class ListExtensionTest
    {
        [Test]
        public void TestAddRange1()
        {
            IList<int> sequence = new List<int>();

            sequence.AddRange(new[] {1, 2, 3});

            Assert.AreEqual(3, sequence.Count);
            Assert.IsTrue(sequence.Contains(1));
            Assert.IsTrue(sequence.Contains(2));
            Assert.IsTrue(sequence.Contains(3));
        }
    }
}
