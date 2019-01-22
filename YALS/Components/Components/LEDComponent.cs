using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    [Serializable()]
    public class LEDComponent : Component
    {
        private Bitmap trueImage;
        private Bitmap falseImage;

        public override void Activate()
        {
            // Nothing to do here.
        }

        public override void Execute()
        {
            var input = this.Inputs.First();
            var state = (bool)input.Value.Current;

            if (state)
            {
                this.Picture = this.trueImage;
            }
            else
            {
                this.Picture = this.falseImage;
            }

            this.FirePictureChanged();
        }

        protected override void Setup()
        {
            this.Type = NodeType.Display;
            this.Label = "LED";
            var input = new Pin<bool>("Input");
            this.Inputs.Add(input);
            this.LoadImage();
            this.Picture = this.falseImage;
        }

        private void LoadImage()
        {
            this.trueImage = Properties.Resources.Led_Green;
            this.falseImage = Properties.Resources.Led_Red;
        }
    }
}
