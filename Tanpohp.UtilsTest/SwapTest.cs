#region usings

using NUnit.Framework;
using Tanpohp.Utils;

#endregion

namespace Tanpohp.UtilsTest
{
    [TestFixture]
    public class SwapTest
    {
        [Test]
        public void Test1()
        {
            var x = 1;
            var y = 2;

            Swap.Perform(ref x, ref y);

            Assert.AreEqual(2, x);
            Assert.AreEqual(1, y);
        }

        [Test]
        public void Test2()
        {
            var x = new Dummy(1);
            var y = new Dummy(2);

            Swap.Perform(ref x, ref y);

            Assert.AreEqual(2, x.Field);
            Assert.AreEqual(1, y.Field);
        }

        private class Dummy
        {
            public readonly int Field;

            public Dummy(int field)
            {
                Field = field;
            }
        }
    }
}
