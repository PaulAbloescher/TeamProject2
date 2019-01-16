using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace YALS_WaspEdition.Model.Component.Connection
{
    public class Connection : IConnection
    {
        public Connection(IPin output)
        {
            this.InputPins = new List<IPin>();
            this.Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public IPin Output
        {
            get;
            set;
        }

        public ICollection<IPin> InputPins
        {
            get;
            set;
        }

        public void AddInputPin(IPin pin)
        {
            pin.Value = this.Output.Value;
            this.InputPins.Add(pin);
        }

        public void RemoveInputPin(IPin pin)
        {
            if (this.InputPins.Remove(pin))
            {
                pin.Value = null;
            }
        }
    }
}
