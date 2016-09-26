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

namespace Evolution.Application.SystemManage
{
    public class RoleAuthorizeApp
    {
        #region 私有变量
        private IRoleAuthorizeRepository service = null;
        private MenuButtonApp moduleButtonApp = null;
        private IMemoryCache memoryCache;
        private MenuApp menuApp = null;
        private RoleApp roleApp = null;
        private HttpContext context = null;
        #endregion
        #region 构造函数
        public RoleAuthorizeApp(IRoleAuthorizeRepository service, MenuButtonApp moduleButtonApp, MenuApp menuApp,IMemoryCache _memoryCache,RoleApp roleApp,IHttpContextAccessor contextAccessor)
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
        public List<RoleAuthorizeEntity> GetListByObjectId(string ObjectId)
        {
            return service.IQueryable(t => t.ObjectId == ObjectId).ToList();
        }
        /// <summary>
        /// 通过角色Id获取该角色的资源授权
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissionIds">输出参数：逗号分隔的权限Id文本</param>
        /// <returns>角色对象</returns>
        public RoleEntity GetResoucesByRoleId(string roleId,out string permissionIds)
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
        public void Save(string roleId,List<string> resourceIds)
        {
            service.Delete(t => t.ObjectId == roleId && t.ObjectType == 1 && t.ItemType == 4);
            foreach(string resourceId in resourceIds)
            {
                RoleAuthorizeEntity entity = new RoleAuthorizeEntity();
                entity.AttachCreateInfo(context);
                entity.ItemId = resourceId;
                entity.ItemType = 4;
                entity.ObjectId = roleId;
                entity.ObjectType = 1;
                service.Insert(entity);
            }
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
        public List<string> GetResorucePermissionsByRoleId(string roleId)
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
        public static bool CheckPermission(AuthorizationHandlerContext ctx)
        {
            AuthorizationFilterContext res = (AuthorizationFilterContext)ctx.Resource;
            string url = res.HttpContext.Request.Path.Value;
            Claim isSystemClaim = ctx.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.IsSystem);
            if (isSystemClaim == null) return false;
            if (Convert.ToBoolean(isSystemClaim.Value)) return true;
            Claim permissionClaim = ctx.User.Claims.SingleOrDefault(t => t.Type == OperatorModelClaimNames.Permission);
            if (permissionClaim == null) return false;
            List<string> permissionIds = JsonConvert.DeserializeObject<List<string>>(permissionClaim.Value);
            if (permissionIds.Contains(Md5.md5(url, 16)))
                return true;
            return false;
        }


    }
}
