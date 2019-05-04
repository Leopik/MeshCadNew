﻿using MeshCAD.UIModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    public class GroupTreeViewItem : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public List<BaseUIElement> UIElements { get; set; }

        public bool? IsShown
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
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        public GroupTreeViewItem()
        {

        }
    }
}
