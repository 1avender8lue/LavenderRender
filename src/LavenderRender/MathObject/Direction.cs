using System;

namespace LavenderRender.MathObject;

public class Direction
{
    internal Vector _vector;

    public double Length => Math.Sqrt(_vector * _vector);

    public Direction(Point startPoint, Point endPoint)
    {
        _vector = endPoint._vector - startPoint._vector;
    }

    public Direction(Vector vector)
    {
        _vector = vector;
    }

    public Direction(double x, double y, double z)
    {
        _vector = new Vector(x, y, z);
    }

    public UnitDirection Normalize()
    {
        return new UnitDirection(_vector.Normalize());
    }

    public static Direction operator +(Direction leftDirection, Direction rightDirection)
    {
        return new Direction(leftDirection._vector + rightDirection._vector);
    }

    public static Direction operator +(Direction direction, UnitDirection unitDirection)
    {
        return new Direction(direction._vector + unitDirection._vector);
    }

    public static Direction operator -(Direction leftDirection, Direction rightDirection)
    {
        return new Direction(leftDirection._vector - rightDirection._vector);
    }

    public static Direction operator -(Direction direction, UnitDirection unitDirection)
    {
        return new Direction(direction._vector - unitDirection._vector);
    }

    public static Direction operator *(double scalar, Direction direction)
    {
        return new Direction(scalar * direction._vector);
    }

    public static Direction operator *(Direction direction, double scalar)
    {
        return new Direction(direction._vector * scalar);
    }

    public static double operator *(Direction left, Direction right)
    {
        return left._vector * right._vector;
    }

    public static double operator *(Direction left, UnitDirection right)
    {
        return left._vector * right._vector;
    }

    public static Direction operator %(Direction left, Direction right)
    {
        return new Direction(left._vector % right._vector);
    }

    public static Direction operator %(Direction left, UnitDirection right)
    {
        return new Direction(left._vector % right._vector);
    }
}
