// <copyright file="ConnectionVM.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents the Connection View Model.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.ViewModels
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Represents the Connection View Model.
    /// </summary>
    [Serializable]
    public class ConnectionVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionVM"/> class.
        /// </summary>
        /// <param name="outputPin">The output pin.</param>
        /// <param name="inputPin">The input pin.</param>
        /// <param name="disconnectCommand">The disconnect command.</param>
        public ConnectionVM(PinVM outputPin, PinVM inputPin, ICommand disconnectCommand)
        {
            this.OutputPin = outputPin ?? throw new ArgumentNullException(nameof(outputPin));
            this.InputPin = inputPin ?? throw new ArgumentNullException(nameof(inputPin));
            this.DisconnectCommand = disconnectCommand ?? throw new ArgumentNullException(nameof(disconnectCommand));
        }

        /// <summary>
        /// Gets the output pin.
        /// </summary>
        /// <value>
        /// The output pin.
        /// </value>
        public PinVM OutputPin
        {
            get;
        }

        /// <summary>
        /// Gets the input pin.
        /// </summary>
        /// <value>
        /// The input pin.
        /// </value>
        public PinVM InputPin
        {
            get;
        }

        /// <summary>
        /// Gets the connection as string.
        /// </summary>
        /// <value>
        /// The connection as string.
        /// </value>
        public string ConnectionAsString
        {
            get
            {
                return $"Output: {this.OutputPin.Pin.Label} -> Input: {this.InputPin.Pin.Label}";
            }
        }

        /// <summary>
        /// Gets the disconnect command.
        /// </summary>
        /// <value>
        /// The disconnect command.
        /// </value>
        public ICommand DisconnectCommand
        {
            get;
        }
    }
}