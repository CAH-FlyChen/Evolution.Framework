using Evolution.Plugin.Core;
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

namespace Evolution.Plugins.Demo
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
                if(config["DataBase"].ToLower()=="sqlserver")
                    options.UseSqlServer(
                        config.GetConnectionString("MDatabase"),
                        b => b.UseRowNumberForPaging()
                            );
                else if(config["DataBase"].ToLower() == "mysql")
                    options.UseMySQL(
                    config.GetConnectionString("MMysqlDatabase")
                        );
            });
            //TODO:此处可以添加以来注入，跨项目使用，或者接口和实现不再同一个类中时使用
            services.AddTransient<IDemoRepositroy,DemoRepository>();
            services.AddTransient<IDemoService, DemoService>();

        }
    }
}
