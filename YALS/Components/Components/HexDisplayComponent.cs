using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Components
{
    public class HexDisplayComponent : Component
    {
        private Bitmap zeroImg;
        private Bitmap oneImg;
        private Bitmap twoImg;
        private Bitmap threeImg;
        private Bitmap fourImg;
        private Bitmap fiveImg;
        private Bitmap sixImg;
        private Bitmap sevenImg;
        private Bitmap eightImg;
        private Bitmap nineImg;
        private Bitmap aImg;
        private Bitmap bImg;
        private Bitmap cImg;
        private Bitmap dImg;
        private Bitmap eImg;
        private Bitmap fImg;

        private Bitmap[] stateImages;

        public override void Activate()
        {
            // Nothing to do here.
        }

        public override void Execute()
        {
            this.Picture = this.GetRepresentingStateImage();

            this.FirePictureChanged();
        }

        protected override void Setup()
        {
            this.Type = NodeType.Display;
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

            this.stateImages = new Bitmap[] { this.zeroImg, this.oneImg, this.twoImg, this.threeImg, this.fourImg,
                                              this.fiveImg, this.sixImg, this.sevenImg, this.eightImg, this.nineImg,
                                              this.aImg, this.bImg, this.cImg, this.dImg, this.eImg, this.fImg};
        }

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
