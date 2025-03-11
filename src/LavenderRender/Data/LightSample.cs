using LavenderRender.MathObject;

namespace LavenderRender.Data;

public class LightSample
{
    public Point Position { get; set; }
    public UnitDirection Normal { get; set; }

    public LightSample(double x, double y, double z, UnitDirection normal)
    {
        Position = new Point(x, y, z);
        Normal = normal;
    }

    public LightSample(Vector vector, UnitDirection normal)
    {
        Position = new Point(vector);
        Normal = normal;
    }

    public LightSample(Point point, UnitDirection normal)
    {
        Position = point;
        Normal = normal;
    }

    public static Direction operator -(LightSample samplePoint, Point point)
    {
        return samplePoint.Position - point;
    }

}