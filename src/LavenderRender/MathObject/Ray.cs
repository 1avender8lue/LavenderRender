using LavenderRender.Data;
using LavenderRender.Interface;
using System;
using System.Collections.Generic;

namespace LavenderRender.MathObject;

public class Ray
{
    public Point Position { get; set; }
    public UnitDirection Direction { get; set; }

    public Ray(Point position, UnitDirection direction)
    {
        Position = position;
        Direction = direction;
    }

    public HitRecord HitNearest(Dictionary<Guid, IGeometry> geometries)
    {
        double minT = 1e30d;
        HitRecord record = HitRecord.Empty;

        foreach (var geometryKeyValue in geometries)
        {
            HitRecord hitRecord = geometryKeyValue.Value.Intersect(this);

            if (hitRecord.T > 0d && hitRecord.T < minT)
            {
                minT = hitRecord.T;
                record = hitRecord;
            }
            else
            {
                continue;
            }
        }

        if (record != HitRecord.Empty)
        {
            return record;
        }
        else
        {
            return HitRecord.Empty;
        }
    }
}
