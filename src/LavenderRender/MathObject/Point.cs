namespace LavenderRender.MathObject;

public class Point
{
    internal Vector _vector;
    public double X { get => _vector.X; set => _vector.X = value; }
    public double Y { get => _vector.Y; set => _vector.Y = value; }
    public double Z { get => _vector.Z; set => _vector.Z = value; }

    public static Point Empty => new Point(Vector.Zero);

    public Point(double x, double y, double z)
    {
        _vector = new Vector(x, y, z);
    }

    public Point(Vector vector)
    {
        _vector = vector;
    }

    public static Point operator +(Point startPoint, Direction direction)
    {
        return new Point(startPoint._vector + direction._vector);
    }

    public static Point operator +(Point startPoint, UnitDirection unitDirection)
    {
        return new Point(startPoint._vector + unitDirection._vector);
    }

    public static Direction operator -(Point endPoint, Point startPoint)
    {
        return new Direction(endPoint._vector - startPoint._vector);
    }

    public static Point operator -(Point startPoint, Direction direction)
    {
        return new Point(startPoint._vector - direction._vector);
    }

    public static Point operator -(Point startPoint, UnitDirection unitDirection)
    {
        return new Point(startPoint._vector - unitDirection._vector);
    }
}
