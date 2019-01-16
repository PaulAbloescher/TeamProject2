using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.Model.Component.Connection;
using YALS_WaspEdition.Model.Component.Reflection;

namespace YALS_WaspEdition.DI
{
    public class Provider
    {
        public Provider()
        {
            Configure();
        }

        public static IServiceProvider Container
        {
            get;
            private set;
        }

        private static void Configure()
        {
            var services = new ServiceCollection();
            services.AddTransient<IConnection, Connection>();
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddTransient<IComponentLoader, ComponentLoader>();
            Container = services.BuildServiceProvider();
        }
    }
}
