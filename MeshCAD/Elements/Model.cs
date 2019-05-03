using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Model
    {
        public Dictionary<int, Vertex> Vertices;
        public Dictionary<int, Triangle> Triangles;
        public Dictionary<int, Rectangle> Rectangles;
        public Dictionary<int, Rod> Rods;

        public Model(Dictionary<int, Vertex> vertices, Dictionary<int, Triangle> triangles, Dictionary<int, Rectangle> rectangles, Dictionary<int, Rod> rods)
        {
            Vertices = vertices;
            Triangles = triangles;
            Rectangles = rectangles;
            Rods = rods;
        }
    }
}
