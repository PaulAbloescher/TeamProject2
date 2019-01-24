// ----------------------------------------------------------------------- 
// <copyright file="ComponentLoader.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ComponentLoader class.</summary> 
// <author>Killerwasps</author> 
//-----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Component.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;   
    using System.Reflection;
    using Shared;
    using YALS_WaspEdition.Model.Reflection;

    /// <summary>
    /// Represents the <see cref="ComponentLoader"/> class.
    /// </summary>
    /// <seealso cref="YALS_WaspEdition.Model.Reflection.IComponentLoader" />
    public class ComponentLoader : IComponentLoader
    {
        /// <summary>
        /// Loads <see cref="IDisplayableNode"/> from the specified paths with reflection and returns them in a dictionary sorted by <see cref="NodeType"/>.
        /// </summary>
        /// <param name="paths">The directory paths from which the assembly items get loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        public IDictionary<NodeType, ICollection<IDisplayableNode>> Load(IEnumerable<string> paths)
        {
            IDictionary<NodeType, ICollection<IDisplayableNode>> components = new Dictionary<NodeType, ICollection<IDisplayableNode>>();

            foreach (string path in paths)
            {
                var interimResult = this.Load(path);
                this.MergeDictionaries(interimResult, components);
            }

            return components;
        }

        /// <summary>
        /// Loads <see cref="IDisplayableNode"/> from the specified path with reflection and returns them in a dictionary sorted by <see cref="NodeType"/>.
        /// </summary>
        /// <param name="path">The directory path from which the assemblies get loaded.</param>
        /// <returns>A <see cref="IDisplayableNode"/> dictionary sorted by <see cref="NodeType"/>.</returns>
        public IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path)
        {
            IDictionary<NodeType, ICollection<IDisplayableNode>> components = new Dictionary<NodeType, ICollection<IDisplayableNode>>();

            foreach (string file in Directory.GetFiles($"{path}", "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom($"{file}");

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetInterfaces().Contains(typeof(IDisplayableNode)) && !type.IsAbstract)
                    {
                        var component = Activator.CreateInstance(type) as IDisplayableNode;

                        if (component != null)
                        {
                            if (!components.ContainsKey(component.Type))
                            {
                                components.Add(component.Type, new List<IDisplayableNode>());
                            }

                            components[component.Type].Add(component);
                        }
                    }
                }
            }

            return components;
        }

        /// <summary>
        /// Merges two <see cref="IDictionary{NodeType, ICollection{IDisplayableNode}}"/> dictionaries.
        /// </summary>
        /// <param name="source">The source dictionary.</param>
        /// <param name="target">The target dictionary.</param>
        private void MergeDictionaries(IDictionary<NodeType, ICollection<IDisplayableNode>> source, IDictionary<NodeType, ICollection<IDisplayableNode>> target)
        {
            foreach (KeyValuePair<NodeType, ICollection<IDisplayableNode>> pair in source)
            {
                if (target.ContainsKey(pair.Key))
                {
                    List<IDisplayableNode> newTargetItems = new List<IDisplayableNode>(target[pair.Key]);
                    newTargetItems.AddRange(pair.Value);

                    target[pair.Key] = newTargetItems;
                }
                else
                {
                    target.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
