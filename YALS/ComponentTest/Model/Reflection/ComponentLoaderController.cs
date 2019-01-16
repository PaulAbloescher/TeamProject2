using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ComponentTest.Model.Reflection
{
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
