using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YALS_WaspEdition.Commands;

namespace YALS_WaspEdition.ViewModels
{
    [Serializable()]
    public class NodeVM : INotifyPropertyChanged
    {
        private readonly IDisplayableNode node;

        private ICommand inputSelectedCommand;

        private ICommand outputSelectedCommand;

        public NodeVM(IDisplayableNode node, ICommand outputSelectedCommand, ICommand inputSelectedCommand)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.node.PictureChanged += Node_PictureChanged;
            this.inputSelectedCommand = inputSelectedCommand ?? throw new ArgumentNullException(nameof(inputSelectedCommand));
            this.outputSelectedCommand = outputSelectedCommand ?? throw new ArgumentNullException(nameof(outputSelectedCommand));
            this.ActivateCommand = new Command((obj) =>
            {
                this.Node.Activate();
            });
            this.Setup();
        }

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
            get;
        }

        public ICommand RemoveCommand
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
            var inputPinVms = this.node.Inputs.Select(p => new PinVM(p, this.inputSelectedCommand)).ToList();
            var outputPinVms = this.node.Outputs.Select(p => new PinVM(p, this.outputSelectedCommand)).ToList();

            this.Inputs = inputPinVms;
            this.Outputs = outputPinVms;
        }

        private void Node_PictureChanged(object sender, EventArgs e)
        {
            this.FirePropertyChanged(nameof(this.Picture));
        }
    }
}
