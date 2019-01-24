// ----------------------------------------------------------------------- 
// <copyright file="IComponentLoaderController.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the IComponentLoaderController interface.</summary> 
// <author>Killerwasps</author> 
//-----------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Reflection
{ 
    using System.Collections.Generic;
    using Shared;

    /// <summary>
    /// Represents the <see cref="IComponentLoaderController"/> interface.
    /// </summary>
    public interface IComponentLoaderController
    {
        /// <summary>
        /// Loads the <see cref="IDisplayableNode"/> items from the specified path.
        /// </summary>
        /// <param name="path">The path from which the assembly gets loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path);
    }
}
