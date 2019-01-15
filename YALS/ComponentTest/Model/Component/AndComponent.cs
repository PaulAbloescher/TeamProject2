using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Model.Component
{
    public class AndComponent : Component
    {
        public AndComponent()
        {
            this.Setup();
        }

        public override void Execute()
        {
            var inputPin1 = this.Inputs.ElementAt(0);
            var inputPin2 = this.Inputs.ElementAt(1);
            var output = this.Outputs.First();
            output.Value.Value = false;

            if (inputPin1.Value != null && inputPin2.Value != null)
            {
                var firstValue = (bool)inputPin1.Value.Value;
                var secondValue = (bool)inputPin2.Value.Value;

                if (firstValue && secondValue)
                {
                    output.Value.Value = true;
                }
            }
        }

        private void Setup()
        {
            var inputPin1 = new Pin<bool>("Input1");
            var inputPin2 = new Pin<bool>("Input2");
            var outputPin1 = new Pin<bool>("Output1");
            this.Inputs.Add(inputPin1);
            this.Inputs.Add(inputPin2);
            this.Outputs.Add(outputPin1);
        }
    }
}
