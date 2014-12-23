namespace Tanpohp.MathematicsTest
{
    using System;
    using Extensions;
    using Mathematics;
    using NUnit.Framework;

    [TestFixture]
    public class Vector3Test
    {
        [Test]
        public void DistanceTo1()
        {
            var unitX = Vector3.One;
            var unitY = new Vector3(2,2,2);

            var distance = unitX.DistanceTo(ref unitY);

            Assert.AreEqual(Math.Sqrt(3), distance);
        }

        [Test]
        public void DistanceTo2()
        {
            var one = Vector3.One;
            var zero = Vector3.Zero;

            var distance = zero.DistanceTo(ref one);

            Assert.AreEqual(Math.Sqrt(3), distance, 0.0001);
        }

        [Test]
        public void Angle()
        {
            var unitX = Vector3.UnitX;
            var unitY = Vector3.UnitY;

            var angleInRadian = Vector3.Angle(unitY, unitX);
            var angleInDegree = angleInRadian.ToDegree();

            Assert.AreEqual(90, angleInDegree);
        }

        [Test]
        public void Cross1()
        {
            var one = new Vector3(1,2,3);
            var two = new Vector3(4, 5, 6);

            var cross = Vector3.Cross(one, two);
           
            Assert.AreEqual(-3, cross.X);
            Assert.AreEqual(6, cross.Y);
            Assert.AreEqual(-3, cross.Z);
        }

        [Test]
        public void Cross2()
        {
            var unitX = Vector3.UnitX;
            var unitY = Vector3.UnitY;

            var cross = Vector3.Cross(unitX, unitY);

            Assert.AreEqual(0, cross.X);
            Assert.AreEqual(0, cross.Y);
            Assert.AreEqual(1, cross.Z);
        }

        [Test]
        public void Normalize()
        {
            var unitX = Vector3.UnitX;

            unitX.Normalize();

            Assert.AreEqual(1, unitX.X);
            Assert.AreEqual(0, unitX.Y);
            Assert.AreEqual(0, unitX.Z);
        }

        [Test]
        public void Normalize2()
        {
            var one = Vector3.One;

            one.Normalize();

            Assert.AreEqual(1/Math.Sqrt(3), one.X, 0.0001);
            Assert.AreEqual(1 / Math.Sqrt(3), one.Y, 0.0001);
            Assert.AreEqual(1 / Math.Sqrt(3), one.Z, 0.0001);
        }

        [Test]
        public void Clone()
        {
            var one = Vector3.One;

            var clone = one.Clone();

            Assert.AreEqual(1, clone.X, 0.0001);
            Assert.AreEqual(1, clone.Y, 0.0001);
            Assert.AreEqual(1, clone.Z, 0.0001);
        }

        [Test]
        public void Length()
        {
            var one = Vector3.One;

            var length = one.Length();

            Assert.AreEqual(Math.Sqrt(3), length, 0.0001);
        }

        [Test]
        public void LengthSquared()
        {
            var one = Vector3.One;

            var length = one.LengthSquared();

            Assert.AreEqual(3, length, 0.0001);
        }

        [Test]
        public void OperatorMultiplyWithVector3()
        {
            var one = new Vector3(1, 2, 3);
            var two = new Vector3(4, 5, 6);

            var result = one*two;


            Assert.AreEqual(32, result, 0.0001);
        }

        [Test]
        public void OperatorMultiplyVectorWithScalar()
        {
            var one = new Vector3(1, 2, 3);

            var result = 2* one;


            Assert.AreEqual(2, result.X, 0.0001);
            Assert.AreEqual(4, result.Y, 0.0001);
            Assert.AreEqual(6, result.Z, 0.0001);
        }
        [Test]
        public void OperatorMultiplyScalarWithVector()
        {
            var one = new Vector3(1, 2, 3);

            var result = one * 3;


            Assert.AreEqual(3, result.X, 0.0001);
            Assert.AreEqual(6, result.Y, 0.0001);
            Assert.AreEqual(9, result.Z, 0.0001);
        }

        [Test]
        public void OperatorAdd()
        {
            var one = new Vector3(1, 2, 3);
            var two = new Vector3(4, 5, 6);

            var result = one + two;


            Assert.AreEqual(5, result.X, 0.0001);
            Assert.AreEqual(7, result.Y, 0.0001);
            Assert.AreEqual(9, result.Z, 0.0001);
        }

        [Test]
        public void OperatorMinus()
        {
            var one = new Vector3(1, 2, 3);
            var two = new Vector3(4, 5, 6);

            var result = one - two;


            Assert.AreEqual(-3, result.X, 0.0001);
            Assert.AreEqual(-3, result.Y, 0.0001);
            Assert.AreEqual(-3, result.Z, 0.0001);
        }

        [Test]
        public void Scalar()
        {
            var one = new Vector3(1, 2, 3);
            var two = new Vector3(4, 5, 6);

            var result = one * two;


            Assert.AreEqual(32, result, 0.0001);
        }
    }
}