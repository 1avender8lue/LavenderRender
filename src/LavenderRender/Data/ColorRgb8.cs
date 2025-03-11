using System;

namespace LavenderRender.Data;

public class ColorRgb8
{
    public static ColorRgb8 Red => new ColorRgb8(1.0d, 0.0d, 0.0d);
    public static ColorRgb8 Green => new ColorRgb8(0.0d, 1.0d, 0.0d);
    public static ColorRgb8 Blue => new ColorRgb8(0.0d, 0.0d, 1.0d);
    public static ColorRgb8 White => new ColorRgb8(1.0d, 1.0d, 1.0d);
    public static ColorRgb8 Black => new ColorRgb8(0.0d, 0.0d, 0.0d);
    public static ColorRgb8 None => new ColorRgb8(0.0d, 0.0d, 0.0d);



    public double R { get; set; }
    public double G { get; set; }
    public double B { get; set; }

    public ColorRgb8(double r, double g, double b)
    {
        R = r;
        G = g;
        B = b;
    }

    public System.Drawing.Color ToSystemColor()
    {
        int red = Math.Clamp(Convert.ToInt32(Math.Round(R * 255.0d, MidpointRounding.ToPositiveInfinity)), 0, 255);
        int green = Math.Clamp(Convert.ToInt32(Math.Round(G * 255.0d, MidpointRounding.ToPositiveInfinity)), 0, 255);
        int blue = Math.Clamp(Convert.ToInt32(Math.Round(B * 255.0d, MidpointRounding.ToPositiveInfinity)), 0, 255);

        return System.Drawing.Color.FromArgb(255, red, green, blue);
    }

    public static ColorRgb8 operator +(ColorRgb8 color1, ColorRgb8 color2)
    {
        return new ColorRgb8(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
    }
    public static ColorRgb8 operator *(ColorRgb8 color1, ColorRgb8 color2)
    {
        return new ColorRgb8(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B);
    }
    public static ColorRgb8 operator *(double number, ColorRgb8 color)
    {
        return new ColorRgb8(number * color.R, number * color.G, number * color.B);
    }
    public static ColorRgb8 operator *(ColorRgb8 color, double number)
    {
        return new ColorRgb8(color.R * number, color.G * number, color.B * number);
    }
    public static ColorRgb8 operator /(ColorRgb8 color, double number)
    {
        return new ColorRgb8(color.R / number, color.G / number, color.B / number);
    }
}
