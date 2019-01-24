// <copyright file="IValue.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    /// <summary>
    /// Defines the interface for a pin value.
    /// </summary>
    public interface IValue
    {
        /// <summary>
        /// Gets or sets the current pin value.
        /// </summary>
        /// <value>Object representing the pin value.</value>
        object Current { get; set; }
    }
}
