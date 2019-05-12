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
    public class BelongingUIElement : BaseUIElement
    {
        public static Dictionary<int, Material> BoardMaterials = new Dictionary<int, Material>();
        public BelongingElement BelongingElement { get; private set; }
        private List<string> Colors = new List<string> {  "#3cb44b", "#ffe119", "#4363d8", "#f58231", "#911eb4", "#46f0f0", "#f032e6", "#bcf60c", "#fabebe", "#008080", "#e6beff", "#9a6324", "#fffac8", "#800000", "#aaffc3", "#808000", "#ffd8b1", "#000075", "#808080", "#ffffff", "#000000" };

        public BelongingUIElement(BelongingElement belongingElement) :base(belongingElement)
        {
            BelongingElement = belongingElement;
            var key = belongingElement.BoardNumber == 0 ? belongingElement.FixPlateNumber * 2 : (belongingElement.BoardNumber * 2 - 1);
            if (!BoardMaterials.ContainsKey(key))
                BoardMaterials[key] = MaterialHelper.CreateMaterial((Color)ColorConverter.ConvertFromString(Colors[key]));
            Material.Children.Add(BoardMaterials[key]);
        }
    }
}
