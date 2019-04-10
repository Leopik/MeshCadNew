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

namespace MeshCAD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var coor = new NewParser().Parse(@"E:\Dropbox\учебахуеба\диплом\progi\его прога\pros_pl1.dat");

            var hVp3D = new HelixViewport3D();
            var lights = new DefaultLights();
            for (int i = 0; i < coor.GetLength(1); i++)
            {
                var sphere = new SphereVisual3D();
                sphere.Radius = 10 / 10f;
                sphere.Transform = new TranslateTransform3D(coor[0, i], coor[1, i], coor[2, i]);
                hVp3D.Children.Add(sphere);
            }
            hVp3D.Children.Add(lights);
            AddChild(hVp3D);

            //using (StreamReader modelFile = new StreamReader(@"E:\Dropbox\учебахуеба\диплом\progi\его прога\pros_pl1.dat"))
            //    new DatParser().Parse(modelFile);
        }
    }
}
