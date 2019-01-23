﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    [Serializable()]
    public class OrComponent : Component
    {
        public OrComponent()
        {
        }

        public override void Activate()
        {
           
        }

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

        private void LoadImage()
        {
            this.Picture = Properties.Resources.OrGate;
        }
    }
}