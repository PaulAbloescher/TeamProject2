using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    [Serializable()]
    public class XORComponent : Component
    {
        public XORComponent()
        {
        }

        public override void Activate()
        {
        }

        public override void Execute()
        {
            int trueInputs = this.Inputs.Where(i => (bool)i.Value.Current).Count();

            if (trueInputs % 2 != 0)
            {
                this.Outputs.ElementAt(0).Value.Current = true;
            }
            else
            {
                this.Outputs.ElementAt(0).Value.Current = false;
            }

        }

        protected override void Setup()
        {
            this.Label = "Xor";
            this.Description = "Output is true if the number of true values on the input pins is uneven.";
            this.Type = NodeType.Logic;

            this.Inputs.Add(new Pin<bool>("Input1"));
            this.Inputs.Add(new Pin<bool>("Input2"));
            this.Outputs.Add(new Pin<bool>("Output1"));
            this.LoadImage();
        }

        private void LoadImage()
        {
            this.Picture = Properties.Resources.XorGate;
        }
    }
}
