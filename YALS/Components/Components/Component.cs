// ---------------------------------------------------------------------
// <copyright file="Component.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Abstract class for implementin a component in a logic simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Shared;

    /// <summary>
    /// Abstract class for implementing a component in a logic simulation.
    /// </summary>
    /// <seealso cref="Shared.IDisplayableNode" />
    [Serializable]
    public abstract class Component : IDisplayableNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        public Component()
        {
            this.Inputs = new List<IPin>();
            this.Outputs = new List<IPin>();
            this.Setup();
        }

        /// <summary>
        /// Occurs when [picture changed].
        /// </summary>
        public event EventHandler PictureChanged;

        /// <summary>
        /// Gets or sets the input pins of a component.
        /// </summary>
        /// <value>
        /// The input pins of a component.
        /// </value>
        public ICollection<IPin> Inputs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the output pins of a component.
        /// </summary>
        /// <value>
        /// The output pins of a component.
        /// </value>
        public ICollection<IPin> Outputs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label of a component.
        /// </summary>
        /// <value>
        /// The label of a component.
        /// </value>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description of a component.
        /// </summary>
        /// <value>
        /// The description of a component.
        /// </value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of a component.
        /// </summary>
        /// <value>
        /// The type of a component.
        /// </value>
        public NodeType Type
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the picture of a component.
        /// </summary>
        /// <value>
        /// The picture of a component.
        /// </value>
        public Bitmap Picture
        {
            get;
            set;
        }

        /// <summary>
        /// Defines what a component should do during a simulation.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Defines how the state of a component that can be activated should change when it is activated.
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Sets up a component.
        /// </summary>
        protected abstract void Setup();

        /// <summary>
        /// Used when the picture of a component changed.
        /// </summary>
        protected virtual void FirePictureChanged()
        {
            this.PictureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
