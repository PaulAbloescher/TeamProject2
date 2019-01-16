using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    public class XORComponent : Component
    {
        public XORComponent()
        {
        }

        public override void Execute()
        {
            var notNullInputs = this.Inputs.Where(pin => pin.Value != null);
            var falseInputs = notNullInputs.Where(pin => (bool)pin.Value.Current == false);


            int trueInputs = notNullInputs.Count() - falseInputs.Count();

            if (trueInputs % 2 == 0)
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
            this.Type = NodeType.Logic;

            this.Inputs.Add(new Pin<bool>("Input1"));
            this.Inputs.Add(new Pin<bool>("Input2"));
            this.Outputs.Add(new Pin<bool>("Output1"));
        }
    }
}
