using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class MassVertex : Vertex
    {
        public double Mass { get; private set; }
        public MassVertex(Vertex vertex, double mass) : base(vertex)
        {
            Mass = mass;
        }

        public override string ToString()
        {
            return base.ToString() + $"Значение массы: {Mass} кг\n";
        }
    }
}
