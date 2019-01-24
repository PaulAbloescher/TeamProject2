// <copyright file="IPinGeneric.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the interface for a generic pin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPinGeneric<T> : IPin
    {
        /// <summary>
        /// Gets or sets the pin value.
        /// </summary>
        /// <value>An IValueGeneric instance.</value>
        new IValueGeneric<T> Value { get; set; }
    }
}
