using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.ViewModels
{
    public class PinVM
    {
        public PinVM(IPin pin)
        {
            this.Pin = pin ?? throw new ArgumentNullException(nameof(pin));
        }

        public IPin Pin
        {
            get;
            private set;
        }

        public int Left
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }
    }
}
