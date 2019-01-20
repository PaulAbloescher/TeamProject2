using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YALS_WaspEdition.Commands;

namespace YALS_WaspEdition.ViewModels
{
    public class ConnectionVM
    {
        public ConnectionVM(PinVM outputPin, PinVM inputPin, ICommand disconnectCommand)
        {
            this.OutputPin = outputPin ?? throw new ArgumentNullException(nameof(outputPin));
            this.InputPin = inputPin ?? throw new ArgumentNullException(nameof(inputPin));
            this.DisconnectCommand = disconnectCommand ?? throw new ArgumentNullException(nameof(disconnectCommand));
        }

        public PinVM OutputPin
        {
            get;
        }

        public PinVM InputPin
        {
            get;
        }

        public string ConnectionAsString
        {
            get
            {
                return $"Output: {this.OutputPin.Pin.Label} -> Input: {this.InputPin.Pin.Label}";
            }
        }

        public ICommand DisconnectCommand
        {
            get;
        }
    }
}
