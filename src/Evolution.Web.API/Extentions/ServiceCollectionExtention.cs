using CoreProfiler;
using CoreProfiler.Data;
using Evolution.Data;
using Evolution.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using MySQL.Data.Entity.Extensions;
using System.Data.Common;
using Evolution.Application.SystemSecurity;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Domain.IRepository.SystemSecurity;
using Evolution.Repository.SystemManage;
using Evolution.Repository.SystemSecurity;
using Evolution.Application.SystemManage;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using Evolution.Plugins.Abstract;
using Evolution.Application;
using Microsoft.AspNetCore.Http;

namespace Evolution.Web.API.Extentions
{
    public static class ServiceCollectionExtention
    {
        /// <summary>
        /// 添加Mvc服务
        /// </summary>
        /// <param name="services"></param>
        public static IMvcBuilder AddEvolutionMVCService(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc(opts =>
            {
                Func<AuthorizationHandlerContext, bool> handler = RoleAuthorizeService.CheckPermissionNew;
                opts.Filters.Add(new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAssertion(handler).Build()));
            });

            //自定义路径解析View
            mvcBuilder.AddRazorOptions(opt =>
            {
                opt.ViewLocationExpanders.Add(new PluginViewLocationExpander());
            });
            return mvcBuilder;
        }
        /// <summary>
        /// 添加缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddEvolutionCacheService(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            bool useRedis = Configuration["Caching:UseRedis"].ToBool();
            if (useRedis)
            {
                services.AddDistributedRedisCache(option => {
                    option.Configuration = Configuration.GetConnectionString("RedisCache");
                    option.InstanceName = "master";
                });
            }
            else
            {
                services.AddMemoryCache();
            }
        }
        /// <summary>
        /// 添加数据库服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddEvolutionDBService(this IServiceCollection services, EvolutionPluginManager manager, IConfigurationRoot Configuration)
        {
            string DataBase = Configuration["DataBase"];
            if (DataBase.ToLower() == "sqlserver")
            {
                services.AddEntityFramework()
                .AddDbContext<EvolutionDBContext>(options =>
                {
                    options.UseSqlServer(
                        GetDbConnection(Configuration,DataBase),
                        b => b.UseRowNumberForPaging()
                            );
                });
            }
            else if (DataBase.ToLower() == "mysql")
            {
                services.AddEntityFramework()
                .AddDbContext<EvolutionDBContext>(options =>
                {
                    options.UseMySQL(
                        GetDbConnection(Configuration,DataBase)
                            );
                });
            }
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void InjectEvolutionDependency(this IServiceCollection services)
        {
            #region 注册Service
            services.AddLogging();

            //services.AddSingleton<ClaimListProvider, ClaimListProvider>();

            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IItemsDetailRepository, ItemsDetailRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IMenuButtonRepository, MenuButtonRepository>();
            services.AddTransient<IRoleAuthorizeRepository, RoleAuthorizeRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUserLogOnRepository, UserLogOnRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDbBackupRepository, DbBackupRepository>();
            //services.AddTransient<ILogger, Logger>();
            services.AddTransient<IFilterIPRepository, FilterIPRepository>();
            //services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IOrganizeRepository, OrganizeRepository>();
            services.AddTransient<IPluginRepository, PluginRepository>();
            services.AddTransient<ITenantRepository, TenantRepository>();

            services.AddTransient<MenuButtonService>();
            services.AddTransient<RoleAuthorizeService>();
            services.AddTransient<UserLogOnService>();
            services.AddTransient<UserService>();
            services.AddTransient<LogService>();
            services.AddTransient<ItemsDetailService>();
            services.AddTransient<ItemsService>();
            services.AddTransient<RoleService>();
            services.AddTransient<DutyService>();
            services.AddTransient<RoleAuthorizeService>();
            services.AddTransient<DbBackupService>();
            services.AddTransient<FilterIPService>();
            //services.AddTransient<PermissionApp>();
            services.AddTransient<MenuService>();
            services.AddTransient<ResourceService>();
            services.AddTransient<OrganizeService>();
            services.AddTransient<PluginService>();
            services.AddTransient<ITenantService,TenantService>();
            #endregion
        }

        /*
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="Configuration"></param>
        /// <param name="DataBase"></param>
        /// <returns></returns>
        public static DbConnection GetDbConnection(IConfigurationRoot Configuration,string DataBase)
        {
            if (DataBase.ToLower() == "sqlserver")
            {
                return new ProfiledDbConnection(new System.Data.SqlClient.SqlConnection(Configuration.GetConnectionString("MDatabase")), () => {
                    if (ProfilingSession.Current == null)
                        return null;
                    return new DbProfiler(ProfilingSession.Current.Profiler);
                });

            }
            else if (DataBase.ToLower() == "mysql")
            {
                return new ProfiledDbConnection(new MySqlConnection(Configuration.GetConnectionString("MMysqlDatabase")), () => {
                    if (ProfilingSession.Current == null)
                        return null;
                    return new DbProfiler(ProfilingSession.Current.Profiler);
                });
            }
            else
            {
                return null;
            }
        }
        */
        public static DbConnection GetDbConnection(IConfigurationRoot Configuration, string DataBase)
        {
            if (DataBase.ToLower() == "sqlserver")
            {
                return new System.Data.SqlClient.SqlConnection(Configuration.GetConnectionString("MDatabase"));

            }
            else if (DataBase.ToLower() == "mysql")
            {
                return new MySqlConnection(Configuration.GetConnectionString("MMysqlDatabase"));
            }
            else
            {
                return null;
            }
        }
    }
}
