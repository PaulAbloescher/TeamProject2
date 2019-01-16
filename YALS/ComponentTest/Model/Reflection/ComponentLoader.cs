using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ComponentTest.Model.Reflection
{
    public class ComponentLoader : IComponentLoader
    {
        public IDictionary<NodeType, ICollection<INode>> Load(IEnumerable<string> paths)
        {
            throw new NotImplementedException();
        }

        public IDictionary<NodeType, ICollection<INode>> Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}
