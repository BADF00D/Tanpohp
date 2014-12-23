namespace Tanpohp.Mathematics.Spline
{
    using System.Collections.Generic;

    public interface ISplinePathGenerator
    {
        IList<Vector3> Generate(IList<Vector3> controlPoints, double lengthPerSegment, double splineParameter);
    }
}