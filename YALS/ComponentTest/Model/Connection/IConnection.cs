using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ComponentTest.Model.Connection
{
    public interface IConnection
    {
        IPin Output { get;}
        ICollection<IPin> InputPins { get; }

        void AddInputPin(IPin pin);
        void RemoveInputPin(IPin pin);
    }
}
