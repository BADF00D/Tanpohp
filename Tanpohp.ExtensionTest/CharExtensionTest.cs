#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class CharExtensionTest
    {
        [Test]
        public static void IsBracket()
        {
            Assert.IsTrue('('.IsBracket());
            Assert.IsTrue(')'.IsBracket());
            Assert.IsTrue('['.IsBracket());
            Assert.IsTrue(']'.IsBracket());
            Assert.IsTrue('{'.IsBracket());
            Assert.IsTrue('}'.IsBracket());
            for (var i = 0; i < 10; i++)
                Assert.IsFalse(i.ToString().ToCharArray()[0].IsBracket());
        }

        [Test]
        public static void IsCircleBracket()
        {
            Assert.IsTrue('('.IsCircleBracket());
            Assert.IsTrue(')'.IsCircleBracket());
            Assert.IsFalse('['.IsCircleBracket());
            Assert.IsFalse(']'.IsCircleBracket());
            Assert.IsFalse('{'.IsCircleBracket());
            Assert.IsFalse('}'.IsCircleBracket());
        }

        [Test]
        public static void IsCurlyBracket()
        {
            Assert.IsFalse('('.IsCurlyBracket());
            Assert.IsFalse(')'.IsCurlyBracket());
            Assert.IsFalse('['.IsCurlyBracket());
            Assert.IsFalse(']'.IsCurlyBracket());
            Assert.IsTrue('{'.IsCurlyBracket());
            Assert.IsTrue('}'.IsCurlyBracket());
        }

        [Test]
        public static void IsSquareBracket()
        {
            Assert.IsFalse('('.IsSquareBracket());
            Assert.IsFalse(')'.IsSquareBracket());
            Assert.IsTrue('['.IsSquareBracket());
            Assert.IsTrue(']'.IsSquareBracket());
            Assert.IsFalse('{'.IsSquareBracket());
            Assert.IsFalse('}'.IsSquareBracket());
        }
    }
}