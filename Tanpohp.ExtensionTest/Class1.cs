#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class DoubleExtensionTest
    {
        [Test]
        public void TestInRange1()
        {
            const int lower = 1;
            const int upper = 2;
            const double value = 2.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(false, inRange);
        }

        [Test]
        public void TestInRange2()
        {
            const int lower = 1;
            const int upper = 2;
            const double value = 1.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(true, inRange);
        }

        [Test]
        public void TestInRange3()
        {
            const int lower = -1;
            const int upper = 2;
            const double value = 1.5;

            var inRange = value.InRange(lower, upper);

            Assert.AreEqual(true, inRange);
        }
    }
}
