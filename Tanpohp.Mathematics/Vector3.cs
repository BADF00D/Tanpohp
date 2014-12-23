namespace Tanpohp.Mathematics
{
    using System;
    using System.Runtime.InteropServices;
    using Extensions;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public double X;
        public double Y;
        public double Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return "({0:00.00}/{1:00.00}/{2:00.00})".FormatWith(X, Y, Z);
        }

        public static Vector3 operator +(Vector3 summand1, Vector3 summand2)
        {
            return new Vector3(summand1.X + summand2.X, summand1.Y + summand2.Y, summand1.Z + summand2.Z);
        }

        public static Vector3 operator -(Vector3 minuend, Vector3 subtrahend)
        {
            return new Vector3(minuend.X - subtrahend.X, minuend.Y - subtrahend.Y, minuend.Z - subtrahend.Z);
        }

        public static Vector3 operator *(float scalar, Vector3 factor)
        {
            return new Vector3(scalar*factor.X, scalar*factor.Y, scalar*factor.Z);
        }

        public static Vector3 operator *(Vector3 factor, float scalar)
        {
            return new Vector3(scalar * factor.X, scalar * factor.Y, scalar * factor.Z);
        }

        public static double operator *(Vector3 scalar1, Vector3 scalar2)
        {
            return scalar1.X*scalar2.X + scalar1.Y*scalar2.Y + scalar1.Z*scalar2.Z;
        }

        public static double Scalar(ref Vector3 scalar1, ref Vector3 scalar2)
        {
            return scalar1.X*scalar2.X + scalar1.Y*scalar2.Y + scalar1.Z*scalar2.Z;
        }

        public float Length()
        {
            return (float) Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return X*X + Y*Y + Z*Z;
        }

        public static Vector3 Cross(Vector3 one, Vector3 two)
        {
            return new Vector3(one.Y*two.Z - one.Z*two.Y, one.Z*two.X - one.X*two.Z, one.X*two.Y - one.Y*two.X);
        }

        public static double Angle(Vector3 one, Vector3 two)
        {
            return Math.Acos(Scalar(ref one, ref two)/(one.Length()*two.Length()));
        }

        public void Normalize()
        {
            var inverseLength = 1.0/Length();
            X *= inverseLength;
            Y *= inverseLength;
            Z *= inverseLength;
        }

        public void Clone(out Vector3 clone)
        {
            clone.X = X;
            clone.Y = Y;
            clone.Z = Z;
        }

        public Vector3 Clone()
        {
            return new Vector3(X, Y, Z);
        }

        public double DistanceTo(ref Vector3 two)
        {
            return
                Math.Sqrt((X - two.X)*(X - two.X) +
                          (Y - two.Y)*(Y - two.Y) +
                          (Z - two.Z)*(Z - two.Z));
        }

        public static Vector3 UnitX
        {
            get { return new Vector3(1, 0, 0); }
        }

        public static Vector3 UnitY
        {
            get { return new Vector3(0, 1, 0); }
        }

        public static Vector3 UnitZ
        {
            get { return new Vector3(0, 0, 1); }
        }

        public static Vector3 One
        {
            get { return new Vector3(1, 1, 1); }
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }

    }
}