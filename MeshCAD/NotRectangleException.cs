using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    public class NotRectangleException : Exception
    {
        public NotRectangleException(string exc) : base(exc) { }
    }
}
