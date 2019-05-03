using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Rectangle : BelongingElement
    {
        public Vertex[] Vertices;

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
                $"Первый узел: №{Vertices[0].Number}\n" +
                $"Второй узел: №{Vertices[1].Number}\n" +
                $"Третий узел: №{Vertices[2].Number}\n" +
                $"Четвертый узел: №{Vertices[3].Number}\n" +
                $"Номер прямоугольника: {Number}\n" +
                $"Тип: {Type}\n" +
                (IsLonely ? "Отдельный прямоугольник\n" : "") +
                $"№ пластины с фиксированным шагом сетки: {FixPlateNumber}\n" +
                $"№ платы или пластины с равномерным шагом сетки: {BoardNumber}\n";
        }
    }
}
