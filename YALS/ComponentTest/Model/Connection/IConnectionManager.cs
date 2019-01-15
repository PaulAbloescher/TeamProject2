using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ComponentTest.Model.Connection
{
    public interface IConnectionManager
    {
        ICollection<IConnection> Connections { get; }

        void Connect(IPin output, IPin input);

        void Disconnect(IPin pin);
    }
}
