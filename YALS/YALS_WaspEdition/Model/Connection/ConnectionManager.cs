using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Component.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        public ConnectionManager()
        {
            this.Connections = new List<IConnection>();
        }

        public ICollection<IConnection> Connections
        {
            get;
            private set;
        }

        public void Connect(IPin output, IPin input)
        {
            if (this.CheckPinCompatibility(output, input))
            {
                var existingConnection = this.Connections.FirstOrDefault(c => c.Output.Equals(output));

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

        private IConnection CheckIfPinIsInConnection(IPin pin)
        {
            var connection = this.Connections.FirstOrDefault(c => c.InputPins.Contains(pin));

            if (connection != null)
            {
                return connection;
            } else
            {
                throw new InvalidOperationException("The pin is not connected.");
            }
        }

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
