namespace Tanpohp.Mathematics.Spline
{
    using System;
    using System.Collections.Generic;

    public class SplinePathGenerator : ISplinePathGenerator
    {
        private readonly ICubicSplineInterpolator _splineInterpolator;

        public SplinePathGenerator(ICubicSplineInterpolator splineInterpolator)
        {
            _splineInterpolator = splineInterpolator;
        }

        public IList<Vector3> Generate(IList<Vector3> controlPoints, double lengthPerSegment, double splineParameter)
        {
            var path = new List<Vector3>();
            for (var i = 1; i < controlPoints.Count - 2; i++)
            {
                var point0 = controlPoints[i - 1];
                var point1 = controlPoints[i];
                var point2 = controlPoints[i + 1];
                var point3 = controlPoints[i + 2];

                var segments = (float)Math.Max(3, Math.Ceiling(point1.DistanceTo(ref point2) / lengthPerSegment));
                path.Add(point1);
                for (var j = 1; j < segments; j++)
                {
                    var time = j / segments;
                    var interpolatedPoint = _splineInterpolator.Interpolate(time, splineParameter, point0, point1, point2, point3);
                    path.Add(interpolatedPoint);
                }
            }
            path.Add(controlPoints[controlPoints.Count - 2]);

            return path;
        }
    }
}