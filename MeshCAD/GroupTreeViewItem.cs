using MeshCAD.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    public class GroupTreeViewItem
    {
        public string Title { get; set; }
        public List<BaseUIElement> Elements;

        public GroupTreeViewItem(string title, List<BaseUIElement> elements)
        {
            Title = title;
            Elements = elements;
        }
        public GroupTreeViewItem()
        {

        }
    }
}
