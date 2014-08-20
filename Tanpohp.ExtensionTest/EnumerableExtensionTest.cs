#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class EnumerableExtensionTest
    {
        [Test]
        public void Test1()
        {
            var sequence = new [] {1, 2, 3};

            var sum = 0;
            sequence.ForEach(i => sum += i);
            Assert.AreEqual(6, sum);
        }
    }
}
