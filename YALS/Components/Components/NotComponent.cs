// ---------------------------------------------------------------------
// <copyright file="NotComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a logical NOT in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a logical NOT in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class NotComponent : Component
    {
        /// <summary>
        /// Does nothing for the NOT component.
        /// </summary>
        public override void Activate()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Inverts the signal the component got at it's input.
        /// </summary>
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

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Label = "Not";
            this.Description = "Output is the the negated value of the input pin.";
            this.Type = NodeType.Logic;
            var inputPin1 = new Pin<bool>("Input1");
            var outputPin1 = new Pin<bool>("Output1");
            this.Inputs.Add(inputPin1);
            this.Outputs.Add(outputPin1);
            this.LoadImage();
        }

        /// <summary>
        /// Loads the image of the component.
        /// </summary>
        private void LoadImage()
        {
            this.Picture = Properties.Resources.NotGate;
        }
    }
}
