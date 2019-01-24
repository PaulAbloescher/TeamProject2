// ---------------------------------------------------------------------
// <copyright file="BoolToIntComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a bool to int converter in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Collections;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a boolean to integer converter in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class BoolToIntComponent : Component
    {
        /// <summary>
        /// Does nothing for this component.
        /// </summary>
        public override void Activate()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Checks the input pins and sets the output to the corresponding value.
        /// </summary>
        public override void Execute()
        { 
            var bitArray = new BitArray(this.GetInputAsBoolArray());
            var intValue = this.BitArrayToInt(bitArray);
            this.Outputs.ElementAt(0).Value.Current = intValue;
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Type = NodeType.Converter;
            this.Description = "Converts 8 bool inputs to a number from 0 - 255.";

            var firstPin = new Pin<bool>("Input1");
            var secondPin = new Pin<bool>("Input2");
            var thirdPin = new Pin<bool>("Input3");
            var fourthPin = new Pin<bool>("Input4");
            var fifthPin = new Pin<bool>("Input5");
            var sixthPin = new Pin<bool>("Input6");
            var seventhPin = new Pin<bool>("Input7");
            var eightPin = new Pin<bool>("Input8");
 
            this.Inputs.Add(firstPin);
            this.Inputs.Add(secondPin);
            this.Inputs.Add(thirdPin);
            this.Inputs.Add(fourthPin);
            this.Inputs.Add(fifthPin);
            this.Inputs.Add(sixthPin);
            this.Inputs.Add(seventhPin);
            this.Inputs.Add(eightPin);

            var output = new Pin<int>("Output");
            this.Outputs.Add(output);
            this.Picture = Properties.Resources.boolToIntConverter;
        }

        /// <summary>
        /// Puts the input values in an array.
        /// </summary>
        /// <returns>The array containing the input values.</returns>
        private bool[] GetInputAsBoolArray()
        {
            var boolArray = new bool[8];
            boolArray[0] = (bool)this.Inputs.ElementAt(0).Value.Current;
            boolArray[1] = (bool)this.Inputs.ElementAt(1).Value.Current;
            boolArray[2] = (bool)this.Inputs.ElementAt(2).Value.Current;
            boolArray[3] = (bool)this.Inputs.ElementAt(3).Value.Current;
            boolArray[4] = (bool)this.Inputs.ElementAt(4).Value.Current;
            boolArray[5] = (bool)this.Inputs.ElementAt(5).Value.Current;
            boolArray[6] = (bool)this.Inputs.ElementAt(6).Value.Current;
            boolArray[7] = (bool)this.Inputs.ElementAt(7).Value.Current;
            return boolArray;
        }

        /// <summary>
        /// Gets an integer array filled with the values of a bitmap source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>An integer array filled with the values of a bitmap source.</returns>
        private int BitArrayToInt(BitArray source)
        {
            var intArr = new int[1];
            source.CopyTo(intArr, 0);
            return intArr[0];
        }
    }
}
