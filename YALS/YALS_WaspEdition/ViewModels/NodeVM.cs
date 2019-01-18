using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.ViewModels
{
    public class NodeVM
    {
        private readonly IDisplayableNode node;

        public NodeVM(IDisplayableNode node)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
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

        public int Left
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }

        private void Setup()
        {
            var inputPinVms = this.node.Inputs.Select(p => new PinVM(p)).ToList();
            var outputPinVms = this.node.Outputs.Select(p => new PinVM(p)).ToList();

            this.Inputs = inputPinVms;
            this.Outputs = outputPinVms;
        }
    }
}
