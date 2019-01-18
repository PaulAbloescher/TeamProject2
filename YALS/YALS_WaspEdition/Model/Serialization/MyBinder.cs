using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YALS_WaspEdition.Model.Serialization
{
    public class MyBinder : SerializationBinder
    {
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
