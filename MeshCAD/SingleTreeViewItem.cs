using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    public class SingleTreeViewItem : GroupTreeViewItem
    {
        public new bool? IsShown
        {
            get
            {
                if (UIElements.All(x => x.IsShown))
                    return true;
                if (UIElements.Any(x => x.IsShown))
                    return null;
                return false;
            }
            set
            {
                foreach (var uiElement in UIElements)
                    uiElement.IsShown = (value ?? false);
                //UIElements.Select(x => x.IsShown = (value ?? false));
                OnPropertyChanged("IsShown");
            }
        }
    }
}
