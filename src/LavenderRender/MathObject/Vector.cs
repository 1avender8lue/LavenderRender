using System;

namespace LavenderRender.MathObject;

public class Vector
{
    public double X { get; set; }
    public double Y { get; set; }

    public double Z { get; set; }

    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static Vector UnitX => new Vector(1.0d, 0.0d, 0.0d);
    public static Vector UnitY => new Vector(0.0d, 1.0d, 0.0d);
    public static Vector UnitZ => new Vector(0.0d, 0.0d, 1.0d);
    public static Vector Zero => new Vector(0.0d, 0.0d, 0.0d);

    public Vector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector Normalize()
    {
        return new Vector(X / Length, Y / Length, Z / Length);
    }

    public static Vector operator +(Vector left, Vector right)
    {
        return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Vector operator -(Vector vector)
    {
        return new Vector(-vector.X, -vector.Y, -vector.Z);
    }

    public static Vector operator -(Vector left, Vector right)
    {
        return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static double operator *(Vector left, Vector right)
    {
        return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
    }

    public static Vector operator *(Vector vector, double scale)
    {
        return new Vector(vector.X * scale, vector.Y * scale, vector.Z * scale);
    }

    public static Vector operator *(double scale, Vector vector)
    {
        return new Vector(scale * vector.X, scale * vector.Y, scale * vector.Z);
    }

    public static Vector operator /(Vector vector, double scale)
    {
        return new Vector(vector.X / scale, vector.Y / scale, vector.Z / scale);
    }

    public static Vector operator %(Vector left, Vector right)
    {
        return new Vector(left.Y * right.Z - left.Z * right.Y, -left.X * right.Z + left.Z * right.X, left.X * right.Y - left.Y * right.X);
    }
}
