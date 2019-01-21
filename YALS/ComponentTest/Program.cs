using ComponentTest.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Component;
using ComponentTest.Model.Component.Manager;
using ComponentTest.Model.Reflection;
using Components.Components;

namespace ComponentTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var componentManager = new ComponentManager();
            //var andComponent = new AndComponent();
            //var p1 = andComponent.Inputs.ElementAt(0);
            //var p2 = andComponent.Inputs.ElementAt(1);
            //var p3 = andComponent.Outputs.First();


            //var orComponent = new OrComponent();
            //var p4 = orComponent.Inputs.ElementAt(0);
            //var p5 = orComponent.Inputs.ElementAt(1);
            //var p6 = orComponent.Outputs.First();

            //componentManager.Connect(andComponent, orComponent, p3, p4);

            //p1.Value.Current = true;
            //p2.Value.Current = true;
            //p5.Value.Current = false;

            //componentManager.Components.Add(orComponent);
            //componentManager.Components.Add(andComponent);
            //componentManager.Step();
            //componentManager.Step();
            //componentManager.Step();

            //IEnumerable<string> paths = new List<string>() { @"C: \Users\Paul\Documents\FHWN\Semester 3\TeamProject2\YALS\Components\bin\Debug" };
            //ComponentLoader loader = new ComponentLoader();

            //var dict = loader.Load(paths);
            //var test = "";

            LEDComponent lEDComponent = new LEDComponent();

            Console.WriteLine();
        }
    }
}
