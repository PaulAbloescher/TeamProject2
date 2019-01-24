// ---------------------------------------------------------------------
// <copyright file="IConnection.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Interface for a class that represents a connection between pins.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Component.Connection
{
    using System.Collections.Generic;
    using Shared;

    /// <summary>
    /// Interface for a class that represents a connection between pins.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets the output pin in the connection.
        /// </summary>
        /// <value>
        /// The output in the connection.
        /// </value>
        IPin Output { get; }

        /// <summary>
        /// Gets the input pins in the connection.
        /// </summary>
        /// <value>
        /// The input pins in the connection.
        /// </value>
        ICollection<IPin> InputPins { get; }

        /// <summary>
        /// Adds an input pin to the connection.
        /// </summary>
        /// <param name="pin">The pin that is added.</param>
        void AddInputPin(IPin pin);

        /// <summary>
        /// Removes an input pin from a connection.
        /// </summary>
        /// <param name="pin">The pin that is removed.</param>
        void RemoveInputPin(IPin pin);
    }
}
