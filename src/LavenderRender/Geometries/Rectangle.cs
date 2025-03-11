using LavenderRender.Data;
using LavenderRender.Interface;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Geometries;

public class Rectangle : IRectangle
{
    public required UnitDirection Normal { get; set; }
    public required Point Position { get; set; }
    public required double HalfLengthX { get; set; }
    public required double HalfLengthY { get; set; }
    public required UnitDirection AxisX { get; set; }
    public UnitDirection AxisY => (Normal % AxisX).Normalize();
    public Guid Guid { get; set; } = Guid.NewGuid();
    public required ReflectType ReflectType { get; set; }
    public required ColorRgb8 Color { get; set; }
}
