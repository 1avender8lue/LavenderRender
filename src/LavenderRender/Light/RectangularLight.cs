using LavenderRender.Data;
using LavenderRender.Interface;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Light;

public class RectangularLight : IRectangle, ILight
{
    public required double Intensity { get; set; }
    public required ColorRgb8 LightColor { get; set; }

    public double SampleProbability => 1.0d / Area;

    public required UnitDirection Normal { get; set; }
    public required Point Position { get; set; }
    public required UnitDirection AxisX { get; set; }
    public required double HalfLengthX { get; set; }
    public required double HalfLengthY { get; set; }

    public double LengthX => 2.0 * HalfLengthX;
    public double LengthY => 2.0 * HalfLengthY;
    public Guid Guid { get; set; } = Guid.NewGuid();
    public ReflectType ReflectType { get; set; } = ReflectType.None;
    public ColorRgb8 Color { get; set; } = ColorRgb8.None;

    public double Area => 4.0d * HalfLengthX * HalfLengthY;

    public Random Random { get; } = new Random();

    public UnitDirection AxisY => (Normal % AxisX).Normalize();

    public LightSample RandomSample(UnitDirection toward)
    {
        double randomX = Random.NextDouble();
        double randomY = Random.NextDouble();

        Point randomPoint = Position - HalfLengthX * AxisX - HalfLengthY * AxisY + randomX * LengthX * AxisX + randomY * LengthY * AxisY;

        return new LightSample(randomPoint, Normal);
    }
}
