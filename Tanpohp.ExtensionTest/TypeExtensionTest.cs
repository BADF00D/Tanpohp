using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tanpohp.Extensions;

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class TypeExtensionTest
    {
        [Test]
        public static void TestGetReadableNameWithNormalType()
        {
            var actual = typeof (object).GetReadableName();

            Assert.AreEqual("Object", actual);
        }

        [Test]
        public static void TestGetReadableNameWithGenericType()
        {
            var actual = typeof(Dictionary<string, int>).GetReadableName();

            Assert.AreEqual("Dictionary<String,Int32>", actual);
        }

        [Test]
        public static void TestGetPublicPropertiesWithGenericClass()
        {
            var properties = typeof (List<int>).GetPublicProperties();

            Assert.AreEqual(3, properties.Count());
        }

        [Test]
        public static void TestGetPublicPropertiesWithGenericInterface()
        {
            var properties = typeof(IList<int>).GetPublicProperties();

            Assert.AreEqual(3, properties.Count());
        }

        [Test]
        public static void TestGetPublicPropertiesWithClass()
        {
            var properties = typeof(String).GetPublicProperties();
            
            Assert.AreEqual(2, properties.Count());
        }
    }
}
