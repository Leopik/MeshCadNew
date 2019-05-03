using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD.Configs
{
    public class BoardConfigList : ConfigurationElementCollection
    {
        public BoardConfig this[int index]
        {
            get
            {
                return base.BaseGet(index) as BoardConfig;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new BoardConfig this[string responseString]
        {
            get { return (BoardConfig)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new BoardConfig();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((BoardConfig)element);
        }
    }

}
