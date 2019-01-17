using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    public class NotComponent : Component
    {
        public NotComponent()
        {
        }

        public override void Execute()
        {
            var inputPin = this.Inputs.First();
            var output = this.Outputs.First();

            output.Value.Current = false;

            if (inputPin.Value != null)
            {
                var firstValue = (bool)inputPin.Value.Current;
                
                output.Value.Current = !firstValue;
            }
        }

        protected override void Setup()
        {
            this.Label = "Not";
            this.Description = "Output is the the negated value of the input pin.";
            this.Type = NodeType.Logic;
            var inputPin1 = new Pin<bool>("Input1");
            var outputPin1 = new Pin<bool>("Output1");
            this.Inputs.Add(inputPin1);
            this.Outputs.Add(outputPin1);
            this.LoadImage();
        }

        private void LoadImage()
        {
            this.Picture = Properties.Resources.NotGate;
        }
    }
}
