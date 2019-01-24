// ---------------------------------------------------------------------
// <copyright file="HexDisplayComponent.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The component for simulating a hex display in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Shared;

    /// <summary>
    /// The component for simulating a hex display in a logic simulation.
    /// </summary>
    /// <seealso cref="Components.Components.Component" />
    [Serializable]
    public class HexDisplayComponent : Component
    {
        /// <summary>
        /// The image when the hex display shows a zero.
        /// </summary>
        private Bitmap zeroImg;

        /// <summary>
        /// The image when the hex display shows a one.
        /// </summary>
        private Bitmap oneImg;

        /// <summary>
        /// The image when the hex display shows a two.
        /// </summary>
        private Bitmap twoImg;

        /// <summary>
        /// The image when the hex display shows a three.
        /// </summary>
        private Bitmap threeImg;

        /// <summary>
        /// The image when the hex display shows a four.
        /// </summary>
        private Bitmap fourImg;

        /// <summary>
        /// The image when the hex display shows a five.
        /// </summary>
        private Bitmap fiveImg;

        /// <summary>
        /// The image when the hex display shows a six.
        /// </summary>
        private Bitmap sixImg;

        /// <summary>
        /// The image when the hex display shows a seven.
        /// </summary>
        private Bitmap sevenImg;

        /// <summary>
        /// The image when the hex display shows an eight.
        /// </summary>
        private Bitmap eightImg;

        /// <summary>
        /// The image when the hex display shows a nine.
        /// </summary>
        private Bitmap nineImg;
        
        /// <summary>
        /// The image when the hex display shows an A.
        /// </summary>
        private Bitmap aImg;

        /// <summary>
        /// The image when the hex display shows a B.
        /// </summary>
        private Bitmap bImg;

        /// <summary>
        /// The image when the hex display shows a C.
        /// </summary>
        private Bitmap cImg;

        /// <summary>
        /// The image when the hex display shows a D.
        /// </summary>
        private Bitmap dImg;

        /// <summary>
        /// The image when the hex display shows a E.
        /// </summary>
        private Bitmap eImg;

        /// <summary>
        /// The image when the hex display shows a F.
        /// </summary>
        private Bitmap fImg;

        /// <summary>
        /// The images for the different states of the hex display.
        /// </summary>
        private Bitmap[] stateImages;

        /// <summary>
        /// Does nothing for the hex display component.
        /// </summary>
        public override void Activate()
        {
            // Nothing to do here.
        }

        /// <summary>
        /// Sets the picture corresponding to the inputs.
        /// </summary>
        public override void Execute()
        {
            this.Picture = this.GetRepresentingStateImage();

            this.FirePictureChanged();
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.Type = NodeType.Display;
            this.Label = "HexDisplay";
            this.Description = "Shows a hex value from 0 to 15 corresponding to the input bools.";
            Pin<bool> x1 = new Pin<bool>("x1");
            Pin<bool> x2 = new Pin<bool>("x2");
            Pin<bool> x3 = new Pin<bool>("x3");
            Pin<bool> x4 = new Pin<bool>("x4");
            this.Inputs.Add(x1);
            this.Inputs.Add(x2);
            this.Inputs.Add(x3);
            this.Inputs.Add(x4);
            this.LoadImages();
            this.Picture = this.zeroImg;
        }

        /// <summary>
        /// Loads the images for the component.
        /// </summary>
        private void LoadImages()
        {
            this.zeroImg = Properties.Resources._0;
            this.oneImg = Properties.Resources._1;
            this.twoImg = Properties.Resources._2;
            this.threeImg = Properties.Resources._3;
            this.fourImg = Properties.Resources._4;
            this.fiveImg = Properties.Resources._5;
            this.sixImg = Properties.Resources._6;
            this.sevenImg = Properties.Resources._7;
            this.eightImg = Properties.Resources._8;
            this.nineImg = Properties.Resources._9;
            this.aImg = Properties.Resources._10;
            this.bImg = Properties.Resources._11;
            this.cImg = Properties.Resources._12;
            this.dImg = Properties.Resources._13;
            this.eImg = Properties.Resources._14;
            this.fImg = Properties.Resources._15;

            this.stateImages = new Bitmap[]
            {
                this.zeroImg, this.oneImg, this.twoImg, this.threeImg, this.fourImg,
                this.fiveImg, this.sixImg, this.sevenImg, this.eightImg, this.nineImg,
                this.aImg, this.bImg, this.cImg, this.dImg, this.eImg, this.fImg
            };
        }

        /// <summary>
        /// Gets the image corresponding to the inputs.
        /// </summary>
        /// <returns>The image corresponding to the inputs.</returns>
        private Bitmap GetRepresentingStateImage()
        {
            int decimalNumberForPins = 0;

            if ((bool)this.Inputs.ElementAt(0).Value.Current)
            {
                decimalNumberForPins += 1;
            }

            if ((bool)this.Inputs.ElementAt(1).Value.Current)
            {
                decimalNumberForPins += 2;
            }

            if ((bool)this.Inputs.ElementAt(2).Value.Current)
            {
                decimalNumberForPins += 4;
            }

            if ((bool)this.Inputs.ElementAt(3).Value.Current)
            {
                decimalNumberForPins += 8;
            }

            return this.stateImages[decimalNumberForPins];
        }
    }
}
