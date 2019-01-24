// ----------------------------------------------------------------------- 
// <copyright file="IComponentLoader.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ComponentLoader interface.</summary> 
// <author>Killerwasps</author> 
//-----------------------------------------------------------------------

namespace YALS_WaspEdition.Model.Reflection
{
    using System.Collections.Generic;
    using Shared;

    /// <summary>
    /// Represents the <see cref="IComponentLoader"/> interface.
    /// </summary>
    public interface IComponentLoader
    {
        /// <summary>
        /// Loads <see cref="IDisplayableNode"/> from the specified paths with reflection and returns them in a dictionary sorted by <see cref="NodeType"/>.
        /// </summary>
        /// <param name="paths">The directory paths from which the assembly items get loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        IDictionary<NodeType, ICollection<IDisplayableNode>> Load(IEnumerable<string> paths);

        /// <summary>
        /// Loads <see cref="IDisplayableNode"/> from the specified path with reflection and returns them in a dictionary sorted by <see cref="NodeType"/>.
        /// </summary>
        /// <param name="path">The directory path from which the assemblies get loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path);
    }
}
