using ComponentTest.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Component;
using ComponentTest.Model.Component.Manager;
using Components.Components;

namespace ComponentTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var componentManager = new ComponentManager();

            Components.Components.OrComponent orComponent = new Components.Components.OrComponent();

            orComponent.Inputs.ElementAt(0).Value.Current = true;
            componentManager.Connect(orComponent, orComponent, orComponent.Outputs.ElementAt(0), orComponent.Inputs.ElementAt(1));
      


            Console.WriteLine();
        }
    }
}
