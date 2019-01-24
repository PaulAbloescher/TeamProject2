// ---------------------------------------------------------------------
// <copyright file="XORComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a logical XOR in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a logical XOR in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class XORComponent : Component
    {
        /// <summary>
        /// Does nothing for the XOR component.
        /// </summary>
        public override void Activate()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Checks if the number of inputs that are true is even and sets the output to true if that is the case.
        /// </summary>
        public override void Execute()
        {
            int trueInputs = this.Inputs.Where(i => (bool)i.Value.Current).Count();

            if (trueInputs % 2 != 0)
            {
                this.Outputs.ElementAt(0).Value.Current = true;
            }
            else
            {
                this.Outputs.ElementAt(0).Value.Current = false;
            }
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Label = "Xor";
            this.Description = "Output is true if the number of true values on the input pins is uneven.";
            this.Type = NodeType.Logic;

            this.Inputs.Add(new Pin<bool>("Input1"));
            this.Inputs.Add(new Pin<bool>("Input2"));
            this.Outputs.Add(new Pin<bool>("Output1"));
            this.LoadImage();
        }

        /// <summary>
        /// Loads the image for the component.
        /// </summary>
        private void LoadImage()
        {
            this.Picture = Properties.Resources.XorGate;
        }
    }
}
