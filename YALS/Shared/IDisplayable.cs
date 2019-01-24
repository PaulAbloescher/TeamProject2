// <copyright file="IDisplayable.cs" company="KW Softworks">
//     Copyright (c) Strommer Kilian. All rights reserved.
// </copyright>
// <summary>Represents the bezier line converter.</summary>
// <author>Killerwasps</author>
namespace Shared
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Defines the interface for a displayable object.
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// The event handler for when the picture changes.
        /// </summary>
        event EventHandler PictureChanged;

        /// <summary>
        /// Gets the image of the object.
        /// </summary>
        /// <value>A Bitmap image.</value>
        Bitmap Picture { get; }
    }
}
