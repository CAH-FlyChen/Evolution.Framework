using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using Evolution.Framework;
using Newtonsoft.Json;
using System.Text;

namespace Evolution.Web.API.Middlewares
{
    public class ResourceFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private IDistributedCache dCache;
        private IMemoryCache mCache;
        public ResourceFilterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IDistributedCache dCache, IMemoryCache mCache)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ResourceFilterMiddleware>();
            this.dCache = dCache;
            this.mCache = mCache;
        }

        public async Task Invoke(HttpContext context)
        {
            bool r = false;
            _logger.LogInformation("Resource Filter request: " + context.Request.Path);
            string url = context.Request.Path.Value;
            string authStr = context.Request.Headers["Authorization"];
            //string token = Encoding.UTF8.GetString(Convert.FromBase64String(authStr.Split(' ')[1]));
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            
            Claim isSystemClaim = context.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.IsSystem);
            if(isSystemClaim != null)
            {
                if(Convert.ToBoolean(isSystemClaim.Value))
                {
                    r = true;
                    await _next.Invoke(context);
                }
                else
                {
                    string userName = context.User.Claims.SingleOrDefault(t => t.Type == ClaimTypes.Name).Value;
                    string authUrlStr = dCache.GetString(userName + "@AuthorizedUrl");
                    if (!string.IsNullOrEmpty(authUrlStr))
                    {
                        List<string> permissionIds = JsonConvert.DeserializeObject<List<string>>(authUrlStr);
                        if (permissionIds.Contains(Md5.md5(url, 16)))
                        {
                            r = true;
                            await _next.Invoke(context);
                        }
                    }
                }
            }

            if(!r)
            {
                context.Response.StatusCode = 401;
                _logger.LogInformation("Url:" + context.Request.Path + " access denied");
            }
            _logger.LogInformation("Finished handling Resource Filter request.");
        }
    }
}
