using Evolution.IInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Evolution.Web.Middlewares
{
    public class TransportMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration config;
        public TransportMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<TransportMiddleware>();
            this.config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Handling request: " + context.Request.Path);
            //判断访问是否是ajax,是否是非页面请求(是否有此controller)
            var reqCode = context.Request.Headers["X-Requested-With"].FirstOrDefault();
            if (reqCode == "XMLHttpRequest" && 
                !context.Request.Path.ToString().Contains("CheckLoginJwt"))
            {
                //如果访问的不是页面，则穿透访问
                HttpHelper httpHelper = new HttpHelper(config);
                var x = httpHelper.GetResponseFromNewRequest(context.Request).Result;
                await context.Response.WriteAsync(x);
            }
            else
            {
                await _next.Invoke(context);
            }

            _logger.LogInformation("Finished handling request.");
        }


    }
}
