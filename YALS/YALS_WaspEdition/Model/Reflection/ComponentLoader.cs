using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using System.IO;
using System.Reflection;
using YALS_WaspEdition.Model.Reflection;

namespace YALS_WaspEdition.Model.Component.Reflection
{
    public class ComponentLoader : IComponentLoader
    {
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
