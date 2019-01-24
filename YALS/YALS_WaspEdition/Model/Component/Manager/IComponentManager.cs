// ---------------------------------------------------------------------
// <copyright file="IComponentManager.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The interface for a manager for the components in the main canvas.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Component.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared;
    using YALS_WaspEdition.Model.Component.Connection;

    /// <summary>
    /// The interface for a manager for the components in the main canvas.
    /// </summary>
    public interface IComponentManager
    {
        /// <summary>
        /// Occurs when one step has finished.
        /// </summary>
        event EventHandler StepFinished;

        /// <summary>
        /// Gets the components in the simulation.
        /// </summary>
        /// <value>
        /// The components in the simulation.
        /// </value>
        ICollection<INode> Components { get; }
        
        /// <summary>
        /// Gets the connections in the simulation.
        /// </summary>
        /// <value>
        /// The connections in the simulation.
        /// </value>
        ICollection<IConnection> Connections { get; }

        /// <summary>
        /// Gets a value indicating whether the simulation is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the simulation is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        /// Connects the specified input and output pins.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        void Connect(IPin outputPin, IPin inputPin);

        /// <summary>
        /// Disconnects the specified input and output pins.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        void Disconnect(IPin outputPin, IPin inputPin);

        /// <summary>
        /// Adds a node to the simulation.
        /// </summary>
        /// <param name="node">The node that is added.</param>
        void AddNode(INode node);

        /// <summary>
        /// Removes a node from the simulation.
        /// </summary>
        /// <param name="node">The node that is removed.</param>
        void RemoveNode(INode node);

        /// <summary>
        /// Starts the simulation.
        /// </summary>
        void Play();

        /// <summary>
        /// Executes a step in the simulation.
        /// </summary>
        void Step();

        /// <summary>
        /// Stops the simulation.
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts the simulation asynchronously.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        Task PlayAsync();

        /// <summary>
        /// Executes a step in the simulation asynchronously.
        /// </summary>
        /// <returns>A task awaited.</returns>
        Task StepAsync();

        /// <summary>
        /// Stops the simulation asynchronously.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        Task StopAsync();
    }
}
