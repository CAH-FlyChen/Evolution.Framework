using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Web
{
    public class PluginViewLocationExpander:IViewLocationExpander
    {
        private const string _moduleKey = "module";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.ContainsKey(_moduleKey))
            {
                var module = context.Values[_moduleKey];
                if (!string.IsNullOrWhiteSpace(module))
                {
                    var moduleViewLocations = new string[]
                    {
                    "/Plugins/Evolution.Plugins." + module + "/Views/{1}/{0}.cshtml",
                    "/Plugins/Evolution.Plugins." + module + "/Views/Shared/{0}.cshtml"
                    };

                    viewLocations = moduleViewLocations.Concat(viewLocations);
                }
            }
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controller = context.ActionContext.ActionDescriptor.DisplayName;
            if (controller.Substring(0,13) == "Evolution.Web") return;
            var moduleName = controller.Split('.')[2];
            if (moduleName != "WebHost")//Evolution.Web
            {
                context.Values[_moduleKey] = moduleName;
            }
        }
    }
}
