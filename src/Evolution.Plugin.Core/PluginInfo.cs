using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Evolution.Plugin.Core
{
    public class PluginInfo
    {
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
}
