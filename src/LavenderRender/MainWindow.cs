using LavenderRender.Camera;
using LavenderRender.Data;
using LavenderRender.Geometries;
using LavenderRender.Interface;
using LavenderRender.Light;
using LavenderRender.MathObject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavenderRender;

public partial class MainWindow : Form
{
    private Random _random = new Random();
    private double RussianRoulette = 0.8d;

    private Bitmap _primaryBuffer;

    //private Bitmap _bounceBuffer0;
    //private Bitmap _bounceBuffer1;
    //private Bitmap _bounceBuffer2;
    //private Bitmap _bounceBuffer3;
    //private Bitmap _bounceBuffer4;
    //private Bitmap _bounceBuffer5;

    private Dictionary<Guid, IGeometry> Geometries { get; } = new Dictionary<Guid, IGeometry>();
    private PinHoleCamera _camera;

    public MainWindow()
    {
        InitializeComponent();

        #region initialize buffer
        _primaryBuffer = new Bitmap
        (
            width: _pictureBox.Width,
            height: _pictureBox.Height,
            format: PixelFormat.Format32bppArgb
        );

        for (int i = 0; i < _pictureBox.Height; i++)
        {
            for (int j = 0; j < _pictureBox.Width; j++)
            {
                _primaryBuffer.SetPixel
                (
                    x: i,
                    y: j,
                    color: System.Drawing.Color.Black
                );
            }
        }

        _pictureBox.Image = _primaryBuffer;
        _pictureBox.Invalidate();
        #endregion

        _camera = new PinHoleCamera()
        {
            Position = new MathObject.Point(0d, -2.5d, 0d),
            FOV = 45d,
            Distance = 0.5d,
            ResolutionX = _pictureBox.Width,
            ResolutionY = _pictureBox.Height,
            SamplePerLight = 5,
            SampleLevelPerPixel = 10000
        };

        #region initialize Cornell Box

        Sphere leftSphere = new Sphere()
        {
            Position = new MathObject.Point(-0.45d, 0.35d, -0.7d),
            Radius = 0.3d,
            ReflectType = ReflectType.Specular,
            Color = new ColorRgb8(0.9d, 0.9d, 0.9d)
        };
        Geometries.Add(leftSphere.Guid, leftSphere);

        Sphere rightSphere = new Sphere()
        {
            Position = new MathObject.Point(0.4d, -0.3d, -0.65d),
            Radius = 0.35d,
            ReflectType = ReflectType.Diffuse,
            Color = new ColorRgb8(0.9d, 0.9d, 0.9d)
        };
        Geometries.Add(rightSphere.Guid, rightSphere);


        Geometries.Rectangle leftWall = new Geometries.Rectangle()
        {
            Position = new MathObject.Point(-1d, 0d, 0d),
            Normal = UnitDirection.UnitX,
            AxisX = UnitDirection.UnitY,
            HalfLengthX = 1d,
            HalfLengthY = 1d,
            Color = new ColorRgb8(0.75d, 0.25d, 0.25d),
            ReflectType = ReflectType.Diffuse
        };
        Geometries.Add(leftWall.Guid, leftWall);

        Geometries.Rectangle rightWall = new Geometries.Rectangle()
        {
            Position = new MathObject.Point(1d, 0d, 0d),
            Normal = -UnitDirection.UnitX,
            AxisX = -UnitDirection.UnitY,
            HalfLengthX = 1d,
            HalfLengthY = 1d,
            Color = new ColorRgb8(0.25d, 0.25d, 0.75d),
            ReflectType = ReflectType.Diffuse
        };
        Geometries.Add(rightWall.Guid, rightWall);

        Geometries.Rectangle bottomWall = new Geometries.Rectangle()
        {
            Position = new MathObject.Point(0d, 0d, -1d),
            Normal = UnitDirection.UnitZ,
            AxisX = UnitDirection.UnitX,
            HalfLengthX = 1d,
            HalfLengthY = 1d,
            Color = new ColorRgb8(0.75d, 0.75d, 0.75d),
            ReflectType = ReflectType.Diffuse
        };
        Geometries.Add(bottomWall.Guid, bottomWall);

        Geometries.Rectangle topWall = new Geometries.Rectangle()
        {
            Position = new MathObject.Point(0d, 0d, 1.0d),
            Normal = -UnitDirection.UnitZ,
            AxisX = UnitDirection.UnitX,
            HalfLengthX = 1d,
            HalfLengthY = 1d,
            Color = new ColorRgb8(0.75d, 0.75d, 0.75d),
            ReflectType = ReflectType.Diffuse
        };
        Geometries.Add(topWall.Guid, topWall);

        Geometries.Rectangle frontWall = new Geometries.Rectangle()
        {
            Position = new MathObject.Point(0d, 1d, 0d),
            Normal = -UnitDirection.UnitY,
            AxisX = UnitDirection.UnitX,
            HalfLengthX = 1d,
            HalfLengthY = 1d,
            Color = new ColorRgb8(0.75d, 0.75d, 0.75d),
            ReflectType = ReflectType.Diffuse
        };
        Geometries.Add(frontWall.Guid, frontWall);

        // top light
        RectangularLight testLight3 = new RectangularLight()
        {
            Position = new MathObject.Point(0d, 0.0d, 0.999999d),
            Normal = -UnitDirection.UnitZ,
            AxisX = UnitDirection.UnitX,
            LightColor = ColorRgb8.White,
            HalfLengthX = 0.3d,
            HalfLengthY = 0.2d,
            Intensity = 40d
        };
        Geometries.Add(testLight3.Guid, testLight3);

        #endregion
    }


    private async void RenderButton_Click(object sender, EventArgs e)
    {
        _renderButton.Enabled = false;
        _saveButton.Enabled = false;

        Task task = Task.Run(
            () =>
            {
                // iterate all pixel
                for (int i = 0; i < _camera.ResolutionX; i++)
                {
                    for (int j = 0; j < _camera.ResolutionY; j++)
                    {
                        ColorRgb8 color = ColorRgb8.Black;

                        // take samples in one pixel
                        for (int k = 0; k < _camera.SampleLevelPerPixel; k++)
                        {
                            Ray cameraRay = _camera.RandomCameraRay(i, j);

                            HitRecord hitRecord = cameraRay.HitNearest(Geometries);

                            // hit miss
                            if (hitRecord.HitAny == false)
                            {
                                color += ColorRgb8.Black;
                                continue;
                            }

                            IGeometry hitObject = Geometries[hitRecord.HitGuid];

                            // hit light
                            if (hitObject is ILight light)
                            {
                                color += hitRecord.HitNormal * (-hitRecord.HitFrom) * light.Light;
                            }
                            // hit none-light
                            else
                            {
                                color += Illumination(hitRecord, 1);
                            }
                        }

                        color = color / _camera.SampleLevelPerPixel;

                        System.Drawing.Color systemColor = color.ToSystemColor();

                        _primaryBuffer.SetPixel(i, j, systemColor);
                    }
                }
            }
        );
        await task;

        _pictureBox.Invalidate();

        _renderButton.Enabled = true;
        _saveButton.Enabled = true;

    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "Temp.png";
        string filePath = Path.Combine(directory, fileName);
        _primaryBuffer.Save(filePath, ImageFormat.Png);
    }

    public ColorRgb8 Illumination(HitRecord hitRecord, int depth)
    {
        IGeometry hitObject = Geometries[hitRecord.HitGuid];

        if (hitObject.ReflectType == ReflectType.Diffuse)
        {
            #region Direct Illumination

            ColorRgb8 directIllumination = ColorRgb8.Black;

            int lightCount = 0;
            foreach (var geometries in Geometries.Values)
            {
                if (geometries is ILight light)
                {
                    lightCount++;

                    for (int i = 0; i < _camera.SamplePerLight; i++)
                    {
                        // shoot random ray from hit-point towards light (shadow ray)
                        LightSample samplePoint = light.RandomSample(hitRecord.HitFrom);
                        Direction toLight = (samplePoint - hitRecord.HitPoint);
                        UnitDirection toLightDirection = toLight.Normalize();
                        Ray shadowRay = new Ray(hitRecord.HitPoint + 0.1d * toLightDirection, toLightDirection);
                        HitRecord shadowRayHitRecord = shadowRay.HitNearest(Geometries);

                        // test if hit-point is in shadow
                        if (samplePoint.Normal * toLightDirection > 0.0d || shadowRayHitRecord.HitGuid == Guid.Empty || Geometries[shadowRayHitRecord.HitGuid] != light)
                        {
                            directIllumination += ColorRgb8.Black;
                        }
                        else
                        {
                            directIllumination += ((-toLightDirection) * samplePoint.Normal) * (toLightDirection * hitRecord.HitNormal) * light.Light * hitObject.Color / Math.PI / (toLight.Length * toLight.Length + 0.1d) / light.SampleProbability;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            directIllumination /= lightCount * _camera.SamplePerLight;

            #endregion

            #region Indirect Illumination

            ColorRgb8 indirectIllumination = ColorRgb8.Black;

            double rr = _random.NextDouble();
            if (rr > RussianRoulette)
            {
                indirectIllumination += ColorRgb8.Black;
            }
            else
            {
                //// random direction on hemisphere
                //double r1 = 2 * Math.PI * _random.NextDouble();
                //double r2 = _random.NextDouble();
                //double r2s = Math.Sqrt(r2);

                //UnitDirection w = hitRecord.HitNormal;
                //UnitDirection u;
                //if (Math.Abs(w.X) > 0.1d)
                //{
                //    u = (UnitDirection.UnitY % w).Normalize();
                //}
                //else
                //{
                //    u = (UnitDirection.UnitX % w).Normalize();
                //}
                //Direction v = w % u;
                //UnitDirection randomDirection = (u * Math.Cos(r1) * r2s + v * Math.Sin(r1) * r2s + w * Math.Sqrt(1 - r2)).Normalize();



                // local axis of hitpoint
                UnitDirection hitPointLocalX = new Direction(0, -hitRecord.HitNormal.Z, hitRecord.HitNormal.Y).Normalize();
                UnitDirection hitPointLocalY = (hitRecord.HitNormal % hitPointLocalX).Normalize();

                // random direction in local coordinate
                double randomNumber1 = _random.NextSingle();
                double randomNumber2 = _random.NextSingle();

                double randomPointLocalX = Math.Sqrt(1.0d - randomNumber1 * randomNumber1) * Math.Cos(2.0d * Math.PI * randomNumber2);
                double randomPointLocalY = Math.Sqrt(1.0d - randomNumber1 * randomNumber1) * Math.Sin(2.0d * Math.PI * randomNumber2);
                double randomPointLocalZ = randomNumber1;

                // convert random direction from local coordinate to world coordinate
                UnitDirection randomDirection = (randomPointLocalX * hitPointLocalX + randomPointLocalY * hitPointLocalY + randomPointLocalZ * hitRecord.HitNormal).Normalize();



                // shoot random ray on hemisphere
                Ray randomRay = new Ray(hitRecord.HitPoint, randomDirection);
                HitRecord newHitRecord = randomRay.HitNearest(Geometries);

                // hit miiss or hit light
                if (newHitRecord.HitAny == false || Geometries[newHitRecord.HitGuid] is ILight)
                {
                    indirectIllumination += ColorRgb8.Black;
                }
                else
                {
                    indirectIllumination += hitObject.Color * Illumination(newHitRecord, depth + 1) * (hitRecord.HitNormal * newHitRecord.HitFrom) / RussianRoulette * (2 * Math.PI) / Math.PI;
                }
            }

            #endregion

            ColorRgb8 illumination = directIllumination + indirectIllumination;

            return illumination;


        }
        else if (hitObject.ReflectType == ReflectType.Specular)
        {
            ColorRgb8 specularIllumination = ColorRgb8.Black;

            double rr = _random.NextDouble();
            if (rr > RussianRoulette)
            {
                specularIllumination += ColorRgb8.Black;
            }
            else
            {
                // reflect direction
                UnitDirection reflectDirection = (hitRecord.HitFrom - 2 * hitRecord.HitNormal * hitRecord.HitFrom * hitRecord.HitNormal).Normalize();
                Ray newRay = new Ray(hitRecord.HitPoint, reflectDirection);
                HitRecord newHitRecord = newRay.HitNearest(Geometries);

                // hit miiss or hit light
                if (newHitRecord.HitAny == false)
                {
                    specularIllumination += ColorRgb8.Black;
                }
                else if (Geometries[newHitRecord.HitGuid] is ILight light)
                {
                    specularIllumination += light.Light * (-newHitRecord.HitFrom * newHitRecord.HitNormal) * hitObject.Color * (-newHitRecord.HitFrom * newHitRecord.HitNormal) / RussianRoulette;
                }
                else
                {
                    specularIllumination += Illumination(newHitRecord, depth + 1) * hitObject.Color / RussianRoulette;
                }
            }

            return specularIllumination;
        }
        else if (hitObject.ReflectType == ReflectType.Refrective)
        {
            // TODO
            throw new NotImplementedException();
        }
        else
        {
            throw new Exception();
        }
    }
}

