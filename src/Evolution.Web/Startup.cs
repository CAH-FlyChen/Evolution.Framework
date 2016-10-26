using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Evolution.Application.SystemManage;

using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Evolution.Plugin.Core;
using System.Collections.Generic;
using CoreProfiler.Web;
using Evolution.Web.Extentions;

namespace Evolution.Web
{
    public class Startup
    {
        #region 私有变量
        private static string WebRootPath = "";
        private readonly IHostingEnvironment _hostingEnvironment;
        private IList<IPluginInitializer> pluginInitializers;
        private readonly bool UseRedis = false;
        private readonly string DataBase = null;
        #endregion
        public IConfigurationRoot Configuration { get; }
        public IApplicationBuilder App { get; set; }

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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); 
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            //解析外部插件
            services.LoadPluginAssessmblyToGlobalConfiguration(_hostingEnvironment);
            //mvc
            services.AddEvolutionMVC();
            //cache
            services.AddEvolutionCache(Configuration);
            //session
            services.AddSession((SessionOptions options) =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".MyApplication";
            });
            //database
            services.AddEvolutionDBService(Configuration);
            //inject 
            services.InjectEvolutionDependency(); 

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
            app.UseCoreProfiler(true);
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
            app.InitDbData(app.ApplicationServices, WebRootPath);
            //合并数据库
            foreach (var pluginInit in pluginInitializers)
            {
                pluginInit.DoMerage(app.ApplicationServices);
            }
        }

    }
}


