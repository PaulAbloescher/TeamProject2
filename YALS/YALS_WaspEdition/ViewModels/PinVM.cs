using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YALS_WaspEdition.Commands;

namespace YALS_WaspEdition.ViewModels
{
    public class PinVM
    {
        public PinVM(IPin pin, ICommand selectedCommand)
        {
            this.Pin = pin ?? throw new ArgumentNullException(nameof(pin));
            this.SelectedCommand = selectedCommand;
        }

        public IPin Pin
        {
            get;
            private set;
        }

        public ICommand SelectedCommand
        {
            get;
        }

        public double Left
        {
            get;
            set;
        }

        public double Top
        {
            get;
            set;
        }
    }
}
