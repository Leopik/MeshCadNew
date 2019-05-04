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
        public BaseElement ModelElement { get; private set; }
        private MeshElement3D visualElement;
        public MeshElement3D VisualElement
        {
            get { return visualElement; }
            protected set { visualElement = value;
                Visual3DModel = value.Model;
                visualElement.Material = material;
                visualElement.BackMaterial = material;
            } }

        private MaterialGroup material = new MaterialGroup();
        public MaterialGroup Material
        {
            get { return material; }
            set
            {
                VisualElement.BackMaterial = value;
                VisualElement.Material = value;
                material = value;
            }
        }

        public BaseUIElement(BaseElement baseElement)
        {
            ModelElement = baseElement;
        }

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