/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using Evolution.Framework;

namespace Evolution.Application.SystemManage
{
    public class MenuApp
    {
        private IMenuRepository service = null;
        private IRoleAuthorizeRepository roleAuthRepo = null;
        public MenuApp(IMenuRepository service,IRoleAuthorizeRepository roleAuthRepo)
        {
            this.service = service;
            this.roleAuthRepo = roleAuthRepo;
        }

        /// <summary>
        /// 根据角色获取菜单条目
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="context">context</param>
        /// <returns></returns>
        public List<MenuEntity> GetMenuListByRoleId(string roleId, HttpContext context)
        {
            var data = new List<MenuEntity>();
            var isSystem = context.User.Claims.FirstOrDefault(t => t.Type == OperatorModelClaimNames.IsSystem).Value;
            if (isSystem == null) return null;
            if (isSystem.ToBool())
            {
                data = this.GetList();
            }
            else
            {
                var menuData = this.GetList();
                var authmenudata = roleAuthRepo.IQueryable(t => t.ObjectId == roleId && t.ItemType == 1).ToList();
                foreach (var item in authmenudata)
                {
                    MenuEntity moduleEntity = menuData.Find(t => t.Id == item.ItemId);
                    if (moduleEntity != null)
                        data.Add(moduleEntity);
                }
            }
            return data.OrderBy(t => t.SortCode).ToList();
        }


        public List<MenuEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.SortCode).ToList();
        }
        public MenuEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.Id == keyValue);
            }
        }
        public void SubmitForm(MenuEntity moduleEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue, context);
                service.Update(moduleEntity);
            }
            else
            {
                moduleEntity.Create(context);
                service.Insert(moduleEntity);
            }
        }
    }
}
