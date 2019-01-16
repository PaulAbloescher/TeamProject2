using ComponentTest.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Component;
using ComponentTest.Model.Component.Manager;

namespace ComponentTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var componentManager = new ComponentManager();
            var andComponent = new AndComponent();
            var p1 = andComponent.Inputs.ElementAt(0);
            var p2 = andComponent.Inputs.ElementAt(1);
            var p3 = andComponent.Outputs.First();

            
            var orComponent = new OrComponent();
            var p4 = orComponent.Inputs.ElementAt(0);
            var p5 = orComponent.Inputs.ElementAt(1);
            var p6 = orComponent.Outputs.First();

            componentManager.Connect(andComponent, orComponent, p3, p4);

            p1.Value.Value = true;
            p2.Value.Value = true;
            p5.Value.Value = false;

            componentManager.Components.Add(orComponent);
            componentManager.Components.Add(andComponent);
            componentManager.Step();
            componentManager.Step();
            componentManager.Step();
            
        }
    }
}
