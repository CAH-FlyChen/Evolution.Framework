using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CoreProfiler.Web;
using Evolution.Web.Extentions;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Evolution.Plugins.Abstract;
using Evolution.Framework;
using static Evolution.Framework.Jwt.SimpleTokenProvider;
using Microsoft.IdentityModel.Tokens;
using Evolution.Web.Middlewares;

namespace Evolution.Web
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
            //加载插件
            GloableConfiguration.PluginAssemblies =  pluginManager.LoadPluginAssembly(mvcBuilder);
            //cache
            services.AddEvolutionCacheService(Configuration);
            //session
            services.AddSession((SessionOptions options) =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".MyApplication";
            });

            //database
            services.AddEvolutionDBService(pluginManager,Configuration);
            //plugins injection entityframework and service dependency
            pluginManager.AddPluginEFService(Configuration);
            //inject 
            services.InjectEvolutionDependency();
            services.Add(new ServiceDescriptor(typeof(IConfiguration), Configuration));
            pluginManager.InjectEvolutionDependency();
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

            app.UseMiddleware<TransportMiddleware>();

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
                   
        }

    }
}


