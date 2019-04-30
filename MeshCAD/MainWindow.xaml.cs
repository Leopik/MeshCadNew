using HelixToolkit.Wpf;
using MeshCAD.UIModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Path = System.IO.Path;

namespace MeshCAD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public const float EPS = 0.01f;
        private double GetAngleABC(Point3D a, Point3D b, Point3D c)
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

            this.DataContext = this;
            DarParser.Model model;
            using (StreamReader modelFile = new StreamReader(@"pros_plat.dar"))
                model = new DarParser().Parse(modelFile);
            DrawModel(model);

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

        private void DrawModel(Model model)
        {
            foreach (var point in model.Vertices)
            {
                var vertex = new VertexUI(point);
                vertex.MouseDown += new MouseButtonEventHandler((obj, args) => { if (args.ChangedButton == MouseButton.Left) ShowElementControls(vertex); });
                ViewPort.Children.Add(vertex);
                VertexTree.Items.Add(vertex);
            }
            foreach (var rectangle in model.Rectangles)
            {
                var rectUI = new RectangleUI(rectangle);

                rectUI.MouseDown += new MouseButtonEventHandler((obj, args) => { if (args.ChangedButton == MouseButton.Left) ShowElementControls(rectUI); });
                ViewPort.Children.Add(rectUI);
                RectangleTree.Items.Add(rectUI);
            }
        }
        private BaseUIElement currentChosenElement;
        public BaseUIElement CurrentChosenElement {
            get { return currentChosenElement; }
            set
            {
                currentChosenElement = value;
                OnPropertyChanged("CurrentChosenElement");
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


        private ClickMode currentSelectMode = ClickMode.SelectMode;
        public ClickMode CurrentSelectMode
        {
            get { return currentSelectMode; }
            set { currentSelectMode = value;
                OnPropertyChanged("CurrentSelectMode");
            }
        }

        private void ShowElementControls(BaseUIElement element)
        {
            //if (element == null)
            //    return;
            CurrentChosenElement = element;
            if (currentSelectMode == ClickMode.HideMode)
                CurrentChosenElement.IsShown = false;
            //InfoBlock.Text = element.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "DAR Files (*.dar)|*.dar|DAT Files (*.dat)|*.dat|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                OpenFile(filename);

            }
        }

        private void OpenFile(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            try
            {
                if (fileExtension == ".dar")
                {
                    Model model;
                    using (StreamReader modelFile = new StreamReader(@"pros_plat.dar"))
                        model = new DarParser().Parse(modelFile);

                }
                else if (fileExtension == ".dat")
                {
                    
                } else
                {
                    throw new Exception("Неправильный формат файла");
                }
            } catch ( Exception e)
            {
                MessageBoxResult result = MessageBox.Show( e.Message,
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);

            }
        }

        private void StructureElementClick(object sender, RoutedEventArgs e)
        {
            ShowElementControls(((Button)sender).DataContext as BaseUIElement);
        }

        private void HighlightElementClick(object sender, RoutedEventArgs e)
        {
            if (CurrentChosenElement == null)
                return;
            ViewPort.LookAt(CurrentChosenElement.FindBounds(CurrentChosenElement.Transform).Location, 0.4, 100);
        }
    }
    public enum ClickMode
    {
        HideMode,
        SelectMode
    }
}
