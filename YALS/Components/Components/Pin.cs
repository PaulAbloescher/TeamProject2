// ---------------------------------------------------------------------
// <copyright file="Pin.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Represents a pin on a component.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace Components.Components
{
    using System;
    using Shared;

    /// <summary>
    /// Represents a pin on a component.
    /// </summary>
    /// <typeparam name="T">The type of the pin.</typeparam>
    /// <seealso cref="Shared.IPinGeneric{T}" />
    [Serializable]
    public class Pin<T> : IPinGeneric<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pin{T}"/> class.
        /// </summary>
        /// <param name="label">The label of the pin.</param>
        public Pin(string label)
        {
            this.Label = label ?? throw new ArgumentNullException(nameof(label));
            this.Value = new ValueGeneric<T>();
        }

        /// <summary>
        /// Gets or sets the value of the pin.
        /// </summary>
        /// <value>
        /// The value of the pin.
        /// </value>
        public IValueGeneric<T> Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label of the pin.
        /// </summary>
        /// <value>
        /// The label of the pin.
        /// </value>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the generic value of the pin.
        /// </summary>
        /// <value>
        /// The generic value of the pin.
        /// </value>
        IValue IPin.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (IValueGeneric<T>)value;
            }
        }
    }
}
