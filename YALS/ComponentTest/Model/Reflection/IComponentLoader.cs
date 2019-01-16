using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Model.Reflection
{
    public interface IComponentLoader
    {
        IDictionary<NodeType, ICollection<INode>> Load(IEnumerable<string> paths);
        IDictionary<NodeType, ICollection<INode>> Load(string path);
    }
}
