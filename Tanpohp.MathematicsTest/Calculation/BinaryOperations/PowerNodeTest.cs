#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.BinaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.BinaryOperations
{
    [TestFixture]
    public class PowerNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _left = Mock.Interface<ICalculationNode>();
            _right = Mock.Interface<ICalculationNode>();

            _powerNode = new PowerNode(_left, _right);
        }

        #endregion

        private ICalculationNode _left, _right;
        private PowerNode _powerNode;

        [Test]
        public void MinusThreePowerMinusFour()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-4);

            Assert.AreEqual(0.01234567, _powerNode.Evaluate(), 0.00001);
        }

        [Test]
        public void MinusThreePowerThree()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(3);

            Assert.AreEqual(-27, _powerNode.Evaluate());
        }

        [Test]
        public void OnePowerMinusTwo()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-2);

            Assert.AreEqual(1, _powerNode.Evaluate());
        }

        [Test]
        public void OnePowerTwo()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(2);

            Assert.AreEqual(1, _powerNode.Evaluate());
        }
    }
}