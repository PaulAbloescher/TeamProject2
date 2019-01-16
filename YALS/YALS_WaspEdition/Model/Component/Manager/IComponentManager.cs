using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Component.Manager
{
    public interface IComponentManager
    {
        ICollection<INode> Components { get; }

        void Connect(INode outputNode, INode inputNode, IPin outputPin, IPin inputPin);
        void Disconnect(IPin inputPin);
        void Play();
        void Step();
        void Stop();
    }
}
