namespace Tanpohp.MathematicsTest.Spline
{
    using System.Collections.Generic;
    using Extension;
    using Mathematics;
    using Mathematics.Spline;
    using NUnit.Framework;
    using Simple.Mocking;

    [TestFixture]
    public class SplinePathGeneratorTest
    {
        [SetUp]
        public void Setup()
        {
            _cubicSplineInterpolator = Mock.Interface<ICubicSplineInterpolator>();
            _splinePathGenerator = new SplinePathGenerator(_cubicSplineInterpolator);
        }

        private ICubicSplineInterpolator _cubicSplineInterpolator;
        private ISplinePathGenerator _splinePathGenerator;

        [Test]
        public void TestSegmentLengthThatResultsInMinimumSegmentCount()
        {
            var point0 = new Vector3(0, 0, 0);
            var point1 = new Vector3(1, 0, 0);
            var point2 = new Vector3(9, 0, 0);
            var point3 = new Vector3(10, 0, 0);
            var controlPoints = new List<Vector3> {point0, point1, point2, point3};

            Expect.MethodCall(() => _cubicSplineInterpolator.Interpolate(1/3.0, 0.5, point0, point1, point2, point3));
            Expect.MethodCall(() => _cubicSplineInterpolator.Interpolate(2/3.0, 0.5, point0, point1, point2, point3));

            var result = _splinePathGenerator.Generate(controlPoints, 10, 0.5);
            Assert.AreEqual(result.Count, 1 + 2 + 1); //point1 + 2 interpolated points + point2
            result[0].AssertAreEqual(point1);
            result[3].AssertAreEqual(point2);
        }

        [Test]
        public void TestSegmentLengthThatResultsInThreeInterpolatedPoints()
        {
            var point0 = new Vector3(0, 0, 0);
            var point1 = new Vector3(1, 0, 0);
            var point2 = new Vector3(10, 0, 0);
            var point3 = new Vector3(11, 0, 0);
            var controlPoints = new List<Vector3> {point0, point1, point2, point3};

            Expect.MethodCall(() => _cubicSplineInterpolator.Interpolate(.25, 0.5, point0, point1, point2, point3));
            Expect.MethodCall(() => _cubicSplineInterpolator.Interpolate(.5, 0.5, point0, point1, point2, point3));
            Expect.MethodCall(() => _cubicSplineInterpolator.Interpolate(.75, 0.5, point0, point1, point2, point3));

            var result = _splinePathGenerator.Generate(controlPoints, 2.25, 0.5);
            Assert.AreEqual(result.Count, 1 + 3 + 1); //point1 + 3 interpolated points + point2
            result[0].AssertAreEqual(point1);
            result[4].AssertAreEqual(point2);
        }
    }
}