using HelixToolkit.Wpf;
using MeshCAD.Elements;
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
    public class VertexUI : BaseUIElement
    {
        private Material DefaultVertexMaterial = MaterialHelper.CreateMaterial(Brushes.Red);
        private Material BindVertexMaterial = MaterialHelper.CreateMaterial(Brushes.Yellow);
        private Material ControlVertexMaterial = MaterialHelper.CreateMaterial(Brushes.Green);
        private Material MassVertexMaterial = MaterialHelper.CreateMaterial(Brushes.Violet);

        public Vertex Vertex;
        public VertexUI(Vertex vertex)
        {
            ModelElement = vertex;
            Vertex = vertex;
            var sphere = new SphereVisual3D();

            sphere.Model.Material = MaterialHelper.CreateMaterial(Color.FromRgb(255, 0, 0));
            sphere.Center = vertex.Point.Multiply(SCALE_FACTOR);

            VisualElement = sphere;

            switch(vertex.Type)
            {
                case 0:
                    BaseMaterial = DefaultVertexMaterial;
                    break;
                case 1:
                    BaseMaterial = BindVertexMaterial;
                    break;
                case 2:
                    BaseMaterial = ControlVertexMaterial;
                    break;
                case 3:
                    BaseMaterial = MassVertexMaterial;
                    break;
            }

            Title = "Узел №" + vertex.Number;
        }
    }
}
