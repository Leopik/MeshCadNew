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
        private Material UnknownVertexMaterial = MaterialHelper.CreateMaterial(Brushes.Gainsboro);

        public Vertex Vertex;
        public VertexUI(Vertex vertex) : base(vertex)
        {
            Vertex = vertex;
            var sphere = new SphereVisual3D();

            sphere.Model.Material = MaterialHelper.CreateMaterial(Color.FromRgb(255, 0, 0));
            sphere.Center = vertex.Point.Multiply(SCALE_FACTOR);
            sphere.Radius = sphere.Radius * 0.5;

            VisualElement = sphere;

            Material colorMaterial;
            switch(vertex.Type)
            {
                case 0:
                    colorMaterial = DefaultVertexMaterial;
                    break;
                case 1:
                    colorMaterial = BindVertexMaterial;
                    break;
                case 2:
                    colorMaterial = ControlVertexMaterial;
                    break;
                case 3:
                    colorMaterial = MassVertexMaterial;
                    break;
                default:
                    colorMaterial = UnknownVertexMaterial;
                    break;

            }

            
            var groupMaterial = new MaterialGroup();
            groupMaterial.Children.Add(colorMaterial);
            Material = groupMaterial;

            Title = "Узел №" + vertex.Number;
        }
    }
}
