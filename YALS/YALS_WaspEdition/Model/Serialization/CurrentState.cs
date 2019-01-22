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

        public CurrentState(GlobalConfigSettings settings, ICollection<Tuple<PinVM, PinVM>> connectedPins, ICollection<NodeVM> nodeVMs)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.ConnectedPins = connectedPins;
            this.NodeVMsWithoutCommands = nodeVMs;
        }
        
        public ICollection<Tuple<NodeVM, NodeVM>> ConnectedNodes
        {
            get;
        }

        public ICollection<Tuple<PinVM, PinVM>> ConnectedPins
        {
            get;
        }

        public ICollection<NodeVM> NodeVMsWithoutCommands
        {
            get;
        }
        

        public GlobalConfigSettings Settings
        {
            get;
        }
    }
}
