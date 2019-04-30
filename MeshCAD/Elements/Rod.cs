using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Rod : BaseElement
    {
        public int Number, Type;
        public Vertex[] Vertices;
        public Vertex OrientationVertice;

        public Rod(int number, Vertex[] vertices, Vertex orientationVertice, int type)
        {
            Number = number;
            Type = type;
            Vertices = vertices;
            OrientationVertice = orientationVertice;
        }
    }
}
