using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanpohp.MathematicsTest.Spline
{
    using Mathematics;
    using Mathematics.Spline;
    using NUnit.Framework;

    [TestFixture] 
    public class CubicCattMullRomSplineInterpolatorTest
    {
        private ICubicSplineInterpolator _interpolator;

        private const double DefaultDelta = 0.001;

        [SetUp]
        public void Setup()
        {
            _interpolator = new CubicCattMullRomSplineInterpolator();
        }


        [Test]
        public void InterpolateStraightLine()
        {
            var point0 = new Vector3(0, 0, 0);
            var point1 = new Vector3(1, 0, 0);
            var point2 = new Vector3(2, 0, 0);
            var point3 = new Vector3(3, 0, 0);

            var interpoaltedPoint = _interpolator.Interpolate(.5, .5, point0, point1, point2, point3);

            AssertAreAqual(new Vector3(1.5,0,0), interpoaltedPoint);
        }

        [Test]
        public void InterpolateQuadricCurve()
        {
            var point0 = new Vector3(-2, 4, 0);
            var point1 = new Vector3(-1, 1, 0);
            var point2 = new Vector3(1, 1, 0);
            var point3 = new Vector3(2, 4, 0);

            var interpoaltedPoint = _interpolator.Interpolate(.5, .5, point0, point1, point2, point3);

            AssertAreAqual(new Vector3(0,.5,0), interpoaltedPoint, new Vector3(DefaultDelta, .25, DefaultDelta));
        }

        [Test]
        public void InterpolateSinusCurve()
        {
            var point0 = new Vector3(0, 0, 0);
            var point1 = new Vector3(Math.PI/2, 1, 0);
            var point2 = new Vector3(Math.PI, 0, 0);
            var point3 = new Vector3(Math.PI*1.5, -1, 0);

            var interpoaltedPoint = _interpolator.Interpolate(.5, .5, point0, point1, point2, point3);

            AssertAreAqual(new Vector3(Math.PI * 3.0 / 4, Math.Sin(Math.PI * 3.0 / 4), 0), interpoaltedPoint, new Vector3(DefaultDelta, .125, DefaultDelta));
        }

        private static void AssertAreAqual(Vector3 expected, Vector3 actual, double delta = DefaultDelta)
        {
            Assert.AreEqual(expected.X, actual.X, delta, "X");
            Assert.AreEqual(expected.Y, actual.Y, delta, "Y");
            Assert.AreEqual(expected.Z, actual.Z, delta, "Z");
        }

        private static void AssertAreAqual(Vector3 expected, Vector3 actual, Vector3 delta)
        {
            Assert.AreEqual(expected.X, actual.X, delta.X, "X");
            Assert.AreEqual(expected.Y, actual.Y, delta.Y, "Y");
            Assert.AreEqual(expected.Z, actual.Z, delta.Z, "Z");
        }
    }
}
