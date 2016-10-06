using Evolution.Plugin.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modular.Modules.ModuleB.Module;
using Modular.Modules.ModuleB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleB
{
    public class ModuleInitializer : IPluginInitializer
    {
        public void DoMerage(IServiceProvider applicationServices)
        {
            using (var dbContext = applicationServices.GetService<ModuleBContext>())
            {
                var sqlServerDatabase = dbContext.Database;
                sqlServerDatabase.Migrate();
            }
        }

        public void Init(IServiceCollection services,IConfiguration config)
        {
            services.AddEntityFramework()
            .AddDbContext<ModuleBContext>(options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("MDatabase"),
                    b => b.UseRowNumberForPaging()
                        );
            });

            //services.AddTransient<DemoRepository>();
            services.AddTransient<IDemoService, DemoService>();

        }
    }
}
