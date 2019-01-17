using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.DI;
using YALS_WaspEdition.Model.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace YALS_WaspEdition.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public MainVM()
        {
            this.Setup();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IDictionary<NodeType, ICollection<NodeVM>> AvailableComponents
        {
            get;
            set;
        }

        protected virtual void FirePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Setup()
        {
            this.LoadComponents();
        }

        private void LoadComponents()
        {
            var loader = Provider.Container.GetService<IComponentLoaderController>();
            var result = loader.Load("Plugins");
            this.AvailableComponents = this.CreateNodeVmDictionary(result);
            this.FirePropertyChanged(nameof(this.AvailableComponents));
        }

        private IDictionary<NodeType, ICollection<NodeVM>> CreateNodeVmDictionary(IDictionary<NodeType, ICollection<IDisplayableNode>> source)
        {
            var dictionary = new Dictionary<NodeType, ICollection<NodeVM>>();

            foreach (var kvp in source)
            {
                dictionary.Add(kvp.Key, kvp.Value.Select(c => new NodeVM(c)).ToList());
            }

            return dictionary;
        }
    }
}
