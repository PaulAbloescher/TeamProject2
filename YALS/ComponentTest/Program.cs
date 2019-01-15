using ComponentTest.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Component;
using OrComponent;

namespace ComponentTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var connectionManager = new ConnectionManager();
            var andComponent = new AndComponent();

            foreach (var input in andComponent.Inputs)
            {
                input.Value.Value = true;
            }

            var xorComp = new XORComponent();

            foreach (var input in xorComp.Inputs)
            {
                input.Value.Value = true;
            }

            xorComp.Inputs.ElementAt(0).Value = null;

            andComponent.Execute();
            xorComp.Execute();

            var orComponent = new OrComponent.OrComponent();

            orComponent.Inputs.ElementAt(0).Value.Value = true;

            orComponent.Execute();
        }
    }
}
