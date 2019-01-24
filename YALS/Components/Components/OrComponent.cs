// ---------------------------------------------------------------------
// <copyright file="OrComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a logical OR in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a logical OR in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class OrComponent : Component
    {
        /// <summary>
        /// Does nothing for the OR component.
        /// </summary>
        public override void Activate()
        {
           // Nothing to do here.
        }

        /// <summary>
        /// Checks if one of the inputs is true and sets the output to true if that is the case.
        /// </summary>
        public override void Execute()
        {
            var inputPin1 = this.Inputs.ElementAt(0);
            var inputPin2 = this.Inputs.ElementAt(1);
            var output = this.Outputs.First();

            if (inputPin1.Value != null && inputPin2.Value != null)
            {
                var firstValue = (bool)inputPin1.Value.Current;
                var secondValue = (bool)inputPin2.Value.Current;

                if (firstValue || secondValue)
                {
                    output.Value.Current = true;
                    return;
                }
            }
            
            output.Value.Current = false;
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Label = "Or";
            this.Description = "Output is true if one of the input pins is true.";
            this.Type = NodeType.Logic;
            var inputPin1 = new Pin<bool>("OrInput1");
            var inputPin2 = new Pin<bool>("OrInput2");
            var outputPin1 = new Pin<bool>("OrOutput1");

            this.Inputs.Add(inputPin1);
            this.Inputs.Add(inputPin2);
            this.Outputs.Add(outputPin1);

            this.LoadImage();
        }

        /// <summary>
        /// Loads the image of the component.
        /// </summary>
        private void LoadImage()
        {
            this.Picture = Properties.Resources.OrGate;
        }
    }
}