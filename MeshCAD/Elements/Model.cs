using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class Model
    {
        public List<Vertex> Vertices;
        public List<Triangle> Triangles;
        public List<Rectangle> Rectangles;
        public List<Rod> Rods;

        public Model(List<Vertex> vertices, List<Triangle> triangles, List<Rectangle> rectangles, List<Rod> rods)
        {
            Vertices = vertices;
            Triangles = triangles;
            Rectangles = rectangles;
            Rods = rods;
        }
    }
}
