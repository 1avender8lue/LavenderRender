namespace LavenderRender.MathObject;

public class UnitDirection
{
    public static UnitDirection UnitX => new UnitDirection(Vector.UnitX);
    public static UnitDirection UnitY => new UnitDirection(Vector.UnitY);
    public static UnitDirection UnitZ => new UnitDirection(Vector.UnitZ);
    public static UnitDirection Empty => new UnitDirection(Vector.Zero);

    public double X => _vector.X;
    public double Y => _vector.Y;
    public double Z => _vector.Z;

    internal Vector _vector;

    public UnitDirection(Point startPoint, Point endPoint)
    {
        _vector = endPoint._vector - startPoint._vector;
    }

    public UnitDirection(Vector vector)
    {
        _vector = vector;
    }

    public static Direction operator +(UnitDirection leftUnitDirection, UnitDirection rightUnitDirection)
    {
        return new Direction(leftUnitDirection._vector + rightUnitDirection._vector);
    }

    public static Direction operator +(UnitDirection unitDirection, Direction direction)
    {
        return new Direction(unitDirection._vector + direction._vector);
    }

    public static UnitDirection operator -(UnitDirection unitDirection)
    {
        return new UnitDirection(-unitDirection._vector);
    }

    public static Direction operator -(UnitDirection leftUnitDirection, UnitDirection rightUnitDirection)
    {
        return new Direction(leftUnitDirection._vector - rightUnitDirection._vector);
    }

    public static Direction operator -(UnitDirection unitDirection, Direction direction)
    {
        return new Direction(unitDirection._vector - direction._vector);
    }

    public static Direction operator *(double scalar, UnitDirection unitDirection)
    {
        return new Direction(scalar * unitDirection._vector);
    }

    public static Direction operator *(UnitDirection unitDirection, double scalar)
    {
        return new Direction(unitDirection._vector * scalar);
    }

    public static double operator *(UnitDirection left, UnitDirection right)
    {
        return left._vector * right._vector;
    }

    public static double operator *(UnitDirection left, Direction right)
    {
        return left._vector * right._vector;
    }

    public static Direction operator %(UnitDirection left, UnitDirection right)
    {
        return new Direction(left._vector % right._vector);
    }

    public static Direction operator %(UnitDirection left, Direction right)
    {
        return new Direction(left._vector % right._vector);
    }
}

