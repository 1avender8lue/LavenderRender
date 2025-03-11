using LavenderRender.MathObject;
using System;

namespace LavenderRender.Data;

public class HitRecord
{
    public required bool HitAny { get; set; }
    public required Guid HitGuid { get; set; }
    public required double T { get; set; }
    public required Point HitPoint { get; set; }
    public required UnitDirection HitFrom { get; set; }
    public required UnitDirection HitNormal { get; set; }
    public static HitRecord Empty => new HitRecord
    {
        HitAny = false,
        HitGuid = Guid.Empty,
        T = 0d,
        HitPoint = Point.Empty,
        HitFrom = UnitDirection.Empty,
        HitNormal = UnitDirection.Empty
    };
}
