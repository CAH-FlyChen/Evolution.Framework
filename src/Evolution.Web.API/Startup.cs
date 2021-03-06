﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CoreProfiler.Web;
using Evolution.Web.API.Extentions;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Evolution.Plugins.Abstract;
using Evolution.Framework;
using static Evolution.Framework.Jwt.SimpleTokenProvider;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Evolution.Web.API
{
    public class Startup
    {
        #region 私有变量
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion
        public IConfigurationRoot Configuration { get; }
        public IApplicationBuilder App { get; set; }

        EvolutionPluginManager pluginManager = null;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            pluginManager = new EvolutionPluginManager(_hostingEnvironment, services);

            services.AddApplicationInsightsTelemetry(Configuration);
            
            //mvc
            var mvcBuilder = services.AddEvolutionMVCService();
            //配置跨域
            services.AddCors();

            //加载插件
            GloableConfiguration.PluginAssemblies =  pluginManager.LoadPluginAssembly(mvcBuilder);
            //cache
            services.AddEvolutionCacheService(Configuration);

            //database
            services.AddEvolutionDBService(pluginManager,Configuration);
            //plugins injection entityframework and service dependency
            pluginManager.AddPluginEFService(Configuration);
            //inject 
            services.InjectEvolutionDependency();
            pluginManager.InjectEvolutionDependency();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            App = app;
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");
            ILogger _logger = loggerFactory.CreateLogger("MyApplication.Startup");

            _logger.LogInformation("系统启动【开始】");
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

            app.UseCors(policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //提供jwttoken服务
            app.GenJWTEndpoint(Configuration);
            //配置token验证
            app.ConfigureJwtAuth(Configuration);

            //app.UseMiddleware<ResourceFilterMiddleware>();
            app.UseCoreProfiler(true);

            app.UseMvc(routes =>
            {
                // Areas support
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



            //***  Initialize the DB ***//
            //if (env.EnvironmentName == "Development")
            //   InitializeBasicDb(app.ApplicationServices).Wait();
            //将插件数据写入数据库
            EvolutionDBContext dbContext = app.ApplicationServices.GetService<EvolutionDBContext>();
            _logger.LogInformation("初始化测试数据【开始】");
            app.InitFreameworkDbData(app.ApplicationServices, env.WebRootPath, dbContext);
            _logger.LogInformation("初始化测试数据【完成】");
            //add plugin info to MainDb
            foreach (var p in pluginManager.PluginAssemblies)
            {
                _logger.LogInformation(string.Format("开始处理插件[{0}]数据库",p.Name));
                try
                {
                    var exist = dbContext.Plugins.CountAsync(t => t.Id == p.Id).Result;
                    if (exist == 0)
                    {
                        dbContext.Add(p);
                    }
                }
                catch
                {
                    continue;
                }
            }
            _logger.LogInformation("合并插件数据库结构到主数据库");
            //exec db
            pluginManager.MeragePluginDBStruct(app.ApplicationServices);
            _logger.LogInformation("初始化插件数据");
            pluginManager.InitPluginData(dbContext, app.ApplicationServices,env.WebRootPath);
            dbContext.SaveChanges();
            _logger.LogInformation("系统启动【完成】");
        }

    }
}


