#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.BinaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.BinaryOperations
{
    [TestFixture]
    public class SubstractionNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _left = Mock.Interface<ICalculationNode>();
            _right = Mock.Interface<ICalculationNode>();

            _substractionNode = new SubstractionNode(_left, _right);
        }

        #endregion

        private ICalculationNode _left, _right;
        private SubstractionNode _substractionNode;

        [Test]
        public void NegativeMinusNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-4);

            Assert.AreEqual(1, _substractionNode.Evaluate());
        }

        [Test]
        public void NegativeMinusPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(3);

            Assert.AreEqual(-6, _substractionNode.Evaluate());
        }

        [Test]
        public void PositiveMinusNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-2);

            Assert.AreEqual(3, _substractionNode.Evaluate());
        }

        [Test]
        public void PositiveMinusPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(2);

            Assert.AreEqual(-1, _substractionNode.Evaluate());
        }
    }
}