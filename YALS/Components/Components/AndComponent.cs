// ---------------------------------------------------------------------
// <copyright file="AndComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a logical and in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a logical and in a logic simulation.
    /// </summary>
    [Serializable]
    public class AndComponent : Component
    {
        /// <summary>
        /// Does nothing for the and component.
        /// </summary>
        public override void Activate()
        {
           // Nothing to do here
        }

        /// <summary>
        /// Checks if both inputs are true and sets the output to true if that is the case.
        /// </summary>
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

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Label = "And";
            this.Description = "Output is true if all inputs are true.";
            this.Type = NodeType.Logic;
            var inputPin1 = new Pin<bool>("Input1");
            var inputPin2 = new Pin<bool>("Input2");
            var outputPin1 = new Pin<bool>("Output1");
            this.Inputs.Add(inputPin1);
            this.Inputs.Add(inputPin2);
            this.Outputs.Add(outputPin1);
            this.LoadImage();
        }

        /// <summary>
        /// Loads the image for the component.
        /// </summary>
        private void LoadImage()
        {
            this.Picture = Properties.Resources.AndGate;
        }
    }
}
