// <copyright file="IPin.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    /// <summary>
    /// Defines the interface for node pins.
    /// </summary>
    public interface IPin
    {
        /// <summary>
        /// Gets or sets the label text of the pin.
        /// </summary>
        /// <value>String containing the label text.</value>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the IValue of the pin.
        /// </summary>
        /// <value>An IValue instance.</value>
        IValue Value { get; set; }
    }
}
