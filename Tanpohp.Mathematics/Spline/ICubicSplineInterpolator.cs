namespace Tanpohp.Mathematics.Spline
{
    public interface ICubicSplineInterpolator
    {
        Vector3 Interpolate(double time, double curveType, Vector3 point0, Vector3 point1, Vector3 point2,
            Vector3 point3);
    }
}