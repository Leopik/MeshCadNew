using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Rectangle : BaseElement
    {
        public int Number;
        public Vertex[] Vertices;
        public int Type;
        public int FixPlateNumber;
        public int BoardNumber;

        public Rectangle(int number, Vertex[] vertices, int type, int fixPlateNumber, int boardNumber)
        {
            Number = number;
            Vertices = vertices;
            Type = type;
            FixPlateNumber = fixPlateNumber;
            BoardNumber = boardNumber;
        }

        public override string ToString()
        {
            return
                $"Первая точка: {Vertices[0].Number}\n" +
                $"Вторая точка: {Vertices[1].Number}\n" +
                $"Третья точка: {Vertices[2].Number}\n" +
                $"Четвертая точка: {Vertices[3].Number}\n" +
                $"Номер: {Number}\n" +
                $"Тип: {Type}\n";
        }
    }
}
