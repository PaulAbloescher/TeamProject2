// <copyright file="Provider.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Provides the registered services.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.DI
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using YALS_WaspEdition.Model.Component.Connection;
    using YALS_WaspEdition.Model.Component.Manager;
    using YALS_WaspEdition.Model.Component.Reflection;
    using YALS_WaspEdition.Model.Reflection;
    using YALS_WaspEdition.Model.Serialization;

    /// <summary>
    /// Provides the registered services.
    /// </summary>
    public class Provider
    {
        /// <summary>
        /// Initializes static members of the <see cref="Provider"/> class.
        /// </summary>
        static Provider()
        {
            Configure();
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IServiceProvider Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
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
