// <copyright file="PinSelectedEventArgs.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the PinSelectedEventArgs class.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.MyEventArgs
{
    using System;
    using YALS_WaspEdition.ViewModels;

    /// <summary>
    /// Represents the PinSelectedEventArgs class.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class PinSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PinSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="pin">Represents the pin.</param>
        /// <exception cref="ArgumentNullException">Thrown when the pin name is null.</exception>
        public PinSelectedEventArgs(PinVM pin)
        {
            this.SelectedPin = pin ?? throw new ArgumentNullException(nameof(pin));
        }

        /// <summary>
        /// Gets the selected pin.
        /// </summary>
        /// <value>
        /// The selected pin.
        /// </value>
        public PinVM SelectedPin
        {
            get;
            private set;
        }
    }
}
