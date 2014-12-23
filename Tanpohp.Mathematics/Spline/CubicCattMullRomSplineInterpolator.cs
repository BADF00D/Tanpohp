namespace Tanpohp.Mathematics.Spline
{
    using System;
    using System.Collections.Generic;

    internal class CubicCattMullRomSplineInterpolator : ICubicSplineInterpolator
    {
        private const float Uniform = 0f;
        private readonly double[] _times = new double[4];
        private readonly double[] _xs = new double[4];
        private readonly double[] _ys = new double[4];
        private readonly double[] _zs = new double[4];

        public Vector3 Interpolate(double time, double curveType, Vector3 point0, Vector3 point1, Vector3 point2,
            Vector3 point3)
        {
            CopyComponents(point0, point1, point2, point3);
            AssignDefaultTimeWeights();

            if (IsNonUniformCurve(curveType))
            {
                AdjustTimesParameter(curveType);
            }
            var percent = _times[1] + time*(_times[2] - _times[1]);
            var xi = Interpolate(_xs, _times, percent);
            var yi = Interpolate(_ys, _times, percent);
            var zi = Interpolate(_zs, _times, percent);

            return new Vector3(xi, yi, zi);
        }

        private void AssignDefaultTimeWeights()
        {
            for (var i = 0; i < 4; i++)
                _times[i] = i;
        }

        private void CopyComponents(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3)
        {
            _xs[0] = point0.X;
            _xs[1] = point1.X;
            _xs[2] = point2.X;
            _xs[3] = point3.X;
            _ys[0] = point0.Y;
            _ys[1] = point1.Y;
            _ys[2] = point2.Y;
            _ys[3] = point3.Y;
            _zs[0] = point0.Z;
            _zs[1] = point1.Z;
            _zs[2] = point2.Z;
            _zs[3] = point3.Z;
        }

        private static bool IsNonUniformCurve(double curveType)
        {
            return curveType > Uniform;
        }

        private void AdjustTimesParameter(double curveType)
        {
            var total = 0.0;
            var exponent = .5f*curveType; //.5f is same as sqrt
            for (var i = 01; i < 4; i++)
            {
                var dx = _xs[i] - _xs[i - 1];
                var dy = _ys[i] - _ys[i - 1];
                var dz = _zs[i] - _zs[i - 1];
                total += Math.Pow(dx*dx + dy*dy + dz*dz, exponent);
                _times[i] = (float) total;
            }
        }

        private static float Interpolate(IList<double> points, IList<double> times, double time)
        {
            var l01 = points[0]*(times[1] - time)/(times[1] - times[0]) +
                      points[1]*(time - times[0])/(times[1] - times[0]);
            var l12 = points[1]*(times[2] - time)/(times[2] - times[1]) +
                      points[2]*(time - times[1])/(times[2] - times[1]);
            var l23 = points[2]*(times[3] - time)/(times[3] - times[2]) +
                      points[3]*(time - times[2])/(times[3] - times[2]);
            var l012 = l01*(times[2] - time)/(times[2] - times[0]) + l12*(time - times[0])/(times[2] - times[0]);
            var l123 = l12*(times[3] - time)/(times[3] - times[1]) + l23*(time - times[1])/(times[3] - times[1]);
            var c12 = l012*(times[2] - time)/(times[2] - times[1]) + l123*(time - times[1])/(times[2] - times[1]);
            return (float) c12;
        }
    }
}