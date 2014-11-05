#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.BinaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.BinaryOperations
{
    [TestFixture]
    public class DiversionNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _left = Mock.Interface<ICalculationNode>();
            _right = Mock.Interface<ICalculationNode>();

            _diversionNode = new DiversionNode(_left, _right);
        }

        #endregion

        private ICalculationNode _left, _right;
        private DiversionNode _diversionNode;

        [Test]
        public void NegativeThroughNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-4);

            Assert.AreEqual(0.75, _diversionNode.Evaluate());
        }

        [Test]
        public void NegativeThroughPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(3);

            Assert.AreEqual(-1, _diversionNode.Evaluate());
        }

        [Test]
        public void PositiveThroughNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-2);

            Assert.AreEqual(-0.5, _diversionNode.Evaluate());
        }

        [Test]
        public void PositiveThroughPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(2);

            Assert.AreEqual(0.5, _diversionNode.Evaluate());
        }
    }
}