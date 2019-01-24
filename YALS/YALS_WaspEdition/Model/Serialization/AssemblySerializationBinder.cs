// ----------------------------------------------------------------------- 
// <copyright file="AssemblySerializationBinder.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the AssemblySerializationBinder class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Serialization
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the <see cref="AssemblySerializationBinder"/> class.
    /// </summary>
    public class AssemblySerializationBinder : SerializationBinder
    {
        /// <summary>
        /// When overridden in a derived class, controls the binding of a serialized object to a type.
        /// </summary>
        /// <param name="assemblyName">Specifies the assembly name of the serialized object.</param>
        /// <param name="typeName">Specifies the type name of the serialized object.</param>
        /// <returns>
        /// The type of the object the formatter creates a new instance of.
        /// </returns>
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type = null;

            string shortAssemblyName = assemblyName.Split(',')[0];

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                if (shortAssemblyName == assembly.FullName.Split(',')[0])
                {
                    type = assembly.GetType(typeName);
                    break;
                }
            }

            return type;
        }
    }
}
