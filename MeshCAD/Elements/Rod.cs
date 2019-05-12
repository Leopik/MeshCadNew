using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Rod : BaseElement
    {
        public Vertex[] Vertices;
        public Vertex OrientationVertice;

        public Rod(int number, Vertex[] vertices, Vertex orientationVertice, int type)
        {
            Number = number;
            Type = type;
            Vertices = vertices;
            OrientationVertice = orientationVertice;
        }

        public override string ToString()
        {
            return $"Стержень №{Number}\n" +
    $"Первый узел: №{Vertices[0].Number}\n" +
    $"Второй узел: №{Vertices[1].Number}\n" +
    $"Узел ориентации: №{OrientationVertice.Number}\n" +
    //$"Номер стержня: {Number}\n" +
    $"Тип: {Type}\n";
        }
    }
}
