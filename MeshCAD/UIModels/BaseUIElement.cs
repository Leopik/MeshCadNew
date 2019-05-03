using HelixToolkit.Wpf;
using MeshCAD.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MeshCAD.UIModels
{
    public class BaseUIElement : UIElement3D, INotifyPropertyChanged
    {
        public string Title { get; set; }
        public BaseElement ModelElement { get; protected set; }
        private MeshElement3D visualElement;
        public MeshElement3D VisualElement
        {
            get { return visualElement; }
            protected set { visualElement = value;
                Visual3DModel = value.Model;
            } }

        private Material baseMaterial;
        public Material BaseMaterial
        {
            get { return baseMaterial; }
            protected set
            {
                VisualElement.BackMaterial = value;
                VisualElement.Material = value;
                baseMaterial = value;
            }
        }

        private Material material;
        public Material Material
        {
            get { return material; }
            set
            {
                VisualElement.BackMaterial = value;
                VisualElement.Material = value;
                material = value;
            }
        }

        protected List<string> Colors = new List<string> { "#e6194b", "#3cb44b", "#ffe119", "#4363d8", "#f58231", "#911eb4", "#46f0f0", "#f032e6", "#bcf60c", "#fabebe", "#008080", "#e6beff", "#9a6324", "#fffac8", "#800000", "#aaffc3", "#808000", "#ffd8b1", "#000075", "#808080", "#ffffff", "#000000" };

        //private Material baseBackMaterial;
        //public Material BaseBackMaterial
        //{
        //    get { return baseBackMaterial; }
        //    protected set
        //    {
        //        VisualElement.BackMaterial = value;
        //        baseBackMaterial = value;
        //    }
        //}
        //private Material baseFrontMaterial;
        //public Material BaseFrontMaterial
        //{
        //    get { return baseFrontMaterial; }
        //    protected set
        //    {
        //        VisualElement.Material = value;
        //        baseFrontMaterial = value;
        //    }
        //}

        //helix toolkit looks bad on small sizes, upscaling everything helps
        public const double SCALE_FACTOR = 1000;
        public bool IsShown
        {
            get
            {
                return Visibility == Visibility.Visible;
            }
            set
            {
                Visibility = value ? Visibility.Visible : Visibility.Hidden;
                OnPropertyChanged("IsShown");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}