namespace Tanpohp.Mathematics.Spline
{
    public interface ICubicSplineInterpolator
    {
        /// <summary>
        /// Interpolates a point between control point 1 and 2 at postion specified by time.
        /// </summary>
        /// <param name="time">Value [0,1] that controls postion between point1 and point2 that should be interpolated.</param>
        /// <param name="curveType">Value [0,1] that controls curvature. 0.0 - unform, 0.5 - centripental (no self intersection), 1.0 - chordal.</param>
        /// <param name="point0">Control point 0.</param>
        /// <param name="point1">Control point 1.</param>
        /// <param name="point2">Control point 2.</param>
        /// <param name="point3">Control point 3.</param>
        /// <returns>Interplated points between point1 and point2.</returns>
        Vector3 Interpolate(double time, double curveType, Vector3 point0, Vector3 point1, Vector3 point2,
            Vector3 point3);
    }
}