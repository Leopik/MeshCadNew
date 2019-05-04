using HelixToolkit.Wpf;
using MeshCAD.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeshCAD.UIModels
{
    public class RodUI : BaseUIElement
    {
        public RodUI(Rod rod) : base(rod)
        {

            var rodVisual = new PipeVisual3D();
            rodVisual.Point1 = rod.Vertices[0].Point.Multiply(SCALE_FACTOR);
            rodVisual.Point2 = rod.Vertices[1].Point.Multiply(SCALE_FACTOR);
            //rodVisual.Diameter = 0.001f;
            VisualElement = rodVisual;
            
            var colorMaterial = MaterialHelper.CreateMaterial(Brushes.Blue);
            var groupMaterial = new MaterialGroup();
            groupMaterial.Children.Add(colorMaterial);
            Material = groupMaterial;
            Title = "Стержень №" + rod.Number;
        }
    }
}
