using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Reflection
{
    public interface IComponentLoaderController
    {
        IDictionary<NodeType, ICollection<IDisplayableNode>> Load(string path);
    }
}
