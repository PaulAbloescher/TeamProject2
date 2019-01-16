﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Component.Reflection
{
    public interface IComponentLoader
    {
        IDictionary<NodeType, ICollection<INode>> Load(IEnumerable<string> paths);
        IDictionary<NodeType, ICollection<INode>> Load(string path);
    }
}
