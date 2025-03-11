using LavenderRender.Data;
using LavenderRender.Interface;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Geometries;

public class Sphere : ISphere
{
    public required double Radius { get; set; }
    public required Point Position { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public required ColorRgb8 Color { get; set; }
    public required ReflectType ReflectType { get; set; }
}
