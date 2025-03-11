using LavenderRender.Data;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Interface;

public interface IGeometry
{
    public Guid Guid { get; set; }
    public HitRecord Intersect(Ray ray);
    public ReflectType ReflectType { get; set; }
    public ColorRgb8 Color { get; set; }
}
