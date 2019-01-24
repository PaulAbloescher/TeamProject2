// ---------------------------------------------------------------------
// <copyright file="LEDComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a LED in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a LED in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class LEDComponent : Component
    {
        /// <summary>
        /// The image if the LED receives a true value.
        /// </summary>
        private Bitmap trueImage;

        /// <summary>
        /// The image if the LED receives a false value.
        /// </summary>
        private Bitmap falseImage;

        /// <summary>
        /// Does nothing for the LED component.
        /// </summary>
        public override void Activate()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Checks the input of the LED and sets the corresponding image.
        /// </summary>
        public override void Execute()
        {
            var input = this.Inputs.First();

            bool state = false;

            if (input.Value != null)
            {
                state = (bool)input.Value.Current;
            }

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

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Type = NodeType.Display;
            this.Label = "LED";
            this.Description = "Emits green light if input is true. Otherwise emits red light.";
            var input = new Pin<bool>("Input");
            this.Inputs.Add(input);
            this.LoadImage();
            this.Picture = this.falseImage;
        }

        /// <summary>
        /// Loads the image of the component.
        /// </summary>
        private void LoadImage()
        {
            this.trueImage = Properties.Resources.Led_Green;
            this.falseImage = Properties.Resources.Led_Red;
        }
    }
}
