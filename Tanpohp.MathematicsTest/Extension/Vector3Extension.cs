namespace Tanpohp.MathematicsTest.Extension
{
    using Mathematics;
    using NUnit.Framework;

    public static class Vector3Extension
    {
        public static void AssertAreEqual(this Vector3 actual, Vector3 expected, double delta = 0.0001)
        {
            Assert.AreEqual(expected.X, actual.X, delta, "X");
            Assert.AreEqual(expected.Y, actual.Y, delta, "Y");
            Assert.AreEqual(expected.Z, actual.Z, delta, "Z");
        }

        public static void AssertAreEqual(this Vector3 actual, Vector3 expected, Vector3 delta)
        {
            Assert.AreEqual(expected.X, actual.X, delta.X, "X");
            Assert.AreEqual(expected.Y, actual.Y, delta.Y, "Y");
            Assert.AreEqual(expected.Z, actual.Z, delta.Z, "Z");
        }
    }
}