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
using System.Windows.Input;

namespace YALS_WaspEdition.ViewModels
{
    [Serializable()]
    public class ComponentManagerVM
    {
        public ComponentManagerVM()
        {
            this.Manager = Provider.Container.GetService<IComponentManager>();
            this.Connections = new ObservableCollection<ConnectionVM>();
            this.NodeVMs = new List<NodeVM>();
            this.PlayCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(() => {
                    var task = this.Manager.PlayAsync();
                });
            });
            this.PauseCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(() => {
                    var task = this.Manager.StopAsync();
                });
            });
            this.StepCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(() => {
                    var task = this.Manager.StepAsync();
                });
            });
        }

        public ICommand PlayCommand
        {
            get;
        }

        public ICommand PauseCommand
        {
            get;
        }

        public ICommand StepCommand
        {
            get;
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

        public ICollection<NodeVM> NodeVMs
        {
            get;
        }

        public void AddNode(NodeVM node)
        {
            this.Manager.AddNode(node.Node);
            this.NodeVMs.Add(node);
        }

        public void RemoveNode(NodeVM node)
        {
            var affectedConnections = this.Connections.Where(c => node.Inputs.Contains(c.InputPin) || node.Outputs.Contains(c.OutputPin)).ToList();
            foreach (var connection in affectedConnections)
            {
                this.Manager.Disconnect(connection.OutputPin.Pin, connection.InputPin.Pin);
                this.Connections.Remove(connection);
            }

            this.Manager.RemoveNode(node.Node);
            this.NodeVMs.Remove(node);
        }

        public void Clear()
        {
            for(int i = 0; i < this.NodeVMs.Count; i++)
            {
                this.NodeVMs.ElementAt(i).RemoveCommand.Execute(null);
            }
        }

        public void Connect(PinVM outputPin, PinVM inputPin)
        {
            try
            {


                this.Manager.Connect(outputPin.Pin, inputPin.Pin);
                this.Connections.Add(new ConnectionVM(outputPin, inputPin, new Command((obj) =>
                {
                    this.Disconnect(outputPin, inputPin);
                })));
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidOperationException("An input pin cannot be connected twice.", ex);
            }
        }

        public void Disconnect(PinVM outputPin, PinVM inputPin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.OutputPin.Equals(outputPin) && c.InputPin.Equals(inputPin));
            this.Manager.Disconnect(outputPin.Pin, inputPin.Pin);
            this.Connections.Remove(connection);
        }
    }
}
