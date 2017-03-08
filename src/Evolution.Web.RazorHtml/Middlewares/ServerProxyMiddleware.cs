using Evolution.IInfrastructure;
using JWT.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Evolution.Web.Middlewares
{
    public class ServerProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration config;
        public ServerProxyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ServerProxyMiddleware>();
            this.config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("服务器代理中间件处理Url: " + context.Request.Path);
            try
            {
                //判断访问是否是ajax,是否是非页面请求(是否有此controller)
                var reqCode = context.Request.Headers["X-Requested-With"].FirstOrDefault();
                if (reqCode == "XMLHttpRequest" &&
                    !context.Request.Path.ToString().Contains("CheckLoginJwt"))
                {
                    //如果访问的不是页面，则穿透访问
                    HttpHelper httpHelper = new HttpHelper(config);
                    var isNeedRefreshToken = this.needRefreshToken(context);
                    string token = context.Request.Cookies["access_token"];
                    Token newTokenObj = null;
                    string requestResult = string.Empty;
                    if (isNeedRefreshToken)
                    {
                        newTokenObj = await GetNewToken(context, token);
                        requestResult = httpHelper.GetResponseFromNewRequest(context.Request, newTokenObj.access_token).Result;
                        context.Response.Cookies.Append("access_token", newTokenObj.access_token);
                        context.Response.Cookies.Append("token_refresh_time", newTokenObj.expires_dt.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        requestResult = httpHelper.GetResponseFromNewRequest(context.Request, token).Result;
                    }
                    await context.Response.WriteAsync(requestResult);
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            catch(UnauthorizedAccessException e)
            {
                context.Response.Redirect("~/login");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("服务器代理中间件处理完成");
        }
        private bool needRefreshToken(HttpContext context)
        {
            string expDateTimeStr = context.Request.Cookies["token_refresh_time"];
            if (string.IsNullOrEmpty(expDateTimeStr))
            {
                return false;
            }
            else
            {
                //还剩5分钟的时候申请延长Token
                DateTime expTime = Convert.ToDateTime(expDateTimeStr);
                if (DateTime.Now > expTime)
                {
                    //已经过期无法申请
                    throw new UnauthorizedAccessException("Api token is 过期,需要重新登陆");
                }
                else if (DateTime.Now > expTime.AddMinutes(-5))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private async Task<Token> GetNewToken(HttpContext context,string token)
        {
            //用当前token申请刷新token
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.PostAsync(config["ApiServerBaseUrl"] + "/auth/refresh_token", null);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    Token tokenObj = JsonConvert.DeserializeObject<Token>(responseJson);
                    return tokenObj;
                }
                else
                {
                    throw new HttpRequestException("刷新 token API访问出现错误:HttpCode is " + response.StatusCode.ToString());
                }
            }
        }

    }
}
