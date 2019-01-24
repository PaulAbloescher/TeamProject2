// ---------------------------------------------------------------------
// <copyright file="Connection.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Represents a connection between pins.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Component.Connection
{
    using System;
    using System.Collections.Generic;
    using Shared;

    /// <summary>
    /// Represents a connection between pins.
    /// </summary>
    /// <seealso cref="YALS_WaspEdition.Model.Component.Connection.IConnection" />
    public class Connection : IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="output">The output pin in the connection.</param>
        public Connection(IPin output)
        {
            this.InputPins = new List<IPin>();
            this.Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Gets or sets the output pin in the connection.
        /// </summary>
        /// <value>
        /// The output in the connection.
        /// </value>
        public IPin Output
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the input pins in the connection.
        /// </summary>
        /// <value>
        /// The input pins in the connection.
        /// </value>
        public ICollection<IPin> InputPins
        {
            get;
            set;
        }

        /// <summary>
        /// Adds an input pin to the connection.
        /// </summary>
        /// <param name="pin">The pin that is added.</param>
        public void AddInputPin(IPin pin)
        {
            pin.Value = this.Output.Value;
            this.InputPins.Add(pin);
        }

        /// <summary>
        /// Removes an input pin from a connection.
        /// </summary>
        /// <param name="pin">The pin that is removed.</param>
        public void RemoveInputPin(IPin pin)
        {
            if (this.InputPins.Remove(pin))
            {
                pin.Value = null;
            }
        }
    }
}
