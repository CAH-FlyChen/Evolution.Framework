using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;
using System.Security.Principal;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace JWT.Common.Middlewares.TokenProvider
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private UserService userApp;
        private RoleService roleApp;
        private RoleAuthorizeService roleAuth;
        private IDistributedCache dCache;
        private IMemoryCache mCache;
        private IConfigurationRoot configuration;
        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,UserService uapp,RoleService r,
            RoleAuthorizeService ra, IDistributedCache dCache,IMemoryCache mCache)
        {
            _next = next;
            _options = options.Value;
            userApp = uapp;
            roleApp = r;
            roleAuth = ra;
            this.dCache = dCache;
            this.mCache = mCache;
            this.configuration = options.Value.config;        
        }    

        /// <summary>
        /// invoke the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {   
                   
            if (context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                //get token 
                if (!context.Request.Method.Equals("POST")
                   || !context.Request.HasFormContentType)
                {
                    await ReturnBadRequest(context);
                }

                await GenerateAuthorizedResult(context);
            }
            else if(context.Request.Path.Equals(_options.RefreshTokenPath, StringComparison.Ordinal))
            {
                //refresh token
                if (!context.Request.Method.Equals("POST"))
                {
                    await ReturnBadRequest(context);
                }

                var handler = new JwtSecurityTokenHandler();
                var audience = configuration["Jwt:Audience:Name"];
                var issuer = configuration["Jwt:Issuer"];
                var symmetricKeyAsBase64 = configuration["Jwt:Audience:Secret"];
                var keyByteArray = System.Text.Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
                var signingKey = new SymmetricSecurityKey(keyByteArray);
                ClaimsPrincipal principal = null;
                SecurityToken validToken = null;
                string authorizationStr = context.Request.Headers["Authorization"];
                var protectedText = authorizationStr.Split(' ')[1];
                var tokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = audience,

                    // Validate the token expiry
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };

                principal = handler.ValidateToken(protectedText, tokenValidationParameters, out validToken);

                var validJwt = validToken as JwtSecurityToken;

                if (validJwt == null)
                {
                    throw new ArgumentException("Invalid JWT");
                }

                // if more than 14 days old, force login
                DateTime dtNow = DateTime.Now;
                TimeSpan ts = validJwt.ValidTo.ToLocalTime() - dtNow;
                //提前10分钟可以刷新，给予延期
                if ( ts.TotalMinutes < 10)
                {
                    var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: validJwt.Claims,
                    notBefore: dtNow,
                    expires: dtNow.Add(_options.Expiration),
                    signingCredentials: _options.SigningCredentials);
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    var response = new
                    {
                        access_token = encodedJwt,
                        expires_in = ((int)_options.Expiration.TotalSeconds).ToString(),
                        expires_dt = dtNow.Add(_options.Expiration).ToLocalTime(),
                        token_type = "Bearer",
                    };

                    string r = JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(r);
                }
                else
                {
                    throw (new Exception("no need to refresh token.limit 10 minutes."));
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// get the result of authorized
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GenerateAuthorizedResult(HttpContext context)
        {         
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];
            var tenantId = context.Request.Form["tid"];

            var identity = await GetIdentity(username, password,tenantId);
            if (identity == null)
            {
                await ReturnBadRequest(context);
                return;
            }            
            
            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(GetJwt(username, identity));
        }

        /// <summary>
        /// validate the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string username, string password, string tenantId)
        {
           UserEntity u = userApp.CheckLogin(username, password,tenantId).Result;
            if(u!=null)
            {
                var r = roleApp.GetRoleById(u.RoleId,tenantId).Result;

                //ClaimsIdentity identity = new ClaimsIdentity("local");
                ////system
                //identity.AddClaim(new Claim(ClaimTypes.Name, u.Account));
                //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, u.Id));
                //identity.AddClaim(new Claim(ClaimTypes.Role, r.FullName));
                ////custom
                //identity.AddClaim(new Claim(OperatorModelClaimNames.RoleId, r.Id));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.IsSystem, (u.Account.ToLower() == "admin").ToString()));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.CompanyId, u.DepartmentId));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.DepartmentId, om.DepartmentId));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.IsSystem, om.IsSystem.ToString()));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddress, om.LoginIPAddress));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginIPAddressName, om.LoginIPAddressName));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginTime, om.LoginTime.ToString("yyyy-MM-dd HH:mm:ss")));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.LoginToken, om.LoginToken));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.UserCode, om.UserCode));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.UserId, om.UserId));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.UserName, r.));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.RoleName, om.RoleName));
                //identity.AddClaim(new Claim(OperatorModelClaimNames.Permission, JsonConvert.SerializeObject(roleAuth.GetResorucePermissionsByRoleId(om.RoleId))));
                List<string> urls = roleAuth.GetResorucePermissionsByRoleId(u.RoleId,tenantId).Result;
                string urlStr = JsonConvert.SerializeObject(urls);
                string k = u.Account + "@AuthorizedUrl";
                if (dCache!=null)
                {
                    dCache.Set(k, urlStr.ToBytes());
                }
                else if(mCache!=null)
                {
                    mCache.Set(k, urlStr);
                }
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), 
                    new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, u.Id),
                    new Claim(ClaimTypes.Role, r.FullName),
                    new Claim(OperatorModelClaimNames.RoleId, r.Id),
                    new Claim(OperatorModelClaimNames.IsSystem, (u.Account.ToLower() == "admin").ToString())
                }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }

        /// <summary>
        /// return the bad request (400)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnBadRequest(HttpContext context)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = "invalid_grant",
                error_description = "Audience validation failed"
            }));
        }

        /// <summary>
        /// get the jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GetJwt(string username,ClaimsIdentity ci)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64),
                
            };
            foreach(var c in ci.Claims)
            {
                claims.Add(c);
            }

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                expires_dt = now.Add(_options.Expiration).ToLocalTime(),
                token_type = "Bearer",
                user_name = ci.FindFirst(ClaimTypes.Name).Value,
                user_code = ci.FindFirst(ClaimTypes.NameIdentifier).Value,
                role_name = ci.FindFirst(ClaimTypes.Role).Value
            };            

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

    }
}
