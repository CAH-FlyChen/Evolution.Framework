using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugin.Core
{
    public interface IPluginInitializer
    {
        void Init(IServiceCollection serviceCollection, IConfiguration config);
        void DoMerage(IServiceProvider applicationServices);
    }


         
}
