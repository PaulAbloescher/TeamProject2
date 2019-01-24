// <copyright file="IValueGeneric.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the interface for a generic pin value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueGeneric<T> : IValue
    {
        /// <summary>
        /// Gets or sets the current pin value.
        /// </summary>
        /// <value>Pin value as type T</value>
        new T Current { get; set; }
    }
}
