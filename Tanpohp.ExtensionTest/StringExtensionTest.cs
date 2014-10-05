#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class StringExtensionTest
    {
        [Test]
        public void TestFormatWith()
        {
            var expected = string.Format("{0}", "Test");
            var actual = "{0}".FormatWith("Test");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TryFindNthIndexOfTest1()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            int index;
            var hasResult = source.TryFindNthIndexOf("0", 2, out index);
            Assert.IsTrue(hasResult);
            Assert.AreEqual(8, index);
        }

        [Test]
        public void TryFindNthIndexOfTest2()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            int index;
            var hasResult = source.TryFindNthIndexOf("0", 6, out index);
            Assert.IsFalse(hasResult);
        }

        [Test]
        public void TryFindNthIndexOfTest3()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            int index;
            var hasResult = source.TryFindNthIndexOf("1", 2, out index);
            Assert.IsFalse(hasResult);
        }

        [Test]
        public void RemoveBrackets()
        {
            const string source = "(a+b)*c != [c*d]-a";
            Assert.AreEqual("a+b*c != c*d-a", source.RemoveBraces());
        }

        [Test]
        public void RemoveChar()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            var result = source.RemoveChar(c => c=='0');
            Assert.AreEqual("xxxxxxxxxxxxxxx", result);
        }

        [Test]
        public void RemoveDigit()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            var result = source.RemoveDigit();
            Assert.AreEqual("xxxxxxxxxxxxxxx", result);
        }

        [Test]
        public void RemoveLetter()
        {
            const string source = "0xxx0xxx0xxx0xxx0xxx0";
            var result = source.RemoveLetter();
            Assert.AreEqual("000000", result);
        }
    }
}
