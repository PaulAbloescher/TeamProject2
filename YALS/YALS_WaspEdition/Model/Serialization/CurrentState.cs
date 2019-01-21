using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.GlobalConfig;
using YALS_WaspEdition.Model.Component.Connection;
using YALS_WaspEdition.ViewModels;

namespace YALS_WaspEdition.Model.Serialization
{
    [Serializable()]
    public class CurrentState
    {

        public CurrentState(GlobalConfigSettings settings, ComponentManagerVM manager)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.ComponentManagerVM = manager ?? throw new ArgumentNullException(nameof(manager));
        }
        //public ICollection<IDisplayableNode> Nodes
        //{
        //    get;
        //    set;
        //}

        //public ICollection<IConnection> Connections
        //{
        //    get;
        //    set;
        //}

        public GlobalConfigSettings Settings
        {
            get;
        }

        public ComponentManagerVM ComponentManagerVM
        {
            get;
        }
    }
}
