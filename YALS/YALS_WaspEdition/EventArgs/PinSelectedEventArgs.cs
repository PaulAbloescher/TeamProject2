using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.ViewModels;

namespace YALS_WaspEdition.MyEventArgs
{
    public class PinSelectedEventArgs : EventArgs
    {
        public PinSelectedEventArgs(PinVM pin)
        {
            this.SelectedPin = pin ?? throw new ArgumentNullException(nameof(pin));
        }

        public PinVM SelectedPin
        {
            get;
            private set;
        }
    }
}
