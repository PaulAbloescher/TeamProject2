using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YALS_WaspEdition.ViewModels
{
    public class NodeVM
    {
        private readonly IDisplayableNode node;

        private ICommand inputSelectedCommand;

        private ICommand outputSelectedCommand;

        public NodeVM(IDisplayableNode node, ICommand outputSelectedCommand, ICommand inputSelectedCommand)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.inputSelectedCommand = inputSelectedCommand ?? throw new ArgumentNullException(nameof(inputSelectedCommand));
            this.outputSelectedCommand = outputSelectedCommand ?? throw new ArgumentNullException(nameof(outputSelectedCommand));
            this.Setup();
        }

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

        public ICommand RemoveCommand
        {
            get;
            set;
        }

        private void Setup()
        {
            var inputPinVms = this.node.Inputs.Select(p => new PinVM(p, this.inputSelectedCommand)).ToList();
            var outputPinVms = this.node.Outputs.Select(p => new PinVM(p, this.outputSelectedCommand)).ToList();

            this.Inputs = inputPinVms;
            this.Outputs = outputPinVms;
        }
    }
}
