using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.GlobalConfig;
using YALS_WaspEdition.Model.Component.Connection;

namespace YALS_WaspEdition.Model.Serialization
{
    [Serializable()]
    public class CurrentState
    {
        public ICollection<IDisplayableNode> Nodes
        {
            get;
            set;
        }

        public ICollection<IConnection> Connections
        {
            get;
            set;
        }

        public GlobalConfigSettings Settings
        {
            get;
            set;
        }
    }
}
