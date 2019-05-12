using HelixToolkit.Wpf;
using MeshCAD.Configs;
using MeshCAD.Elements;
using MeshCAD.Properties;
using MeshCAD.UIModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ModelUI ModelUI;
        private Material SelectMaterial = MaterialHelper.CreateImageMaterial(ToBitmapImage(Properties.Resources.SelectMaterial), 1);

        private BaseUIElement currentChosenElement;
        public BaseUIElement CurrentChosenElement
        {
            get { return currentChosenElement; }
            set
            {
                currentChosenElement = value;
                OnPropertyChanged("CurrentChosenElement");
            }
        }

        private ClickMode currentSelectMode = ClickMode.SelectMode;
        public ClickMode CurrentSelectMode
        {
            get { return currentSelectMode; }
            set
            {
                currentSelectMode = value;
                OnPropertyChanged("CurrentSelectMode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            try
            {
                //Model model;
                //using (StreamReader modelFile = new StreamReader(@"pros_plat.dar"))
                //    model = new DarParser().Parse(modelFile);
                //DrawModel(model);
            }catch (Exception e)
            {

            }
        }
        
        private void DrawModel(Model model)
        {
            ModelUI = new ModelUI(model,
                new MouseButtonEventHandler((obj, args) =>
                {
                    if (args.ChangedButton == MouseButton.Left) ShowElementControls((BaseUIElement)obj);
                }));

            VertexTree.UIElements.Clear();

            foreach (var ver in ModelUI.verticesUI.Values)
                VertexTree.UIElements.Add(ver);
            RectangleTree.UIElements.Clear();
            
            foreach (var rec in ModelUI.rectanglesUI.Values)
                RectangleTree.UIElements.Add(rec);
            RodTree.UIElements.Clear();
            
            foreach (var rod in ModelUI.rodsUI.Values)
                RodTree.UIElements.Add(rod);
            TriangleTree.UIElements.Clear();
            
            foreach (var tri in ModelUI.trianglesUI.Values)
                TriangleTree.UIElements.Add(tri);
            //VertexTree.UIElements = ModelUI.verticesUI.Values.Select(x => (BaseUIElement)x).ToList();
            //RectangleTree.UIElements = ModelUI.rectanglesUI.Values.Select(x => (BaseUIElement)x).ToList();
            //RodTree.UIElements = ModelUI.rodsUI.Values.Select(x => (BaseUIElement)x).ToList();
            //TriangleTree.UIElements = ModelUI.trianglesUI.Values.Select(x => (BaseUIElement)x).ToList();
            LonelyTree.UIElements.Clear();
            
            foreach (var lonely in ModelUI.lonelyElementsUI)
                LonelyTree.UIElements.Add(lonely);
            foreach (var vertex in ModelUI.verticesUI)
            {
                ViewPort.Children.Add(vertex.Value);
            }
            foreach (var rectangle in ModelUI.rectanglesUI)
            {
                ViewPort.Children.Add(rectangle.Value);
            }
            foreach (var rod in ModelUI.rodsUI)
            {
                ViewPort.Children.Add(rod.Value);
            }
            foreach (var triangle in ModelUI.trianglesUI)
            {
                ViewPort.Children.Add(triangle.Value);
            }
            foreach (var plate in ModelUI.platesUI)
            {
                var plateGroup = new GroupTreeViewItem()
                {
                    Title = "Пластина №" + plate.Key,
                    UIElements = new System.Collections.ObjectModel.ObservableCollection<BaseUIElement>(plate.Value.Select(x => (BaseUIElement)x))
                };
                StructureTree.Items.Add(plateGroup);
            }
            foreach (var board in ModelUI.boardsUI)
            {
                var boardGroup = new GroupTreeViewItem()
                {
                    Title = "Плата №" + board.Key,
                    UIElements = new System.Collections.ObjectModel.ObservableCollection<BaseUIElement>(board.Value.Select(x => (BaseUIElement)x))
                };
                StructureTree.Items.Add(boardGroup);
            }
            TriangleTree.IsShown = true;
            VertexTree.IsShown = true;
            RectangleTree.IsShown = true;
            LonelyTree.IsShown = true;
            RodTree.IsShown = true;
            ElementTypeComboBox.ItemsSource = StructureTree.Items;

        }

        public void OnPropertyChanged(string prop = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        private void ShowElementControls(BaseUIElement element)
        {

            if (CurrentChosenElement != null)
            {
                CurrentChosenElement.Material.Children.Remove(SelectMaterial);
            }

            CurrentChosenElement = element;

            switch (currentSelectMode)
            {
                case ClickMode.HideMode:
                    CurrentChosenElement.IsShown = false;
                    break;
                case ClickMode.SelectMode:
                    CurrentChosenElement.Material.Children.Add(SelectMaterial);
                    break;
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "DAR Files (*.dar)|*.dar|DAT Files (*.dat)|*.dat|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

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
                    using (StreamReader modelFile = new StreamReader(fileName))
                        model = new DarParser().Parse(modelFile);
                    DrawModel(model);

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
            
            ViewPort.LookAt(CurrentChosenElement.FindBounds(CurrentChosenElement.Transform).Location, 100, 1);
        }

        //allow only numbers into search box
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }
 
        private void SearchNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void SearchNumberTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
    public enum ClickMode
    {
        HideMode,
        SelectMode
    }
}
