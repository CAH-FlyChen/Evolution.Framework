//using Microsoft.Extensions.FileProviders;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Internal;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using Microsoft.AspNetCore.Hosting;

//namespace Evolution.Plugin.Core
//{
//    public class CustomDirectoryAssemblyProvider
//    {
//        //private readonly IFileProvider _fileProvider;

//        //public CustomDirectoryAssemblyProvider(
//        //        IFileProvider fileProvider)
//        //{
//        //    _fileProvider = fileProvider;

//        //}
        
//        public static ApplicationPartManager GetApplicationPartManager(IServiceCollection services)
//        {
//            var manager = GetServiceFromCollection<ApplicationPartManager>(services);
//            if (manager == null)
//            {
//                manager = new ApplicationPartManager();

//                var environment = GetServiceFromCollection<IHostingEnvironment>(services);
//                if (environment == null)
//                {
//                    return manager;
//                }

//                var parts = DefaultAssemblyPartDiscoveryProvider.DiscoverAssemblyParts(environment.ApplicationName);
//                foreach (var part in parts)
//                {
//                    manager.ApplicationParts.Add(part);
//                }
//            }

//            return manager;
//        }

//        private static T GetServiceFromCollection<T>(IServiceCollection services)
//        {
//            return (T)services
//                .FirstOrDefault(d => d.ServiceType == typeof(T))
//                ?.ImplementationInstance;
//        }

//    }
//}
