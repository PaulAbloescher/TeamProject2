// ---------------------------------------------------------------------
// <copyright file="SwitchComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a switch in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a switch in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class SwitchComponent : Component
    {
        /// <summary>
        /// The image for when the switch is turned on.
        /// </summary>
        private Bitmap switchOn;

        /// <summary>
        /// The switch offThe image for when the switch is turned off.
        /// </summary>
        private Bitmap switchOff;

        /// <summary>
        /// Is the switch turned on.
        /// </summary>
        private bool on;

        /// <summary>
        /// Changes the state of the switch.
        /// </summary>
        public override void Activate()
        {
            this.on = !this.on;

            this.Picture = this.on ? this.switchOn : this.switchOff;

            this.FirePictureChanged();
        }

        /// <summary>
        /// Determines if the switch further transmits the signal it received.
        /// </summary>
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

        /// <summary>
        /// Sets up this instance.
        /// </summary>
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

        /// <summary>
        /// Loads the image of the component.
        /// </summary>
        private void LoadImage()
        {
            this.switchOn = Properties.Resources.switch_on;
            this.switchOff = Properties.Resources.switch_off;
        }
    }
}
