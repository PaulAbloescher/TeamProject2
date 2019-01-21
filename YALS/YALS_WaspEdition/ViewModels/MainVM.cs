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
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public void LoadState(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }


        }

        public void Save(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            CurrentState state = new CurrentState(new GlobalConfig.GlobalConfigSettings(), this.Manager);

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
        }

        public IDictionary<NodeType, ICollection<NodeVM>> AvailableComponents
        {
            get;
            set;
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
            this.Manager.Connect(outputPin, inputPin);
            this.CurrentSelectedInput = null;
            this.CurrentSelectedOutput = null;
        }
    }
}
