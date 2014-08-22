#region usings

using NUnit.Framework;
using Tanpohp.Wpf.Converter;

#endregion

namespace Tanpohp.WpfTest.Converter
{
    [TestFixture]
    public class BooleanToConverterTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _converter = new IntConverter {ErrorValue = 3, TrueValue = 2, FalseValue = 1};
        }

        #endregion

        private BooleanToConverter<int> _converter;

        private class IntConverter : BooleanToConverter<int>
        {
        }

        [Test]
        public void TestConvert1()
        {
            Assert.AreEqual(1, _converter.Convert(false, null, null, null));
            Assert.AreEqual(2, _converter.Convert(true, null, null, null));
            Assert.AreEqual(3, _converter.Convert(new object(), null, null, null));
        }
    }
}