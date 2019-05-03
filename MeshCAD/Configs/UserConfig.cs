using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeshCAD.Configs
{
    public class UserConfig : ConfigurationSection
    {
 
        [ConfigurationProperty("BoardConfigList")]
        [ConfigurationCollection(typeof(BoardConfigList), AddItemName = "BoardConfig")]
        public BoardConfigList Companies
        {
            get
            {
                object o = this["BoardConfigList"];
                return o as BoardConfigList;
            }
        }
    }
}
