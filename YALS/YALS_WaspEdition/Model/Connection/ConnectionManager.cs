// ---------------------------------------------------------------------
// <copyright file="ConnectionManager.cs" company="FHWN.ac.at">
// Copyright(c) FHWN. All rights reserved.
// </copyright>
// <summary>Manages the connections in the simulation.</summary>
// <author>Killerwasps</author>
// ---------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Component.Connection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Shared;

    /// <summary>
    /// Manages the connections in the simulation.
    /// </summary>
    /// <seealso cref="YALS_WaspEdition.Model.Component.Connection.IConnectionManager" />
    [Serializable]
    public class ConnectionManager : IConnectionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionManager"/> class.
        /// </summary>
        public ConnectionManager()
        {
            this.Connections = new List<IConnection>();
        }

        /// <summary>
        /// Gets the connections in the simulation.
        /// </summary>
        /// <value>
        /// The connections in the simulation.
        /// </value>
        public ICollection<IConnection> Connections
        {
            get;
            private set;
        }

        /// <summary>
        /// Connects the specified input and output pins.
        /// </summary>
        /// <param name="output">The output pin.</param>
        /// <param name="input">The input pin.</param>
        public void Connect(IPin output, IPin input)
        {
            if (this.CheckPinCompatibility(output, input))
            {
                var existingConnection = this.Connections.FirstOrDefault(c => c.Output.Equals(output));
                var existingInputConnection = this.Connections.FirstOrDefault(c => c.InputPins.Contains(input));

                if (existingInputConnection != null)
                {
                    throw new InvalidOperationException("An input pin cannot be connected twice.");
                }

                if (existingConnection == null)
                {
                    var connection = new Connection(output);
                    connection.AddInputPin(input);
                    this.Connections.Add(connection);
                }
                else
                {
                    existingConnection.AddInputPin(input);
                }
            }
            else
            {
                throw new InvalidOperationException("The types of the given pins do not match.");
            }
        }

        /// <summary>
        /// Disconnects the specified input and output pins.
        /// </summary>
        /// <param name="output">The output pin.</param>
        /// <param name="input">The input pin.</param>
        public void Disconnect(IPin output, IPin input)
        {
            var existingConnection = this.Connections.FirstOrDefault(c => c.Output.Equals(output));

            if (existingConnection != null)
            {
                existingConnection.RemoveInputPin(input);

                if (existingConnection.InputPins.Count == 0)
                {
                    this.Connections.Remove(existingConnection);
                }
            }
        }

        /// <summary>
        /// Checks if pin is in connection.
        /// </summary>
        /// <param name="pin">The pin that is checked.</param>
        /// <returns>The connection which the pin is in.</returns>
        private IConnection CheckIfPinIsInConnection(IPin pin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.InputPins.Contains(pin));

            if (connection != null)
            {
                return connection;
            }
            else
            {
                throw new InvalidOperationException("The pin is not connected.");
            }
        }

        /// <summary>
        /// Checks the compatibility of two pins.
        /// </summary>
        /// <param name="firstPin">The first pin.</param>
        /// <param name="secondPin">The second pin.</param>
        /// <returns>If the pins are compatible.</returns>
        private bool CheckPinCompatibility(IPin firstPin, IPin secondPin)
        {
            var firstPinType = firstPin.GetType();
            var firstPinGenericTypes = firstPinType.GetGenericArguments();

            var secondPinType = secondPin.GetType();
            var secondPinGenericTypes = secondPinType.GetGenericArguments();

            if (firstPinGenericTypes.Length > 0 && secondPinGenericTypes.Length > 0)
            {
                var firstGenericType = firstPinGenericTypes[0];
                var secondGenericType = secondPinGenericTypes[0];

                if (firstGenericType == secondGenericType)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
