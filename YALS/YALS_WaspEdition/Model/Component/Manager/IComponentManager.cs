using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.Model.Component.Connection;

namespace YALS_WaspEdition.Model.Component.Manager
{
    public interface IComponentManager
    {
        ICollection<INode> Components { get; }
        ICollection<IConnection> Connections { get; }
        event EventHandler StepFinished;
        bool IsRunning { get; }
        void Connect(IPin outputPin, IPin inputPin);
        void Disconnect(IPin output, IPin inputPin);
        void AddNode(INode node);
        void RemoveNode(INode node);
        void Play();
        void Step();
        void Stop();
        Task PlayAsync();
        Task StepAsync();
        Task StopAsync();
    }
}
