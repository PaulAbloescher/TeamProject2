using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    [Serializable()]
    public class AndComponent : Component
    {
        public AndComponent()
        {
        }

        public override void Activate()
        {
           
        }

        public override void Execute()
        {
            var inputPin1 = this.Inputs.ElementAt(0);
            var inputPin2 = this.Inputs.ElementAt(1);
            var output = this.Outputs.First();
            
            output.Value.Current = false;

            if (inputPin1.Value != null && inputPin2.Value != null)
            {
                var firstValue = (bool)inputPin1.Value.Current;
                var secondValue = (bool)inputPin2.Value.Current;

                if (firstValue && secondValue)
                {
                    output.Value.Current = true;
                }
            }
        }

        protected override void Setup()
        {
            this.Label = "And";
            this.Type = NodeType.Logic;
            var inputPin1 = new Pin<bool>("Input1");
            var inputPin2 = new Pin<bool>("Input2");
            var outputPin1 = new Pin<bool>("Output1");
            this.Inputs.Add(inputPin1);
            this.Inputs.Add(inputPin2);
            this.Outputs.Add(outputPin1);
            this.LoadImage();
        }

        private void LoadImage()
        {
            this.Picture = Properties.Resources.AndGate;
        }
    }
}
