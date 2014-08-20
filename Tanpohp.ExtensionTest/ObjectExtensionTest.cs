#region usings

using NUnit.Framework;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class ObjectExtensionTest
    {
        private Dummy _dummy;

        [SetUp]
        public void Setup()
        {
            _dummy = new Dummy();
        }

        [Test]
        public void TestGetPropertyNameWithProperty()
        {
            var actual = _dummy.GetPropertyName(() => _dummy.Property1);

            Assert.AreEqual("Property1", actual);
        }

        [Test]
        public void TestGetPropertyNameWithField()
        {
            var actual = _dummy.GetPropertyName(() => _dummy.Field1);

            Assert.AreEqual("Field1", actual);
        }

        private class Dummy
        {
            public int Property1 { get; set; }

            public int Field1;
        }
    }
}
