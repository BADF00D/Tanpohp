#region usings

using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.UnaryOperations;

#endregion

namespace Tanpohp.MathematicsTest.Calculation.UnaryOperations
{
    [TestFixture]
    public class AbsoluteNodeTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _value = Mock.Interface<ICalculationNode>();
            _absoluteNode = new AbsoluteNode(_value);
        }

        #endregion

        private ICalculationNode _value;

        private AbsoluteNode _absoluteNode;

        [Test]
        public void TestNegative()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(-2);

            Assert.AreEqual(2, _absoluteNode.Evaluate());
        }

        [Test]
        public void TestPositive()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(1);

            Assert.AreEqual(1, _absoluteNode.Evaluate());
        }

        [Test]
        public void TestZero()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(0);

            Assert.AreEqual(0, _absoluteNode.Evaluate());
        }
    }
}