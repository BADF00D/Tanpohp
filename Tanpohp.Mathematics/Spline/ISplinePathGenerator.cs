namespace Tanpohp.Mathematics.Spline
{
    using System.Collections.Generic;

    public interface ISplinePathGenerator
    {
        /// <summary>
        /// Generates a spline between given control points.
        /// </summary>
        /// <param name="controlPoints">List of control point. First and last are not part of resulting spline.</param>
        /// <param name="lengthPerSegment">Approximate lenth between two resulting interpolated points.</param>
        /// <param name="splineParameter">Spline parameter passed to spline interpolation algorithm.</param>
        /// <returns>List of Vector3 that thats at second control point and an stops at next to last control point.</returns>
        IList<Vector3> Generate(IList<Vector3> controlPoints, double lengthPerSegment, double splineParameter);
    }
}