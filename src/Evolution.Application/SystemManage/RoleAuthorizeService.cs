/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Evolution.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace Evolution.Application.SystemManage
{
    public class RoleAuthorizeService : IRoleAuthorizeService
    {
        #region 私有变量
        private IRoleAuthorizeRepository service = null;
        private MenuButtonService moduleButtonApp = null;
        private IMemoryCache memoryCache;
        private MenuService menuApp = null;
        private RoleService roleApp = null;
        private HttpContext context = null;
        #endregion
        #region 构造函数
        public RoleAuthorizeService(IRoleAuthorizeRepository service, MenuButtonService moduleButtonApp, MenuService menuApp,IMemoryCache _memoryCache,RoleService roleApp,IHttpContextAccessor contextAccessor)
        {
            this.service = service;
            this.moduleButtonApp = moduleButtonApp;
            this.memoryCache = _memoryCache;
            this.menuApp = menuApp;
            this.roleApp = roleApp;
            this.context = contextAccessor.HttpContext;
        }
        #endregion
        /// <summary>
        /// 根据权限所有者对象id获取所有授权对象
        /// </summary>
        /// <param name="ObjectId">权限所有者对象Id（OwnerId）</param>
        /// <returns>授权对象</returns>
        public Task<List<RoleAuthorizeEntity>> GetListByObjectId(string ObjectId)
        {
            return service.IQueryable(t => t.ObjectId == ObjectId).ToListAsync();
        }
        /// <summary>
        /// 通过角色Id获取该角色的资源授权
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissionIds">输出参数：逗号分隔的权限Id文本</param>
        /// <returns>角色对象</returns>
        public Task<RoleEntity> GetResoucesByRoleId(string roleId,out string permissionIds)
        {
            List<string> r = service.IQueryable(t => t.ItemType == 4 && t.ObjectType == 1 && t.ObjectId == roleId).Select(t=>t.ItemId).ToList();
            permissionIds = String.Join(",", r);
            return roleApp.GetRoleById(roleId);
        }
        /// <summary>
        /// 保存角色资源授权
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="resourceIds">资源Ids</param>
        public async Task<int> Save(string roleId,List<string> resourceIds)
        {
            await service.DeleteAsync(t => t.ObjectId == roleId && t.ObjectType == 1 && t.ItemType == 4);
            foreach(string resourceId in resourceIds)
            {
                RoleAuthorizeEntity entity = new RoleAuthorizeEntity();
                entity.AttachCreateInfo(context);
                entity.ItemId = resourceId;
                entity.ItemType = 4;
                entity.ObjectId = roleId;
                entity.ObjectType = 1;
                await service.InsertAsync(entity);
            }
            return await Task.FromResult(0);
        }

        //public bool ActionValidate(string roleId, string moduleId, string action)
        //{
        //    var authorizeurldata = new List<AuthorizeActionModel>();
        //    var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
        //    if (cachedata == null)
        //    {
        //        var moduledata = moduleApp.GetList();
        //        var buttondata = moduleButtonApp.GetList();
        //        var authorizedata = service.IQueryable(t => t.ObjectId == roleId).ToList();
        //        foreach (var item in authorizedata)
        //        {
        //            if (item.ItemType == 1)
        //            {
        //                ModuleEntity moduleEntity = moduledata.Find(t => t.Id == item.ItemId);
        //                authorizeurldata.Add(new AuthorizeActionModel { Id = moduleEntity.Id, UrlAddress = moduleEntity.UrlAddress });
        //            }
        //            else if (item.ItemType == 2)
        //            {
        //                ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.Id == item.ItemId);
        //                authorizeurldata.Add(new AuthorizeActionModel { Id = moduleButtonEntity.ModuleId, UrlAddress = moduleButtonEntity.UrlAddress });
        //            }
        //        }
        //        CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
        //    }
        //    else
        //    {
        //        authorizeurldata = cachedata;
        //    }
        //    authorizeurldata = authorizeurldata.FindAll(t => t.Id.Equals(moduleId));
        //    foreach (var item in authorizeurldata)
        //    {
        //        if (!string.IsNullOrEmpty(item.UrlAddress))
        //        {
        //            string[] url = item.UrlAddress.Split('?');
        //            if (item.Id == moduleId && url[0] == action)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        /// <summary>
        /// 根据角色id获取权限ID
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>资源权限列表</returns>
        public Task<List<string>> GetResorucePermissionsByRoleId(string roleId)
        {
            return service.GetResorucePermissionsByRoleId(roleId);
        }
/// <summary>
        /// 检测资源（Resource）的权限。
        /// 当访问资源的时候，会通过此方法来判断是否具有权限。
        /// 如果是系统管理员则自动获取到所有权限
        /// </summary>
        /// <param name="ctx">context</param>
        /// <returns>是否通过</returns>
        [Obsolete]
        public static bool CheckPermission(AuthorizationHandlerContext ctx)
        {
            AuthorizationFilterContext res = (AuthorizationFilterContext)ctx.Resource;
            string url = res.HttpContext.Request.Path.Value;
            Claim isSystemClaim = ctx.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.IsSystem);
            if (isSystemClaim != null && Convert.ToBoolean(isSystemClaim.Value)) return true;
            Claim permissionClaim = ctx.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.Permission);
            
            if (permissionClaim == null) return false;
            List<string> permissionIds = JsonConvert.DeserializeObject<List<string>>(permissionClaim.Value);
            if (permissionIds.Contains(Md5.md5(url, 16)))
                return true;
            return false;
        }

        public static bool CheckPermissionNew(AuthorizationHandlerContext ctx)
        {
            AuthorizationFilterContext res = (AuthorizationFilterContext)ctx.Resource;
            var context = res.HttpContext;
            
            var loggerFactory = (ILoggerFactory)context.RequestServices.GetService(typeof(ILoggerFactory));
            ILogger _logger = loggerFactory.CreateLogger("Permission Check");
            IDistributedCache dCache = (IDistributedCache)context.RequestServices.GetService(typeof(IDistributedCache));
            bool r = false;
            _logger.LogInformation("Resource Filter request: " + context.Request.Path);
            string url = context.Request.Path.Value;
            string authStr = context.Request.Headers["Authorization"];
            //string token = Encoding.UTF8.GetString(Convert.FromBase64String(authStr.Split(' ')[1]));
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            Claim isSystemClaim = context.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.IsSystem);
            if (isSystemClaim != null)
            {
                if (Convert.ToBoolean(isSystemClaim.Value))
                {
                    r = true;
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
                        }
                    }
                }
            }

            if (!r)
            {
                context.Response.StatusCode = 401;
                _logger.LogInformation("Customize Resource Filter Url:" + context.Request.Path + " access denied.Return 401");
            }
            _logger.LogInformation("Finished handling Resource Filter request.");
            return r;
        }
    }
}
