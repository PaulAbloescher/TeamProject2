using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YALS_WaspEdition.Commands;

namespace YALS_WaspEdition.ViewModels
{
    [Serializable()]
    public class NodeVM : INotifyPropertyChanged, ISerializable
    {
        private readonly IDisplayableNode node;

        [NonSerialized()]
        private ICommand inputSelectedCommand;

        [NonSerialized()]
        private ICommand outputSelectedCommand;

        [NonSerialized()]
        private ICommand removeCommand;

        public NodeVM(IDisplayableNode node, ICommand outputSelectedCommand, ICommand inputSelectedCommand)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.node.PictureChanged += Node_PictureChanged;
            this.inputSelectedCommand = inputSelectedCommand ?? throw new ArgumentNullException(nameof(inputSelectedCommand));
            this.outputSelectedCommand = outputSelectedCommand ?? throw new ArgumentNullException(nameof(outputSelectedCommand)); 
            this.Setup();
        }

        public NodeVM(SerializationInfo info, StreamingContext context)
        {
            this.node = (IDisplayableNode)info.GetValue("node", typeof(IDisplayableNode));
            this.Left = (double)info.GetValue("left", typeof(double));
            this.Top = (double)info.GetValue("top", typeof(double));
            this.Inputs = (ICollection<PinVM>)info.GetValue("inputs", typeof(ICollection<PinVM>));
            this.Outputs= (ICollection<PinVM>)info.GetValue("outputs", typeof(ICollection<PinVM>));
        }


        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;


        public IDisplayableNode Node
        {
            get
            {
                return this.node;
            }
        }

        public Bitmap Picture
        {
            get
            {
                return this.node.Picture;
            }
        }

        public ICollection<PinVM> Inputs
        {
            get;
            private set;
        }

        public ICollection<PinVM> Outputs
        {
            get;
            private set;
        }

        public string Label
        {
            get
            {
                return this.node.Label;
            }
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

        public ICommand ActivateCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    this.Node.Activate();
                });
            }
        }

        public ICommand InputSelectedCommand
        {
            get
            {
                return this.inputSelectedCommand;
            }
            private set
            {
                this.inputSelectedCommand = value ?? throw new ArgumentNullException();
            }
        }

        public ICommand OutputSelectedCommand
        {
            get
            {
                return this.outputSelectedCommand;
            }
            private set
            {
                this.outputSelectedCommand = value ?? throw new ArgumentNullException();
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return this.removeCommand;
            }
            set
            {
                this.removeCommand = value ?? throw new ArgumentNullException();
            }
        }

        public string Description
        {
            get
            {
                return this.Node.Description;
            }
        }

        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Setup()
        {
            var inputPinVms = this.node.Inputs.Select(p => new PinVM(p, this.inputSelectedCommand)).ToList();
            var outputPinVms = this.node.Outputs.Select(p => new PinVM(p, this.outputSelectedCommand)).ToList();

            this.Inputs = inputPinVms;
            this.Outputs = outputPinVms;
        }

        private void Node_PictureChanged(object sender, EventArgs e)
        {
            this.FirePropertyChanged(nameof(this.Picture));
        }

        public void SetSelectedCommandForPins(ICommand inputSelected, ICommand outputSelected)
        {
            this.inputSelectedCommand = inputSelected ?? throw new ArgumentNullException(nameof(inputSelected));
            this.outputSelectedCommand = outputSelected ?? throw new ArgumentNullException(nameof(outputSelected));

            foreach(var pin in this.Inputs)
            {
                pin.SelectedCommand = inputSelected;
            }

            foreach(var pin in this.Outputs)
            {
                pin.SelectedCommand = outputSelected;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("node", this.Node);
            info.AddValue("left", this.Left);
            info.AddValue("top", this.Top);
            info.AddValue("inputs", this.Inputs);
            info.AddValue("outputs", this.Outputs);
        }
    }
}
