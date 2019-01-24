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
    public class ComponentLoaderController : IComponentLoaderController
    {
        private readonly IComponentLoader loader;

        public ComponentLoaderController(IComponentLoader loader)
        {
            this.loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path)
        {
            this.CreatePluginsFolder();
            var result = this.loader.Load(path);
            return result;
        }

        private void CreatePluginsFolder()
        {
            Directory.CreateDirectory("Plugins");
        }
    }
}
