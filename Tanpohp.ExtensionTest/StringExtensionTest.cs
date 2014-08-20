#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class StringExtensionTest
    {
        public void TestFormatWith()
        {
            var expected = string.Format("{0}", "Test");
            var actual = "{0}".FormatWith("Test");

            Assert.AreEqual(expected, actual);
        }
    }
}
