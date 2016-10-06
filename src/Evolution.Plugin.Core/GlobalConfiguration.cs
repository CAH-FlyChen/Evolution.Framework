using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugin.Core
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            Plugins = new List<PluginInfo>();
        }

        public static IList<PluginInfo> Plugins { get; set; }
    }
}
