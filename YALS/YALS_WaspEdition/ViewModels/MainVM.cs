// ---------------------------------------------------------------------
// <copyright file="MainVM.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a logical AND in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------
namespace YALS_WaspEdition.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Extensions.DependencyInjection;
    using Shared;
    using YALS_WaspEdition.Commands;
    using YALS_WaspEdition.DI;
    using YALS_WaspEdition.Model.Reflection;
    using YALS_WaspEdition.Model.Serialization;
    using YALS_WaspEdition.MyEventArgs;

    /// <summary>
    /// Implements the Main ViewModel for the application. It handles connections between components and also the tree view on the left.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Backing field for the <see cref="CurrentSelectedInput"/> property.
        /// </summary>
        private PinVM currentSelectedInput;

        /// <summary>
        /// Backing field for the <see cref="CurrentSelectedOutput"/> property.
        /// </summary>
        private PinVM currentSelectedOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        public MainVM()
        {
            this.Setup();
            this.Manager = new ComponentManagerVM();

            this.SaveFileCommand = new Command(obj =>
            {
                this.FireOnSaveFileRequested();
            });

            this.OpenFileCommand = new Command(obj =>
            {
                this.FireOnOpenFileRequested();
            });

            this.ClearCommand = new Command(obj =>
            {
                this.FireOnNewRequested();
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when a property has changed values.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when the user wants to create a new workspace.
        /// </summary>
        public event EventHandler OnNewRequested;

        /// <summary>
        /// Called when the user wants to save a workspace.
        /// </summary>
        public event EventHandler OnSaveFileRequested;

        /// <summary>
        /// Called when the user wants to open an existing workspace.
        /// </summary>
        public event EventHandler OnOpenFileRequested;

        /// <summary>
        /// Called when a notification has to be shown.
        /// </summary>
        public event EventHandler<NotificationEventArgs> NotificationRequested;

        /// <summary>
        /// Gets or sets the currently selected input pin of a component.
        /// </summary>
        /// <value>PinVM instance representing the selected input pin.</value>
        public PinVM CurrentSelectedInput
        {
            get
            {
                return this.currentSelectedInput;
            }

            set
            {
                this.currentSelectedInput = value;

                if (this.CheckIfNewConnectionIsAvailable())
                {
                    this.AddConnection(this.CurrentSelectedOutput, this.CurrentSelectedInput);
                }
            }
        }

        /// <summary>
        /// Gets or sets the currently selected output pin of a component.
        /// </summary>
        /// <value>A PinVM instance.</value>
        public PinVM CurrentSelectedOutput
        {
            get
            {
                return this.currentSelectedOutput;
            }

            set
            {
                this.currentSelectedOutput = value;

                if (this.CheckIfNewConnectionIsAvailable())
                {
                    this.AddConnection(this.CurrentSelectedOutput, this.CurrentSelectedInput);
                }
            }
        }

        /// <summary>
        /// Gets which of two pins is currently selected.
        /// </summary>
        /// <value>A PinVM instance.</value>
        public PinVM FirstSelectedPin
        {
            get
            {
                if (this.CurrentSelectedOutput != null)
                {
                    return this.CurrentSelectedOutput;
                }

                if (this.CurrentSelectedInput != null)
                {
                    return this.CurrentSelectedInput;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the path of the last save action.
        /// </summary>
        /// <value>String to last saved path.</value>
        public string LastSavedPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ComponentManager instance.
        /// </summary>
        /// <value>A ComponentManager instance.</value>
        public ComponentManagerVM Manager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the available components.
        /// </summary>
        /// <value>A Dictionary with NodeTypes as keys and Lists of Components as values.</value>
        public IDictionary<NodeType, ICollection<NodeVM>> AvailableComponents
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the ICommand used when "save file" is selected in the menu bar.
        /// </summary>
        /// <value>Called when the user selects "open file" in the menu bar.</value>
        public ICommand OpenFileCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ICommand used when "save file" is selected in the menu bar.
        /// </summary>
        /// <value>Called when the user selects "save file" in the menu bar.</value>
        public ICommand SaveFileCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ICommand used when "new" is selected in the menu bar.
        /// </summary>
        /// <value>Called when the user selects "new" in the menu bar.</value>
        public ICommand ClearCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ICommand used when "reload plugins" is selected.
        /// </summary>
        /// <value>The ICommand that's used when the "reload plugins" UI action is called.</value>
        public ICommand ReloadPluginsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads application state from a file in an async manner.
        /// </summary>
        /// <param name="path">The path to load data from.</param>
        /// <param name="inputSelected">The input ICommand that gets loaded into newly instanced Nodes.</param>
        /// <param name="outputSelected">The output ICommand that gets loaded into newly instanced Nodes.</param>
        /// <returns>An async Task instance.</returns>
        public async Task LoadStateAsync(string path, ICommand inputSelected, ICommand outputSelected)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            this.Manager.Clear();

            ICurrentStateSerializer serializer = Provider.Container.GetService<ICurrentStateSerializer>();
            CurrentState state = serializer.Deserialize(path);
            List<NodeVM> nodes = new List<NodeVM>();
            this.Manager = new ComponentManagerVM(state.Settings);

            foreach (var node in state.NodeVMsWithoutCommands)
            {
                node.SetSelectedCommandForPins(inputSelected, outputSelected);
                await this.Manager.AddNodeAsync(node);
            }

            foreach (var (inputPin, outputPin) in state.ConnectedPins)
            {
                this.Manager.Connect(outputPin, inputPin);
            }

            this.LastSavedPath = path;
            this.FirePropertyChanged(nameof(this.Manager));
        }

        /// <summary>
        /// Saves the current workspace to disk.
        /// </summary>
        /// <param name="path">The location to save to.</param>
        public void Save(string path)
        {
            CurrentState state = new CurrentState(
                this.Manager.Settings, 
                this.Manager.Connections.Select(connection => new Tuple<PinVM, PinVM>(connection.InputPin, connection.OutputPin)).ToList(), 
                this.Manager.NodeVMs);

            ICurrentStateSerializer serializer = Provider.Container.GetService<ICurrentStateSerializer>();
            serializer.Serialize(path, state);
            this.LastSavedPath = path;
        }

        /// <summary>
        /// Fires an OnNewRequested Event.
        /// </summary>
        protected virtual void FireOnNewRequested()
        {
            this.OnNewRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires an OnSaveFileRequested event.
        /// </summary>
        protected virtual void FireOnSaveFileRequested()
        {
            this.OnSaveFileRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires an OnOPenFileRequested event.
        /// </summary>
        protected virtual void FireOnOpenFileRequested()
        {
            this.OnOpenFileRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires a NotificationRequested event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        protected virtual void FireNotificationRequested(NotificationEventArgs args)
        {
            this.NotificationRequested?.Invoke(this, args);
        }

        /// <summary>
        /// Fires a PropertyChanged event.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Procedure to setup the MainVM object.
        /// </summary>
        private void Setup()
        {
            this.LoadComponents();
            this.ReloadPluginsCommand = new Command(obj => 
            {
                this.LoadComponents();
            });
        }

        /// <summary>
        /// Attempts to load all plugins from the "Plugins" subfolder.
        /// </summary>
        private void LoadComponents()
        {
            var loader = Provider.Container.GetService<IComponentLoaderController>();
            var result = loader.Load("Plugins");
            this.AvailableComponents = this.CreateNodeVmDictionary(result);
            this.FirePropertyChanged(nameof(this.AvailableComponents));
        }

        /// <summary>
        /// Creates the dictionary used when display available components.
        /// </summary>
        /// <param name="source">The original component dictionary.</param>
        /// <returns>The transformed dictionary.</returns>
        private IDictionary<NodeType, ICollection<NodeVM>> CreateNodeVmDictionary(IDictionary<NodeType, ICollection<IDisplayableNode>> source)
        {
            var dictionary = new Dictionary<NodeType, ICollection<NodeVM>>();

            foreach (var kvp in source)
            {
                dictionary.Add(kvp.Key, kvp.Value.Select(c => new NodeVM(c, new Command(obj => { }), new Command(obj => { }))).ToList());
            }

            return dictionary;
        }

        /// <summary>
        /// Checks if the selected pins are actually possible to connect.
        /// </summary>
        /// <returns>True if both pins are ready to be connected.</returns>
        private bool CheckIfNewConnectionIsAvailable()
        {
            return this.CurrentSelectedInput != null && this.CurrentSelectedOutput != null;
        }

        /// <summary>
        /// Creates a new connection between two pins.
        /// </summary>
        /// <param name="outputPin">The selected output pin.</param>
        /// <param name="inputPin">The selected input pin.</param>
        private void AddConnection(PinVM outputPin, PinVM inputPin)
        {
            try
            {
                this.Manager.Connect(outputPin, inputPin);
            }
            catch (InvalidOperationException ex)
            {
                this.FireNotificationRequested(
                    new NotificationEventArgs(ex.Message, "Pin type match error", MessageBoxButton.OK, MessageBoxImage.Error));
            }
            catch (Exception)
            {
                this.FireNotificationRequested(
                    new NotificationEventArgs("An unknown error occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error));
            }
            finally
            {
                this.CurrentSelectedInput = null;
                this.CurrentSelectedOutput = null;
            }
        }
    }
}
