﻿using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Model.Component
{
    public abstract class Component : IDisplayableNode
    {

        public Component()
        {
            this.Inputs = new List<IPin>();
            this.Outputs = new List<IPin>();
        }

        public ICollection<IPin> Inputs
        {
            get;
            set;
        }

        public ICollection<IPin> Outputs
        {
            get;
            set;
        }

        public string Label
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public NodeType Type
        {
            get;
            private set;
        }

        public Bitmap Picture
        {
            get;
            set;
        }

        public event EventHandler PictureChanged;

        public abstract void Execute();
    }
}
