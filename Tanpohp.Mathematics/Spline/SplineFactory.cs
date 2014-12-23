namespace Tanpohp.Mathematics.Spline
{
    public static class SplineFactory
    {
        public static ICubicSplineInterpolator GetCattMullRomSplineInterpoltor()
        {
            return new CubicCattMullRomSplineInterpolator();
        }

        public static ISplinePathGenerator GetSplinePathGenerator(ICubicSplineInterpolator splineInterpolator)
        {
            return new SplinePathGenerator(splineInterpolator);
        }
    }
}