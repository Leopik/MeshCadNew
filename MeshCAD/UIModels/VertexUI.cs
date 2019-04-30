﻿using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static MeshCAD.DarParser;

namespace MeshCAD.UIModels
{
    class VertexUI : BaseUIElement
    {
        public Vertex Vertex;
        public VertexUI(Vertex vertex)
        {
            Vertex = vertex;
            var sphere = new SphereVisual3D();
            sphere.Radius = 1 / 1000f;
            sphere.Model.Material = MaterialHelper.CreateMaterial(Color.FromRgb(255, 0, 0));
            Visual3DModel = sphere.Model;
            
            Transform = new TranslateTransform3D(vertex.Point.X, vertex.Point.Y, vertex.Point.Z);

            Title = "Узел №" + vertex.Number;
        }

        public override string ToString()
        {
            return Vertex.ToString();
        }



        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    MessageBoxResult result = MessageBox.Show($"X {Vertex.Point.X} Y {Vertex.Point.Y} Z {Vertex.Point.Z}",
        //                                  "Confirmation",
        //                                  MessageBoxButton.YesNo,
        //                                  MessageBoxImage.Question);
        //}
    }
}
