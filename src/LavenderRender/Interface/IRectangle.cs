using LavenderRender.Data;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Interface;

public interface IRectangle : IGeometry
{
    public const double EPSILON = 0.0001d;

    public UnitDirection Normal { get; set; }
    public Point Position { get; set; }
    public UnitDirection AxisX { get; set; }
    public UnitDirection AxisY { get; }
    public double HalfLengthX { get; set; }
    public double HalfLengthY { get; set; }

    public double LengthX => 2.0 * HalfLengthX;
    public double LengthY => 2.0 * HalfLengthY;
    public double Area => 4 * HalfLengthX * HalfLengthY;

    HitRecord IGeometry.Intersect(Ray ray)
    {
        double maxT = 1e30d;

        if (ray.Direction * Normal == 0.0d)
        {
            return HitRecord.Empty;
        }

        double t = (Position - ray.Position) * Normal / (ray.Direction * Normal);
        Point intersectPoint = ray.Position + t * ray.Direction;
        Direction direction = intersectPoint - Position;
        bool isInArea = Math.Abs(direction * AxisX) < HalfLengthX && Math.Abs(direction * AxisY) < HalfLengthY;

        if (t > EPSILON && t < maxT && isInArea == true)
        {
            Point hitPoint = ray.Position + t * ray.Direction;

            HitRecord hitRecord = new HitRecord()
            {
                HitAny = true,
                HitGuid = Guid,
                T = t,
                HitFrom = ray.Direction,
                HitPoint = hitPoint,
                HitNormal = Normal
            };
            return hitRecord;
        }
        else
        {
            return HitRecord.Empty;
        }
    }
}
