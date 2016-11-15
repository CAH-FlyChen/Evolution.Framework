using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Evolution.Framework
{
    public class PluginAssembly
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Assembly Assembly { get; set; }
        public string SortName
        {
            get
            {
                return Name.Split('.').Last();
            }
        }
        public string Path { get; set; }
    }

    public class GloableConfiguration
    {
        private static List<PluginAssembly> assemblies = null;
        public static List<PluginAssembly> PluginAssemblies
        {
            get
            {
                if (assemblies == null)
                    assemblies = new List<PluginAssembly>();
                return assemblies;
            }
            set
            {
                assemblies = value;
            }
        }
    }
}
