using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Triangle : BelongingElement
    {
        public Vertex[] Vertices;

        public Triangle(int number, Vertex[] vertices, int type, int fixPlateNumber, int boardNumber)
        {
            Number = number;
            Type = type;
            FixPlateNumber = fixPlateNumber;
            BoardNumber = boardNumber;
            Vertices = vertices;
        }

        public override string ToString()
        {
            return
    $"Первый узел: №{Vertices[0].Number}\n" +
    $"Второй узел: №{Vertices[1].Number}\n" +
    $"Третий узел: №{Vertices[2].Number}\n" +
    $"Номер треугольника: {Number}\n" +
    $"Тип: {Type}\n" +
    (IsLonely ? "Отдельный треугольник\n" : "") +
    $"№ пластины с фиксированным шагом сетки: {FixPlateNumber}\n" +
    $"№ платы или пластины с равномерным шагом сетки: {BoardNumber}\n";
        }
    }
}
