using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.Entity.Extensions;
using Evolution.Data;
using System.Reflection;

namespace Evolution.Plugins.Abstract
{
    /// <summary>
    /// Evolution插件
    /// </summary>
    /// <typeparam name="T">Your DbContext</typeparam>
    public abstract class EvolutionPluginBase<T> : IEvolutionPlugin where T : DbContext
    {
        protected IServiceCollection Services { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 合并插件的数据结构
        /// </summary>
        /// <param name="applicationServices"></param>
        public void MeragePluginDataBase(IServiceProvider applicationServices)
        {
            try
            {
                this.ServiceProvider = applicationServices;
                var dbContext = applicationServices.GetService<T>();
                dbContext.Database.Migrate();
            }
            catch(Exception ex)
            {

            }

        }
        /// <summary>
        /// 初始化EF插件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public void AddPluginEFService(IServiceCollection services, IConfiguration config)
        {
            this.Services = services;
            services.AddEntityFramework()
            .AddDbContext<T>(options =>
            {
                if (config["DataBase"].ToLower() == "sqlserver")
                    options.UseSqlServer(
                        config.GetConnectionString("MDatabase"),
                        b => b.UseRowNumberForPaging()
                            );
                else if (config["DataBase"].ToLower() == "mysql")
                    options.UseMySQL(
                    config.GetConnectionString("MMysqlDatabase")
                        );
            });
        }
        /// <summary>
        /// 初始化服务依赖
        /// </summary>
        public abstract void InitDependenceInjection();
        /// <summary>
        /// 初始化插件数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="baseDbContext"></param>
        public virtual void InitPluginData(DbContext baseDbContext, IServiceProvider servicep, string webRootPath)
        {
            
        }
    }
}
