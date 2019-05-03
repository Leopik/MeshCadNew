using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Elements
{
    public class BelongingElement : BaseElement
    {
        public int FixPlateNumber;
        public int BoardNumber;
        public bool IsLonely { get { return BoardNumber == 0 && FixPlateNumber == 0; } }
    }
}
