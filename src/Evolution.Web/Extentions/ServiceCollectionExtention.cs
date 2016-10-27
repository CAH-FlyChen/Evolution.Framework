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
using System.Data.SqlClient;
using Evolution.Application.SystemSecurity;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Domain.IRepository.SystemSecurity;
using Evolution.Repository.SystemManage;
using Evolution.Repository.SystemSecurity;
using Evolution.Application.SystemManage;
using Microsoft.AspNetCore.Authorization;
using Evolution.Plugin.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Evolution.Web.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static void AddEvolutionMVC(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc(opts =>
            {
                Func<AuthorizationHandlerContext, bool> handler = RoleAuthorizeApp.CheckPermission;
                opts.Filters.Add(new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAssertion(handler).Build()));
            });

            //自定义路径解析View
            mvcBuilder.AddRazorOptions(opt =>
            {
                opt.ViewLocationExpanders.Add(new PluginViewLocationExpander());
            });
            //解析构造外部插件
            mvcBuilder.RegisterPluginController(GlobalConfiguration.Plugins);
        }
        public static void AddEvolutionCache(this IServiceCollection services, IConfigurationRoot Configuration)
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
        public static void AddEvolutionDBService(this IServiceCollection services, IConfigurationRoot Configuration)
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
        public static void InjectEvolutionDependency(this IServiceCollection services)
        {
            #region 注册Service
            services.AddLogging();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IItemsDetailRepository, ItemsDetailRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IMenuButtonRepository, MenuButtonRepository>();
            services.AddTransient<IOrganizeRepository, OrganizeRepository>();
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
            services.AddTransient<AreaApp>();
            services.AddTransient<MenuButtonApp>();
            services.AddTransient<RoleAuthorizeApp>();
            services.AddTransient<UserLogOnApp>();
            services.AddTransient<UserApp>();
            services.AddTransient<LogApp>();
            services.AddTransient<ItemsDetailApp>();
            services.AddTransient<ItemsApp>();
            services.AddTransient<OrganizeApp>();
            services.AddTransient<RoleApp>();
            services.AddTransient<DutyApp>();
            services.AddTransient<RoleAuthorizeApp>();
            services.AddTransient<DbBackupApp>();
            services.AddTransient<FilterIPApp>();
            //services.AddTransient<PermissionApp>();
            services.AddTransient<MenuApp>();
            services.AddTransient<ResourceApp>();
            #endregion
        }
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
        public static void RegisterPluginController(this IMvcBuilder mvcBuilder, IList<PluginInfo> plugins)
        {
            foreach (var module in plugins)
            {
                // Register controller from modules
                mvcBuilder.AddApplicationPart(module.Assembly);
            }
        }
        public static void InitPlugins(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            var moduleInitializerInterface = typeof(IPluginInitializer);
            foreach (var module in GlobalConfiguration.Plugins)
            {
                // Register dependency in modules
                var assTypes = module.Assembly.GetTypes();
                var moduleInitializerType = assTypes.Where(x => typeof(IPluginInitializer).IsAssignableFrom(x)).FirstOrDefault();
                if (moduleInitializerType != null && moduleInitializerType != typeof(IPluginInitializer))
                {
                    var moduleInitializer = (IPluginInitializer)Activator.CreateInstance(moduleInitializerType);
                    module.Initializer = moduleInitializer;
                    moduleInitializer.Init(services, Configuration);
                }
            }
        }
        public static DbConnection GetDbConnection(IConfigurationRoot Configuration,string DataBase)
        {
            if (DataBase.ToLower() == "sqlserver")
            {
                return new ProfiledDbConnection(new SqlConnection(Configuration.GetConnectionString("MDatabase")), () => {
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
    }
}
