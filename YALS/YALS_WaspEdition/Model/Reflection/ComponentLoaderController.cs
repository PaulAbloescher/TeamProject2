// ----------------------------------------------------------------------- 
// <copyright file="ComponentLoaderController.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ComponentLoaderController class.</summary> 
// <author>Killerwasps</author> 
//-----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Component.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Shared;
    using YALS_WaspEdition.Model.Reflection;

    /// <summary>
    /// Represents the <see cref="ComponentLoaderController"/> class.
    /// </summary>
    public class ComponentLoaderController : IComponentLoaderController
    {
        /// <summary>
        /// The component loader of the class.
        /// </summary>
        private readonly IComponentLoader loader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentLoaderController"/> class.
        /// </summary>
        /// <param name="loader">The component loader for the class.</param>
        public ComponentLoaderController(IComponentLoader loader)
        {
            this.loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        /// <summary>
        /// Loads the <see cref="IDisplayableNode"/> items from the specified path.
        /// </summary>
        /// <param name="path">The path from which the assembly gets loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        public IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path)
        {
            this.CreatePluginsFolder();
            var result = this.loader.Load(path);
            return result;
        }

        /// <summary>
        /// Creates the plugins folder.
        /// </summary>
        private void CreatePluginsFolder()
        {
            Directory.CreateDirectory("Plugins");
        }
    }
}
