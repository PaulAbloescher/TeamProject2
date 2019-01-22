using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    public class SwitchComponent : Component
    {
        private Bitmap switchOn;
        private Bitmap switchOff;

        private bool on;

        public override void Activate()
        {
            this.on = !this.on;

            this.Picture = this.on ? this.switchOn : this.switchOff;

            this.FirePictureChanged();
        }

        public override void Execute()
        {
            var input = this.Inputs.First();
            var output = this.Outputs.First();

            if (this.on)
            {
                output.Value.Current = input.Value.Current;
            }
            else
            {
                output.Value.Current = false;
            }
        }

        protected override void Setup()
        {
            this.Type = NodeType.Switch;
            this.Label = "Switch";
            var input = new Pin<bool>("Input");
            var output = new Pin<bool>("Output");
            this.Inputs.Add(input);
            this.Outputs.Add(output);
            this.LoadImage();
            this.on = false;
            this.Picture = this.switchOff;
        }

        private void LoadImage()
        {
            this.switchOn = Properties.Resources.switch_on;
            this.switchOff = Properties.Resources.switch_off;
        }
    }
}
