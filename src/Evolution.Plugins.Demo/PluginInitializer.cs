using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySQL.Data.Entity.Extensions;
using Evolution.Plugins.Demo.Repos;
using Evolution.Plugins.Demo.Services;
using Evolution.Plugins.Demo.Modules;
using Evolution.Plugins.Abstract;

namespace Evolution.Plugins.Demo
{
    public class PluginInitializer : EvolutionPluginBase<ModuleBContext>
    {
        public override void InitDependenceInjection()
        {
            Services.AddTransient<IDemoRepositroy, DemoRepository>();
            Services.AddTransient<IDemoService, DemoService>();
        }
    }
}
