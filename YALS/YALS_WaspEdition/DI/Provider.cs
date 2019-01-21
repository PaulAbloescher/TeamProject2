﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YALS_WaspEdition.Model.Component.Connection;
using YALS_WaspEdition.Model.Component.Manager;
using YALS_WaspEdition.Model.Component.Reflection;
using YALS_WaspEdition.Model.Reflection;
using YALS_WaspEdition.Model.Serialization;

namespace YALS_WaspEdition.DI
{
    public class Provider
    {
        static Provider()
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
            services.AddTransient<IComponentManager, ComponentManager>();
            services.AddTransient<IComponentLoaderController, ComponentLoaderController>();
            services.AddTransient<ICurrentStateSerializer, BinaryCurrentStateSerializer>();
            services.AddTransient<SerializationBinder, AssemblySerializationBinder>();
            Container = services.BuildServiceProvider();
        }
    }
}
