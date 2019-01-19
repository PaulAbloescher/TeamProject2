using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.DI;
using Microsoft.Extensions.DependencyInjection;
using YALS_WaspEdition.Model.Component.Manager;
using System.Collections.ObjectModel;
using YALS_WaspEdition.Commands;

namespace YALS_WaspEdition.ViewModels
{
    public class ComponentManagerVM
    {
        public ComponentManagerVM()
        {
            this.Manager = Provider.Container.GetService<IComponentManager>();
            this.Connections = new ObservableCollection<ConnectionVM>();
        }

        public IComponentManager Manager
        {
            get;
            private set;
        }

        public ObservableCollection<ConnectionVM> Connections
        {
            get;
        }

        public void Connect(PinVM outputPin, PinVM inputPin)
        {
            this.Manager.Connect(outputPin.Pin, inputPin.Pin);
            this.Connections.Add(new ConnectionVM(outputPin, inputPin, new Command((obj) => {
                this.Disconnect(outputPin, inputPin);
            })));
        }

        public void Disconnect(PinVM outputPin, PinVM inputPin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.OutputPin.Equals(outputPin) && c.InputPin.Equals(inputPin));
            this.Manager.Disconnect(outputPin.Pin, inputPin.Pin);
            this.Connections.Remove(connection);
        }
    }
}
