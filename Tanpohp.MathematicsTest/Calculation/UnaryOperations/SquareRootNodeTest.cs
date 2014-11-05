using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Mathematics.Calculation;
using Tanpohp.Mathematics.Calculation.UnaryOperations;

namespace Tanpohp.MathematicsTest.Calculation.UnaryOperations
{
    [TestFixture]
    public class SquareRootNodeTest
    {
        private ICalculationNode _value;
        private SquareRootNode _squareRootNode;

        [SetUp]
        public void Setup()
        {
            _value = Mock.Interface<ICalculationNode>();
            _squareRootNode = new SquareRootNode(_value);
        }

        [Test]
        public void SquareRootPositive()
        {
            Expect.MethodCall(()=>_value.Evaluate()).Returns(9);

            Assert.AreEqual(3, _squareRootNode.Evaluate());
        }

        [Test]
        public void SquareRootZero()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(0);

            Assert.AreEqual(0, _squareRootNode.Evaluate());
        }

        [Test]
        [ExpectedException]
        public void SquareRootNegative()
        {
            Expect.MethodCall(() => _value.Evaluate()).Returns(-9);

            Assert.AreEqual(0, _squareRootNode.Evaluate());
        }
    }
}
