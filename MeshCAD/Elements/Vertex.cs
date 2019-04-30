using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MeshCAD.Elements
{
    public class Vertex : BaseElement
    {
        public Point3D Point { get; private set; }
        public int Type, Belongs;
        public int Number;
        public bool IsControl = false;

        public Vertex(Vertex vertex)
        {
            Point = vertex.Point;
            Type = vertex.Type;
            Belongs = vertex.Belongs;
            Number = vertex.Number;
            IsControl = vertex.IsControl;
        }

        public Vertex(int number, double x, double y, double z, int type, int belongs)
        {
            Number = number;
            Point = new Point3D(x, y, z);
            Type = type;
            Belongs = belongs;
        }

        public override string ToString()
        {
            return $"Координаты:\n" +
                $"X: {Point.X}\n" +
                $"Y: {Point.Y}\n" +
                $"Z: {Point.Z}\n" +
                $"Номер: {Number}\n" +
                $"Тип: {Type}\n" +
                $"Принадлежность узла: {Belongs}\n";
        }
    }
}
