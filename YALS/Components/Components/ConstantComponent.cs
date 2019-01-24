// ---------------------------------------------------------------------
// <copyright file="ConstantComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a source in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a source in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class ConstantComponent : Component
    {
        /// <summary>
        /// The image when the component emits true.
        /// </summary>
        private Bitmap trueImage;

        /// <summary>
        /// The image when the component emits false.
        /// </summary>
        private Bitmap falseImage;

        /// <summary>
        /// Changes the value that is emitted by the component.
        /// </summary>
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

        /// <summary>
        /// Does nothing for the Constant component.
        /// </summary>
        public override void Execute()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Type = NodeType.Source;
            this.Label = "Constant";
            var output = new Pin<bool>("Output");
            this.Outputs.Add(output);
            this.LoadImage();
            this.Activate();
        }

        /// <summary>
        /// Loads the image of the component.
        /// </summary>
        private void LoadImage()
        {
            this.trueImage = Properties.Resources.Constant_True;
            this.falseImage = Properties.Resources.Constant_False;
        }
    }
}
