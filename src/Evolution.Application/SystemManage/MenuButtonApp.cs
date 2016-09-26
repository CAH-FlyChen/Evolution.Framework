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
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemManage
{
    public class MenuButtonApp
    {
        #region 私有变量
        private IMenuButtonRepository menuButtonRepo = null;
        private IRoleAuthorizeRepository roleAuthRepo = null;
        private HttpContext context = null;
        #endregion
        #region 构造函数
        public MenuButtonApp(IMenuButtonRepository menuRepo, IRoleAuthorizeRepository roleAuthRepo,IHttpContextAccessor accessor)
        {
            this.menuButtonRepo = menuRepo;
            this.roleAuthRepo = roleAuthRepo;
            this.context = accessor.HttpContext;
        }
        #endregion
        /// <summary>
        /// 根据角色获取表单按钮列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>菜单按钮对象</returns>
        public List<MenuButtonEntity> GetButtonListByRoleId(string roleId)
        {
            var data = new List<MenuButtonEntity>();
            var isSystem = context.User.Claims.First(t => t.Type == OperatorModelClaimNames.IsSystem).Value;
            if (isSystem.ToBool())
            {
                data = this.GetList();
            }
            else
            {
                var buttondata = this.GetList();
                //获取授权过的按钮
                var authorizedata = roleAuthRepo.IQueryable(t => t.ObjectId == roleId && t.ItemType == 2).ToList();
                foreach (var item in authorizedata)
                {
                    MenuButtonEntity menuButtonEntity = buttondata.Find(t => t.Id == item.ItemId);
                    if (menuButtonEntity != null)
                    {
                        data.Add(menuButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 通过菜单Id获取表单按钮对象列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>表单按钮对象</returns>
        public List<MenuButtonEntity> GetListByMenuId(string menuId)
        {
            //var expression = ExtLinq.True<MenuButtonEntity>();
            //if (!string.IsNullOrEmpty(menuId))
            //{
            //    expression = expression.And(t => t.MenuId == menuId);
            //}
            //return menuButtonRepo.IQueryable(expression).OrderBy(t => t.SortCode).ToList();
            if(menuId=="")
            {
                return menuButtonRepo.IQueryable().OrderBy(x => x.SortCode).ToList();
            }
            else
            {
                return menuButtonRepo.IQueryable(t => t.MenuId == menuId).OrderBy(x => x.SortCode).ToList();
            }

        }
        /// <summary>
        /// 获取所有表单按钮对象
        /// </summary>
        /// <returns>表单按钮对象列表</returns>
        public List<MenuButtonEntity> GetList()
        {
            return GetListByMenuId("");
        }
        /// <summary>
        /// 通过Id获取表单按钮对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuButtonEntity GetItemById(string id)
        {
            return menuButtonRepo.FindEntity(id);
        }
        public void DeleteItem(string id)
        {
            if (menuButtonRepo.IQueryable().Count(t => t.ParentId.Equals(id)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                menuButtonRepo.Delete(t => t.Id == id);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menuButtonEntity">表单按钮对象</param>
        /// <param name="keyValue">Id</param>
        public void Save(MenuButtonEntity menuButtonEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                menuButtonEntity.Modify(keyValue, context);
                menuButtonRepo.Update(menuButtonEntity);
            }
            else
            {
                menuButtonEntity.Create(context);
                menuButtonRepo.Insert(menuButtonEntity);
            }
        }
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<MenuButtonEntity> entitys = new List<MenuButtonEntity>();
            foreach (string item in ArrayId)
            {
                MenuButtonEntity moduleButtonEntity = data.Find(t => t.Id == item);
                moduleButtonEntity.Id = Common.GuId();
                moduleButtonEntity.MenuId = moduleId;
                entitys.Add(moduleButtonEntity);
            }
            menuButtonRepo.SubmitCloneButton(entitys);
        }
    }
}
