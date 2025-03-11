using LavenderRender.MathObject;
using System;

namespace LavenderRender.Camera;

public class PinHoleCamera
{
    private readonly Random _random = new Random();
    public required int SampleLevelPerPixel { get; set; }
    public required int SamplePerLight { get; set; }

    public required Point Position { get; set; }
    public UnitDirection LookAt { get; set; } = UnitDirection.UnitY;
    public UnitDirection Right { get; set; } = UnitDirection.UnitX;
    public UnitDirection Up { get; set; } = UnitDirection.UnitZ;

    public required int ResolutionX { get; set; }
    public required int ResolutionY { get; set; }

    public double PixelWidth => Width / ResolutionX;
    public double PixelHeight => Height / ResolutionY;

    public required double FOV { get; set; }
    public required double Distance { get; set; }
    public double Width => 2 * Distance * Math.Tan(FOV * 0.5d);
    public double Height => 2 * Distance * Math.Tan(FOV * 0.5d);

    public Point UpperLeft => Position + Distance * LookAt - 0.5d * Width * Right + 0.5d * Height * Up + 0.5 * PixelWidth * Right - 0.5 * PixelHeight * Up;


    public Ray RandomCameraRay(int pixelCoordinateX, int pixelCoordinateY)
    {
        double randomX = _random.NextDouble() - 0.5d;
        double randomY = _random.NextDouble() - 0.5d;
        Point samplePoint = UpperLeft + (pixelCoordinateX + randomX) * PixelWidth * Right - (pixelCoordinateY + randomY) * PixelHeight * Up;
        UnitDirection sampleDirection = (samplePoint - Position).Normalize();

        return new Ray(Position, sampleDirection);
    }
}
