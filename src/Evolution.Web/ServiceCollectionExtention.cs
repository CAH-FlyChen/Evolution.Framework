using Evolution.Plugin.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Evolution.Web
{
    public static class ServiceCollectionExtention
    {
        public static void LoadPluginAssessmblyToGlobalConfiguration(this IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            IList<PluginInfo> plugins = new List<PluginInfo>();
            var moduleRootFolder = hostingEnvironment.ContentRootFileProvider.GetDirectoryContents("/Plugins");
            foreach (var moduleFolder in moduleRootFolder.Where(x => x.IsDirectory))
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.PhysicalPath, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }
                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            continue;
                        }
                        throw;
                    }
                    if (assembly.FullName.Contains(moduleFolder.Name))
                    {
                        plugins.Add(new PluginInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.PhysicalPath });
                    }
                }
            }
            GlobalConfiguration.Plugins = plugins;
        }

        public static void RegisterPluginController(this IMvcBuilder mvcBuilder,IList<PluginInfo> plugins)
        {
            foreach (var module in plugins)
            {
                // Register controller from modules
                mvcBuilder.AddApplicationPart(module.Assembly);
            }
        }

        public static IList<IPluginInitializer> InitPlugins(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            List<IPluginInitializer> pluginInitializers = new List<IPluginInitializer>();
            var moduleInitializerInterface = typeof(IPluginInitializer);
            foreach (var module in GlobalConfiguration.Plugins)
            {
                // Register dependency in modules
                var assTypes = module.Assembly.GetTypes();
                var moduleInitializerType = assTypes.Where(x => typeof(IPluginInitializer).IsAssignableFrom(x)).FirstOrDefault();
                if (moduleInitializerType != null && moduleInitializerType != typeof(IPluginInitializer))
                {
                    var moduleInitializer = (IPluginInitializer)Activator.CreateInstance(moduleInitializerType);
                    pluginInitializers.Add(moduleInitializer);
                    moduleInitializer.Init(services, Configuration);
                }
            }
            return pluginInitializers;
        }
    }
}
