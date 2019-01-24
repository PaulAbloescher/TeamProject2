// <copyright file="ComponentManagerVM.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the Component Manager View Model.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Microsoft.Extensions.DependencyInjection;
    using YALS_WaspEdition.Commands;
    using YALS_WaspEdition.DI;
    using YALS_WaspEdition.GlobalConfig;
    using YALS_WaspEdition.Model.Component.Manager;

    /// <summary>
    /// Represents the Component Manager View Model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class ComponentManagerVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The color getter.
        /// </summary>
        private IGetColorForPin colorGetter;

        /// <summary>
        /// The settings.
        /// </summary>
        private GlobalConfigSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManagerVM"/> class.
        /// </summary>
        public ComponentManagerVM()
        {
            this.Settings = GlobalConfigSettings.GetDefaultSettings();

            // TODO add more losely coupled config settings           
            this.Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManagerVM"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ComponentManagerVM(GlobalConfigSettings settings)
        {
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.Setup();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the play command.
        /// </summary>
        /// <value>
        /// The play command.
        /// </value>
        public ICommand PlayCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the pause command.
        /// </summary>
        /// <value>
        /// The pause command.
        /// </value>
        public ICommand PauseCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the step command.
        /// </summary>
        /// <value>
        /// The step command.
        /// </value>
        public ICommand StepCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get
            {
                return this.Manager.IsRunning;
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public GlobalConfigSettings Settings
        {
            get
            {
                return this.settings;
            }

            set
            {
                this.settings = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public IComponentManager Manager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public ObservableCollection<ConnectionVM> Connections
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the node view models.
        /// </summary>
        /// <value>
        /// The node view models.
        /// </value>
        public ICollection<NodeVM> NodeVMs
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds the node asynchronous.
        /// </summary>
        /// <param name="node">Represents the node.</param>
        /// <returns>The corresponding task.</returns>
        public async Task AddNodeAsync(NodeVM node)
        {
            await this.Manager.StopAsync();
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

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="node">Represents the node.</param>
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

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            var nodes = this.NodeVMs.ToList();

            foreach (var node in nodes)
            {
                node.RemoveCommand.Execute(null);
            }
        }

        /// <summary>
        /// Connects the specified output pin.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        public void Connect(PinVM outputPin, PinVM inputPin)
        {
                this.Manager.Connect(outputPin.Pin, inputPin.Pin);
                this.Connections.Add(new ConnectionVM(outputPin, inputPin, new Command((obj) => { this.Disconnect(outputPin, inputPin); })));
        }

        /// <summary>
        /// Disconnects the specified output pin.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        public void Disconnect(PinVM outputPin, PinVM inputPin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.OutputPin.Equals(outputPin) && c.InputPin.Equals(inputPin));
            this.Manager.Disconnect(outputPin.Pin, inputPin.Pin);
            this.Connections.Remove(connection);
        }

        /// <summary>
        /// Fires the on property changed.
        /// </summary>
        /// <param name="name">Represents the name.</param>
        protected virtual void FireOnPropertyChanged([CallerMemberName]string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Managers the step finished.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ManagerStepFinished(object sender, EventArgs e)
        {
            foreach (var nodeVM in this.NodeVMs)
            {
                foreach (var inputPin in nodeVM.Inputs)
                {
                    inputPin.ApplyColorRules(this.colorGetter);
                }

                foreach (var outputPin in nodeVM.Outputs)
                {
                    outputPin.ApplyColorRules(this.colorGetter);
                }
            }
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        private void Setup()
        {
            this.Manager = Provider.Container.GetService<IComponentManager>();
            this.Connections = new ObservableCollection<ConnectionVM>();
            this.NodeVMs = new List<NodeVM>();

            this.colorGetter = new GetColorWithGlobalConfigSettings(this.Settings);
            this.Manager.StepFinished += this.ManagerStepFinished;
            this.PlayCommand = new Command((obj) => 
            {
                App.Current.Dispatcher.Invoke(async () => 
                {
                    await this.Manager.PlayAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
            this.PauseCommand = new Command((obj) => 
            {
                App.Current.Dispatcher.Invoke(async () => 
                {
                    await this.Manager.StopAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
            this.StepCommand = new Command((obj) => 
            {
                App.Current.Dispatcher.Invoke(async () => 
                {
                    await this.Manager.StepAsync();
                    this.FireOnPropertyChanged(nameof(this.IsRunning));
                });
            });
        }
    }
}
