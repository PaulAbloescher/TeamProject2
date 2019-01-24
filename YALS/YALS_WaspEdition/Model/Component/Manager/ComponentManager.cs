// ---------------------------------------------------------------------
// <copyright file="ComponentManager.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>The manager for the components in the main canvas.</summary>
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
    /// The manager for the components in the main canvas.
    /// </summary>
    /// <seealso cref="YALS_WaspEdition.Model.Component.Manager.IComponentManager" />
    [Serializable]
    public class ComponentManager : IComponentManager
    {
        /// <summary>
        /// The manager holding all the connections in the simulation.
        /// </summary>
        private readonly IConnectionManager connectionManager;

        /// <summary>
        /// Determines if the simulation is running.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        /// <param name="manager">The manager of the connections in the simulation.</param>
        public ComponentManager(IConnectionManager manager)
        {
            this.connectionManager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.Components = new List<INode>();
            this.isRunning = false;
        }

        /// <summary>
        /// Occurs when one step has finished.
        /// </summary>
        public event EventHandler StepFinished;

        /// <summary>
        /// Gets the components in the simulation.
        /// </summary>
        /// <value>
        /// The components in the simulation.
        /// </value>
        public ICollection<INode> Components
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the connections in the simulation.
        /// </summary>
        /// <value>
        /// The connections in the simulation.
        /// </value>
        public ICollection<IConnection> Connections
        {
            get
            {
                return this.connectionManager.Connections;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the simulation is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the simulation is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        /// <summary>
        /// Adds a node to the simulation.
        /// </summary>
        /// <param name="node">The node that is added.</param>
        public void AddNode(INode node)
        {
            this.Components.Add(node);
        }

        /// <summary>
        /// Connects the specified input and output pins.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        public void Connect(IPin outputPin, IPin inputPin)
        {
            this.connectionManager.Connect(outputPin, inputPin);
        }

        /// <summary>
        /// Disconnects the specified input and output pins.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        public void Disconnect(IPin outputPin, IPin inputPin)
        {
            this.connectionManager.Disconnect(outputPin, inputPin);
        }

        /// <summary>
        /// Starts the simulation.
        /// </summary>
        public void Play()
        {
            this.isRunning = !this.isRunning;

            while (this.isRunning)
            {
                this.Step();
            }
        }

        /// <summary>
        /// Starts the simulation asynchronously.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        public Task PlayAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                this.Play();
            });

            return task;
        }

        /// <summary>
        /// Removes a node from the simulation.
        /// </summary>
        /// <param name="node">The node that is removed.</param>
        public void RemoveNode(INode node)
        {
            this.Components.Remove(node);
        }

        /// <summary>
        /// Executes a step in the simulation.
        /// </summary>
        public void Step()
        {
            foreach (var component in this.Components)
            {
                component.Execute();
            }

            this.FireStepFinished();
        }

        /// <summary>
        /// Executes a step in the simulation asynchronously.
        /// </summary>
        /// <returns>A task awaited.</returns>
        public Task StepAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                this.Step();
            });

            return task;
        }

        /// <summary>
        /// Stops the simulation.
        /// </summary>
        public void Stop()
        {
            this.isRunning = false;
        }

        /// <summary>
        /// Stops the simulation asynchronously.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        public Task StopAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                this.Stop();
            });

            return task;
        }

        /// <summary>
        /// Used when a step finished.
        /// </summary>
        protected internal void FireStepFinished()
        {
            this.StepFinished?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Checks if a pin is an input pin.
        /// </summary>
        /// <param name="component">The component that contains the pin.</param>
        /// <param name="pin">The pin that is checked.</param>
        /// <returns>If the pin is an input pin.</returns>
        private bool CheckIfPinIsInput(INode component, IPin pin)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (pin == null)
            {
                throw new ArgumentNullException(nameof(pin));
            }

            var isInputPin = component.Inputs.Contains(pin);
            return isInputPin;
        }

        /// <summary>
        /// Checks if a pin is an output pin.
        /// </summary>
        /// <param name="component">The component that contains the pin.</param>
        /// <param name="pin">The pin that is checked.</param>
        /// <returns>If the pin is an output pin.</returns>
        private bool CheckIfPinIsOutput(INode component, IPin pin)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (pin == null)
            {
                throw new ArgumentNullException(nameof(pin));
            }

            var isOutputPin = component.Outputs.Contains(pin);
            return isOutputPin;
        }
    }
}
