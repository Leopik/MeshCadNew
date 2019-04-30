using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Triangle : BaseElement
    {
        public int Number, Type, FixPlateNumber, BoardNumber;
        public Vertex[] Vertices;

        public Triangle(int number, Vertex[] vertices, int type, int fixPlateNumber, int boardNumber)
        {
            Number = number;
            Type = type;
            FixPlateNumber = fixPlateNumber;
            BoardNumber = boardNumber;
            Vertices = vertices;
        }
    }
}
