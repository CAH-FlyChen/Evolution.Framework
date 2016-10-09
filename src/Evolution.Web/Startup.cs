using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Evolution.Application.SystemManage;
using Evolution.Application.SystemSecurity;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Domain.IRepository.SystemSecurity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text;
using Evolution.Repository.SystemManage;
using Evolution.Repository.SystemSecurity;
using Microsoft.AspNetCore.Authorization;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Plugin.Core;
using System.Reflection;
using System.Runtime.Loader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Evolution.Data.DBContext;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using SapientGuardian.MySql.Data.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;
using Evolution.Framework;

namespace Evolution.Web
{
    public class Startup
    {
        private static string WebRootPath = "";
        private readonly IHostingEnvironment _hostingEnvironment;
        private IList<IPluginInitializer> pluginInitializers;
        private readonly bool UseRedis = false;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            WebRootPath = env.WebRootPath;
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            
            Configuration = builder.Build();
            UseRedis = Configuration["Caching:UseRedis"].ToBool();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); 
        }

        public IConfigurationRoot Configuration { get; }
        public IApplicationBuilder App { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //解析外部插件
            services.LoadPluginAssessmblyToGlobalConfiguration(_hostingEnvironment);

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

            if(UseRedis)
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
            

            //services.AddDistributedMemoryCache();
            services.AddSession((SessionOptions options) =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".MyApplication";
            });

            //services.AddEntityFramework()
            //        .AddDbContext<EvolutionDbContext>(options =>
            //        {
            //            options.UseSqlServer(
            //                Configuration.GetConnectionString("MDatabase"),
            //                b => b.UseRowNumberForPaging()
            //                    );
            //        });
            services.AddEntityFramework()
            .AddDbContext<EvolutionDbContext>(options =>
            {
                options.UseMySQL(
                    Configuration.GetConnectionString("MMysqlDatabase")
                        );
             });

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

            pluginInitializers = services.InitPlugins(Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            App = app;
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "CookieAuth",
                LoginPath = new PathString("/Login/"),
                AccessDeniedPath = new PathString("/error.html"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true

            });
            app.UseMvc(routes =>
            {
                // Areas support
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //***  Initialize the DB ***//
            //if (env.EnvironmentName == "Development")
            //   InitializeBasicDb(app.ApplicationServices).Wait();

            InitTools.InitializeBasicDb(app.ApplicationServices,WebRootPath).Wait();

            //合并数据库
            foreach (var pluginInit in pluginInitializers)
            {
                pluginInit.DoMerage(app.ApplicationServices);
            }
        }

        

    }
}


