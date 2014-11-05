#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.BinaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.BinaryOperations
{
    [TestFixture]
    public class AdditionNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _left = Mock.Interface<ICalculationNode>();
            _right = Mock.Interface<ICalculationNode>();

            _additionNode = new AdditionNode(_left, _right);
        }

        #endregion

        private ICalculationNode _left, _right;
        private AdditionNode _additionNode;

        [Test]
        public void AddNegativeAndNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-4);

            Assert.AreEqual(-7, _additionNode.Evaluate());
        }

        [Test]
        public void AddNegativeAndPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(-3);
            Expect.MethodCall(() => _right.Evaluate()).Returns(3);

            Assert.AreEqual(0, _additionNode.Evaluate());
        }

        [Test]
        public void AddPositiveAndNegative()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(-2);

            Assert.AreEqual(-1, _additionNode.Evaluate());
        }

        [Test]
        public void AddPositiveAndPositive()
        {
            Expect.MethodCall(() => _left.Evaluate()).Returns(1);
            Expect.MethodCall(() => _right.Evaluate()).Returns(2);

            Assert.AreEqual(3, _additionNode.Evaluate());
        }
    }
}