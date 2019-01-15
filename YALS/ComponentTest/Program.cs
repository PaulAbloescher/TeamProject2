using ComponentTest.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using ComponentTest.Model.Component;

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

            andComponent.Execute();
        }
    }
}
