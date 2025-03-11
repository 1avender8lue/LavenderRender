using LavenderRender.Data;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Interface;

public interface ISphere : IGeometry
{
    public const double EPSILON = 0.0001d;

    public double Radius { get; set; }
    public Point Position { get; set; }

    HitRecord IGeometry.Intersect(Ray ray)
    {
        Direction op = Position - ray.Position;
        double b = op * ray.Direction;
        double delta = b * b - op * op + Radius * Radius;

        if (delta < 0.0d)
        {
            return HitRecord.Empty;
        }

        delta = Math.Sqrt(delta);
        double t1 = b - delta;
        double t2 = b + delta;

        if (t1 > EPSILON)
        {
            Point hitPoint = ray.Position + t1 * ray.Direction;
            UnitDirection hitNormal = (hitPoint - Position).Normalize();

            HitRecord hitRecord = new HitRecord()
            {
                HitAny = true,
                HitGuid = Guid,
                T = t1,
                HitFrom = ray.Direction,
                HitPoint = hitPoint,
                HitNormal = hitNormal
            };
            return hitRecord;
        }
        else if (t2 > EPSILON)
        {
            Point hitPoint = ray.Position + t2 * ray.Direction;
            UnitDirection hitNormal = (hitPoint - Position).Normalize();

            HitRecord hitRecord = new HitRecord()
            {
                HitAny = true,
                HitGuid = Guid,
                T = t2,
                HitFrom = ray.Direction,
                HitPoint = hitPoint,
                HitNormal = hitNormal
            };
            return hitRecord;
        }
        else
        {
            return HitRecord.Empty;
        }
    }
}
