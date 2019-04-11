using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MeshCAD.DarParser;

namespace MeshCAD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const float EPS = 0.01f;
        double GetAngleABC(Point3D a, Point3D b, Point3D c)
        {
            double[] ab = { b.X - a.X, b.Y - a.Y, b.Z - a.Z };
            double[] bc = { c.X - b.X, c.Y - b.Y, c.Z - b.Z };

            double abVec = Math.Pow(ab[0] * ab[0] + ab[1] * ab[1] + ab[2] * ab[2], 0.5);
            double bcVec = Math.Pow(bc[0] * bc[0] + bc[1] * bc[1] + bc[2] * bc[2], 0.5);

            double[] abNorm = { ab[0] / abVec, ab[1] / abVec, ab[2] / abVec };
            double[] bcNorm = { bc[0] / bcVec, bc[1] / bcVec, bc[2] / bcVec };

            double res = abNorm[0] * bcNorm[0] + abNorm[1] * bcNorm[1] + abNorm[2] * bcNorm[2];

            return Math.Acos(res) * 180.0 / Math.PI;
        }


        public MainWindow()
        {
            InitializeComponent();

            DarParser.Model model;
            using (StreamReader modelFile = new StreamReader(@"E:\Downloads\Chrome\pros_plat.dar"))
                model = new DarParser().Parse(modelFile);

            var hVp3D = new HelixViewport3D();
            var lights = new DefaultLights();
            foreach(var point in model.Vertices)
            {
                var sphere = new SphereVisual3D();
                sphere.Radius = 1 / 500f;
                sphere.Transform = new TranslateTransform3D(point.X, point.Y, point.Z);
                hVp3D.Children.Add(sphere);
            }

            foreach (var rectangle in model.Rectangles)
            {
                var rect = new RectangleVisual3D();
                rect.Origin = rectangle.Vertices[0].ToPoint3D();
                double angle = GetAngleABC(rectangle.Vertices[0].ToPoint3D(), rectangle.Vertices[1].ToPoint3D(), rectangle.Vertices[2].ToPoint3D());
                if ((90 - EPS<angle) && (angle<90 + EPS))
                {
                    //Vertice[0] and Vertice[2] - diagonal
                    rect.LengthDirection = new Vector3D(rectangle.Vertices[0].X - rectangle.Vertices[1].X,
                        rectangle.Vertices[0].Y - rectangle.Vertices[1].Y,
                        rectangle.Vertices[0].Z - rectangle.Vertices[1].Z);

                    rect.Normal = new Vector3D(rectangle.Vertices[0].X - rectangle.Vertices[3].X,
                        rectangle.Vertices[0].Y - rectangle.Vertices[3].Y,
                        rectangle.Vertices[0].Z - rectangle.Vertices[3].Z);

                    rect.Width = rectangle.Vertices[0].ToPoint3D().DistanceTo(rectangle.Vertices[3].ToPoint3D());
                    rect.Length = rectangle.Vertices[0].ToPoint3D().DistanceTo(rectangle.Vertices[1].ToPoint3D());
                } else
                {
                    //Vertice[1] and Vertice[2] - diagonal
                    rect.LengthDirection = new Vector3D(rectangle.Vertices[0].X - rectangle.Vertices[1].X,
    rectangle.Vertices[0].Y - rectangle.Vertices[1].Y,
    rectangle.Vertices[0].Z - rectangle.Vertices[1].Z);

                    rect.Normal = new Vector3D(rectangle.Vertices[0].X - rectangle.Vertices[2].X,
                        rectangle.Vertices[0].Y - rectangle.Vertices[2].Y,
                        rectangle.Vertices[0].Z - rectangle.Vertices[2].Z);

                    rect.Width = rectangle.Vertices[0].ToPoint3D().DistanceTo(rectangle.Vertices[2].ToPoint3D());
                    rect.Length = rectangle.Vertices[0].ToPoint3D().DistanceTo(rectangle.Vertices[1].ToPoint3D());
                }
                hVp3D.Children.Add(rect);
            }

            hVp3D.Children.Add(lights);
            AddChild(hVp3D);

            //var coor = new NewParser().Parse(@"E:\Dropbox\учебахуеба\диплом\progi\его прога\pros_pl1.dat");

            //var hVp3D = new HelixViewport3D();
            //var lights = new DefaultLights();
            //for (int i = 0; i < coor.GetLength(1); i++)
            //{
            //    var sphere = new SphereVisual3D();
            //    sphere.Radius = 10 / 10f;
            //    sphere.Transform = new TranslateTransform3D(coor[0, i], coor[1, i], coor[2, i]);
            //    hVp3D.Children.Add(sphere);
            //}
            //hVp3D.Children.Add(lights);
            //AddChild(hVp3D);

            //using (StreamReader modelFile = new StreamReader(@"E:\Dropbox\учебахуеба\диплом\progi\его прога\pros_pl1.dat"))
            //    new DatParser().Parse(modelFile);
        }
    }
}
