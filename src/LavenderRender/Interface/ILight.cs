using LavenderRender.Data;
using LavenderRender.MathObject;
using System;

namespace LavenderRender.Interface;

public interface ILight
{
    public Random Random { get; }
    public double Intensity { get; set; }
    public ColorRgb8 LightColor { get; set; }
    public ColorRgb8 Light => LightColor * Intensity;
    public double SampleProbability { get; }

    public LightSample RandomSample(UnitDirection toward);

}
