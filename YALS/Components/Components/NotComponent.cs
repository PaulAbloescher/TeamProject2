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
            this.Setup();
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

        private void Setup()
        {
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
