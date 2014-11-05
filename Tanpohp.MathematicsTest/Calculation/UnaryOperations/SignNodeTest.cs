#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.UnaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.UnaryOperations
{
    [TestFixture]
    public class SignNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _value = Mock.Interface<ICalculationNode>();
            _node = new SignNode(_value);
        }

        #endregion

        private ICalculationNode _value;
        private SignNode _node;

        [Test]
        public void TestNegative()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(-1);

            Assert.AreEqual(1.0, _node.Evaluate());
        }

        [Test]
        public void TestPositive()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(1);

            Assert.AreEqual(-1.0, _node.Evaluate());
        }

        [Test]
        public void TestZero()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(0);

            Assert.AreEqual(0.0, _node.Evaluate());
        }
    }
}