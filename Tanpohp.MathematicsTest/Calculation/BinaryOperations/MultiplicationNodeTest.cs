#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.BinaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.BinaryOperations
{
    [TestFixture]
    public class MultiplicationNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _left = Mock.Interface<ICalculationNode>();
            _right = Mock.Interface<ICalculationNode>();

            _multiplicationNode = new MultiplicationNode(_left, _right);
        }

        #endregion

        private ICalculationNode _left, _right;
        private MultiplicationNode _multiplicationNode;

        [Test]
        public void NegativeTimesNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-4);

            Assert.AreEqual(12, _multiplicationNode.Evaluate());
        }

        [Test]
        public void NegativeTimesPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(3);

            Assert.AreEqual(-9, _multiplicationNode.Evaluate());
        }

        [Test]
        public void PositiveTimesNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-2);

            Assert.AreEqual(-2, _multiplicationNode.Evaluate());
        }

        [Test]
        public void PositiveTimesPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(2);

            Assert.AreEqual(2, _multiplicationNode.Evaluate());
        }
    }
}