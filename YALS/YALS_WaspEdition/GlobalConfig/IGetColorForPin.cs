// ----------------------------------------------------------------------- 
// <copyright file="IGetColorForPin.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the IGetColorForPin interface.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.GlobalConfig
{
    using System.Windows.Media;
    using Shared;

    /// <summary>
    /// Represents the <see cref="IGetColorForPin"/> class.
    /// </summary>
    public interface IGetColorForPin
    {
        /// <summary>
        /// Gets a <see cref="Color"/> for an <see cref="IPin"/> object.
        /// </summary>
        /// <param name="item">The <see cref="IPin"/> item.</param>
        /// <returns>The <see cref="Color"/> for the pin.</returns>
        Color GetColor(IPin item);
    }
}
