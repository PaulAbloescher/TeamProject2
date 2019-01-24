// ---------------------------------------------------------------------
// <copyright file="IConnectionManager.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The interface for a class that manages connections in a simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Component.Connection
{
    using System.Collections.Generic;
    using Shared;

    /// <summary>
    /// The interface for a class that manages connections in a simulation.
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// Gets the connections in the simulation.
        /// </summary>
        /// <value>
        /// The connections in the simulation.
        /// </value>
        ICollection<IConnection> Connections { get; }

        /// <summary>
        /// Connects the specified input and output pins.
        /// </summary>
        /// <param name="output">The output pin.</param>
        /// <param name="input">The input pin.</param>
        void Connect(IPin output, IPin input);

        /// <summary>
        /// Disconnects the specified input and output pins.
        /// </summary>
        /// <param name="output">The output pin.</param>
        /// <param name="input">The input pin.</param>
        void Disconnect(IPin output, IPin input);
    }
}
