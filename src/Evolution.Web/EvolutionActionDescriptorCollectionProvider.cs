//using Evolution.Plugin.Core;
//using Microsoft.AspNetCore.Mvc.Abstractions;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Evolution.Web
//{
//    public class EvolutionActionDescriptorCollectionProvider: IActionDescriptorCollectionProvider
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private ActionDescriptorCollection _collection;

//        public EvolutionActionDescriptorCollectionProvider(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        /// <summary>
//        /// Returns a cached collection of <see cref="ActionDescriptor" />.
//        /// </summary>
//        public ActionDescriptorCollection ActionDescriptors
//        {
//            get
//            {
//                if (_collection == null|| GlobalConfiguration.RefreshActionDescriptors)
//                {
//                    _collection = GetCollection();
//                    if(GlobalConfiguration.RefreshActionDescriptors)
//                    {
//                        GlobalConfiguration.RefreshActionDescriptors = false;
//                    }
//                }

//                return _collection;
//            }
//        }

//        private ActionDescriptorCollection GetCollection()
//        {
//            var providers =
//                _serviceProvider.GetServices<IActionDescriptorProvider>()
//                                .OrderBy(p => p.Order)
//                                .ToArray();

//            var context = new ActionDescriptorProviderContext();

//            foreach (var provider in providers)
//            {
//                provider.OnProvidersExecuting(context);
//            }

//            for (var i = providers.Length - 1; i >= 0; i--)
//            {
//                providers[i].OnProvidersExecuted(context);
//            }

//            return new ActionDescriptorCollection(
//                new ReadOnlyCollection<ActionDescriptor>(context.Results), 0);
//        }
//    }
//}
