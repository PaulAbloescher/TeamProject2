using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.DI;
using YALS_WaspEdition.Model.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using YALS_WaspEdition.Commands;
using System.IO;
using YALS_WaspEdition.Model.Serialization;
using System.Windows.Input;
using YALS_WaspEdition.MyEventArgs;

namespace YALS_WaspEdition.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        private PinVM currentSelectedInput;

        private PinVM currentSelectedOutput;


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
                this.Manager.Clear();
            });
        }

        public event EventHandler OnSaveFileRequested;
        public event EventHandler OnOpenFileRequested;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<NotificationEventArgs> NotificationRequested;

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

        public void LoadState(string path, ICommand inputSelected, ICommand outputSelected)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            this.Manager.Clear();

            ICurrentStateSerializer serializer = Provider.Container.GetService<ICurrentStateSerializer>();
            CurrentState state = serializer.Deserialize(path);
            List<NodeVM> nodes = new List<NodeVM>();
            this.Manager = new ComponentManagerVM();

            foreach (var node in state.NodeVMsWithoutCommands)
            {
                node.SetSelectedCommandForPins(inputSelected, outputSelected);
                this.Manager.AddNode(node);
            }

            foreach(var conn in state.ConnectedPins)
            {
                this.Manager.Connect(conn.Item2, conn.Item1);
            }

            this.FirePropertyChanged(nameof(this.Manager));
        }

        public void Save(string path)
        {
            CurrentState state = new CurrentState(new GlobalConfig.GlobalConfigSettings(), this.Manager.Connections.Select(connection => new Tuple<PinVM, PinVM>(connection.InputPin, connection.OutputPin)).ToList(), this.Manager.NodeVMs);

            ICurrentStateSerializer serializer = Provider.Container.GetService<ICurrentStateSerializer>();
            serializer.Serialize(path, state);

        }

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


        public ComponentManagerVM Manager
        {
            get;
            private set;
        }

        public IDictionary<NodeType, ICollection<NodeVM>> AvailableComponents
        {
            get;
            set;
        }

        public ICommand OpenFileCommand
        {
            get;
            private set;
        }

        public ICommand SaveFileCommand
        {
            get;
            private set;
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }

        protected virtual void FireOnSaveFileRequested()
        {
            this.OnSaveFileRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnOpenFileRequested()
        {
            this.OnOpenFileRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireNotificationRequested(NotificationEventArgs args)
        {
            this.NotificationRequested?.Invoke(this, args);
        }

        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Setup()
        {
            this.LoadComponents();
        }

        private void LoadComponents()
        {
            var loader = Provider.Container.GetService<IComponentLoaderController>();
            var result = loader.Load("Plugins");
            this.AvailableComponents = this.CreateNodeVmDictionary(result);
            this.FirePropertyChanged(nameof(this.AvailableComponents));
        }

        private IDictionary<NodeType, ICollection<NodeVM>> CreateNodeVmDictionary(IDictionary<NodeType, ICollection<IDisplayableNode>> source)
        {
            var dictionary = new Dictionary<NodeType, ICollection<NodeVM>>();

            foreach (var kvp in source)
            {
                dictionary.Add(kvp.Key, kvp.Value.Select(c => new NodeVM(c, new Command(obj => { }), new Command(obj => { }))).ToList());
            }

            return dictionary;
        }

        private bool CheckIfNewConnectionIsAvailable()
        {
            return this.CurrentSelectedInput != null && this.CurrentSelectedOutput != null;
        }

        private void AddConnection(PinVM outputPin, PinVM inputPin)
        {
            try
            {
                this.Manager.Connect(outputPin, inputPin);
            }
            catch (InvalidOperationException ex)
            {
                this.FireNotificationRequested(new NotificationEventArgs(ex.Message, "Pin type match error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error));
            }
            catch (Exception)
            {
                this.FireNotificationRequested(new NotificationEventArgs("An unknown error occurred.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error));
            }
            finally
            {
                this.CurrentSelectedInput = null;
                this.CurrentSelectedOutput = null;
            }
        }
    }
}
