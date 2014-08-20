#region usings

using System;
using NUnit.Framework;
using Tanpohp.Data.ValueProvider;

#endregion

namespace Tanpohp.DataTest.ValueProvider
{
    [TestFixture]
    public class ExceptionThrowingValueProviderTest
    {
        private IValueProvider<int> _valueProvider;
        
        [SetUp]
        public void Setup()
        {
            _valueProvider = new ValueBiggerThenZeroValueProvider {Value = 100};
        }

        [Test]
        public void TestSetvalueToTwo()
        {
            _valueProvider.Value = 2;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FailTestSetValueToZero()
        {
            _valueProvider.Value = 0;
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

        private class ValueBiggerThenZeroValueProvider : ExceptionThrowingValueProvider<int>
        {
            public ValueBiggerThenZeroValueProvider() 
                : base((val1, val2)=> val1 == val2)
            {
            }

            protected override bool IsValid(int newValue)
            {
                return newValue > 0;
            }
        }
    }
}
