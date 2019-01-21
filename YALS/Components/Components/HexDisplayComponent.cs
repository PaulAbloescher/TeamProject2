﻿using System;
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

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        protected override void Setup()
        {
            Pin<bool> x1 = new Pin<bool>("x1");
            Pin<bool> x2 = new Pin<bool>("x2");
            Pin<bool> x3 = new Pin<bool>("x3");
            Pin<bool> x4 = new Pin<bool>("x4");
            this.Inputs.Add(x1);
            this.Inputs.Add(x2);
            this.Inputs.Add(x3);
            this.Inputs.Add(x4);
            this.LoadImages();
        }

        private void LoadImages()
        {
            throw new NotImplementedException();
        }

        private Bitmap GetRepresentingStateImage()
        {
            throw new NotImplementedException();
        }
    }
}
