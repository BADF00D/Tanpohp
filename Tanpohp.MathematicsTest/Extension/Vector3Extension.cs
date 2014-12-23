namespace Tanpohp.MathematicsTest.Extension
{
    using Mathematics;
    using NUnit.Framework;

    public static class Vector3Extension
    {
        public static void AssertAreEqual(this Vector3 one, Vector3 two, double delta = 0.0001)
        {
            Assert.AreEqual(one.X, two.X, delta, "X");
            Assert.AreEqual(one.Y, two.Y, delta, "Y");
            Assert.AreEqual(one.Z, two.Z, delta, "Z");
        }

        public static void AssertAreEqual(this Vector3 one, Vector3 two, Vector3 delta)
        {
            Assert.AreEqual(one.X, two.X, delta.X, "X");
            Assert.AreEqual(one.Y, two.Y, delta.Y, "Y");
            Assert.AreEqual(one.Z, two.Z, delta.Z, "Z");
        }
    }
}