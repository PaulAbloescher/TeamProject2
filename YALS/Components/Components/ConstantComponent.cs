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
    public class ConstantComponent : Component
    {
        private Bitmap trueImage;
        private Bitmap falseImage;

        public override void Activate()
        {
            var output = this.Outputs.ElementAt(0);
            var newState = !(bool)output.Value.Current;
            output.Value.Current = newState;

            if (newState)
            {
                this.Picture = this.trueImage;
            }
            else
            {
                this.Picture = this.falseImage;
            }

            this.FirePictureChanged();
        }

        public override void Execute()
        {
            // Nothing happens.
        }

        protected override void Setup()
        {
            this.Type = NodeType.Source;
            this.Label = "Constant";
            var output = new Pin<bool>("Output");
            this.Outputs.Add(output);
            this.LoadImage();
            this.Activate();
        }

        private void LoadImage()
        {
            this.trueImage = Properties.Resources.Constant_True;
            this.falseImage = Properties.Resources.Constant_False;
        }
    }
}
