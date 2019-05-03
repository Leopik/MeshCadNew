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
        public int Belongs;
        public bool IsControl = false;
        public bool IsAnchor = false;

        public Vertex(Vertex vertex)
        {
            Point = vertex.Point;
            Type = vertex.Type;
            Belongs = vertex.Belongs;
            Number = vertex.Number;
            IsControl = vertex.IsControl;
            IsAnchor = vertex.IsAnchor;
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
            string typeStr, belongStr;
            switch (Type) {
                case 1:
                    typeStr = $"Узел крепления (тип №{Type})";
                    break;
                case 2:
                    typeStr = $"Контрольный узел (тип №{Type})";
                    break;
                case 3:
                    typeStr = $"Узел с сосредоточенной массой (тип №{Type})";
                    break;
                case 0:
                    typeStr = $"Обычный узел (тип №{Type})";
                    break;
                default:
                    typeStr = $"Неизвестное значение (тип №{Type})";
                    break;
            }

            switch (Belongs)
            {
                case 1:
                    belongStr = $"Узел принадлежит плате или пластине с автоматическим разбиением (№{Belongs})";
                    break;
                case 2:
                    belongStr = $"Узел принадлежит пластине с фиксированным шагом сетки (№{Belongs})";
                    break;
                case 3:
                    belongStr = $"Дополнительный узел модели (№{Belongs})";
                    break;
                case 4:
                    belongStr = $"Дополнительный узел ориентации стержня (№{Belongs})";
                    break;
                default:
                    belongStr = $"Неизвестное значение (№{Belongs})";
                    break;
            }

            return $"Координаты:\n" +
                $"X: {Point.X}\n" +
                $"Y: {Point.Y}\n" +
                $"Z: {Point.Z}\n" +
                $"Номер: {Number}\n" +
                $"Тип: {typeStr}\n" +
                $"Принадлежность узла: {belongStr}\n";
        }
    }
}
