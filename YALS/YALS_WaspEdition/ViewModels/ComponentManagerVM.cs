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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using YALS_WaspEdition.GlobalConfig;

namespace YALS_WaspEdition.ViewModels
{
    [Serializable()]
    public class ComponentManagerVM : INotifyPropertyChanged
    {
        private IGetColorForPin colorGetter;

        public ComponentManagerVM()
        {
            this.Manager = Provider.Container.GetService<IComponentManager>();
            this.Connections = new ObservableCollection<ConnectionVM>();
            this.NodeVMs = new List<NodeVM>();
            this.Settings = GlobalConfigSettings.GetDefaultSettings();
            // TODO add more losely coupled config settings
            this.colorGetter = new GetColorWithGlobalConfigSettings(this.Settings);
            this.Manager.StepFinished += ManagerStepFinished;
            this.PlayCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(async () => {
                    await this.Manager.PlayAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
            this.PauseCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(async () => {
                    await this.Manager.StopAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
            this.StepCommand = new Command((obj) => {
                App.Current.Dispatcher.Invoke(async () => {
                    await this.Manager.StepAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
        }

        private void ManagerStepFinished(object sender, EventArgs e)
        {
            foreach(var nodeVM in this.NodeVMs)
            {
                foreach(var inputPin in nodeVM.Inputs)
                {
                    inputPin.ApplyColorRules(this.colorGetter);
                }

                foreach(var outputPin in nodeVM.Outputs)
                {
                    outputPin.ApplyColorRules(this.colorGetter);
                }
            }
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

        public bool IsRunning
        {
            get
            {
                return this.Manager.IsRunning;
            }
        }
        public GlobalConfigSettings Settings
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddNode(NodeVM node)
        {
            this.Manager.AddNode(node.Node);
            this.NodeVMs.Add(node);

            foreach (var inputPin in node.Inputs)
            {
                inputPin.ApplyColorRules(this.colorGetter);
            }

            foreach (var outputPin in node.Outputs)
            {
                outputPin.ApplyColorRules(this.colorGetter);
            }
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
                this.Manager.Connect(outputPin.Pin, inputPin.Pin);
                this.Connections.Add(new ConnectionVM(outputPin, inputPin, new Command((obj) =>
                {
                    this.Disconnect(outputPin, inputPin);
                })));
        }

        public void Disconnect(PinVM outputPin, PinVM inputPin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.OutputPin.Equals(outputPin) && c.InputPin.Equals(inputPin));
            this.Manager.Disconnect(outputPin.Pin, inputPin.Pin);
            this.Connections.Remove(connection);
        }

        protected virtual void FireOnPropertyChanged([CallerMemberName]string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
