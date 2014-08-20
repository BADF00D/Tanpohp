#region usings

using NUnit.Framework;
using Tanpohp.Data.ValueProvider;

#endregion

namespace Tanpohp.DataTest.ValueProvider
{
    [TestFixture]
    public class NoValidationAbstractValueProviderTest
    {
        private IValueProvider<int> _valueProvider;

        [SetUp]
        public void Setup()
        {
            _valueProvider = new NoValidationAbstractValueProvider<int>((val1, val2)=>val1 == val2) {Value = 3};
        }

        [Test]
        public void AssertChangingWasInvoked()
        {
            var wasCalled = false;
            _valueProvider.Changing += (sender, args) => wasCalled = true;
            _valueProvider.Value = 4;
            
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void AssertChangedWasInvoked()
        {
            var wasCalled = false;
            _valueProvider.Changed += (sender, args) => wasCalled = true;
            _valueProvider.Value = 4;

            Assert.IsTrue(wasCalled);
        }
    }
}
