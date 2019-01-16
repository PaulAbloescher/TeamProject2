using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using System.IO;
using System.Reflection;

namespace YALS_WaspEdition.Model.Component.Reflection
{
    public class ComponentLoader : IComponentLoader
    {
        public IDictionary<NodeType, ICollection<INode>> Load(IEnumerable<string> paths)
        {
            IDictionary<NodeType, ICollection<INode>> components = new Dictionary<NodeType, ICollection<INode>>();

            foreach (string path in paths)
            {
                var interimResult = this.Load(path);
                this.MergeDictionaries(interimResult, components);
            }

            return components;
        }

        public IDictionary<NodeType, ICollection<INode>> Load(string path)
        {
            IDictionary<NodeType, ICollection<INode>> components = new Dictionary<NodeType, ICollection<INode>>();

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
                                components.Add(component.Type, new List<INode>());
                            }

                            components[component.Type].Add(component);
                        }
                    }
                }
            }

            return components;
        }

        private void MergeDictionaries(IDictionary<NodeType, ICollection<INode>> source, IDictionary<NodeType, ICollection<INode>> target)
        {
            foreach (KeyValuePair<NodeType, ICollection<INode>> pair in source)
            {
                if (target.ContainsKey(pair.Key))
                {
                    List<INode> newTargetItems = new List<INode>(target[pair.Key]);
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
