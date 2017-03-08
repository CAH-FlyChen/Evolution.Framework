/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.Modules;
using System.Linq;
using Evolution.Framework;
using Evolution.Plugins.WeiXin.DTO.CustomizeMenu;

namespace Evolution.Repository.SystemManage
{
    public class CustomizedMenuRepository : RepositoryBase<CustomizeMenuEntity>, ICustomizedMenuRepository
    {
        WeiXinDbContext ctx = null;
        public CustomizedMenuRepository(WeiXinDbContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        public void AddMenu(CustomizeMenuInput customizeMenuInput)
        {
            lock (ctx)
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (customizeMenuInput.ActionType == MenuActionType.链接)
                        {
                            AddMenuLink(customizeMenuInput.Title, 
                                customizeMenuInput.Url, customizeMenuInput.ParentId,
                                customizeMenuInput.UserId, customizeMenuInput.AppId, 
                                customizeMenuInput.TenantId
                                );
                        }
                        else if (customizeMenuInput.ActionType == MenuActionType.图文 
                            || customizeMenuInput.ActionType == MenuActionType.多图文)
                        {
                            AddNewsLink(customizeMenuInput.Title,
                                customizeMenuInput.NewsId,customizeMenuInput.ActionType,
                                customizeMenuInput.ParentId,
                                customizeMenuInput.UserId, customizeMenuInput.AppId,
                                customizeMenuInput.TenantId
                                );
                        }
                        ctx.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                    }
                }
            }
        }
        public void DeleteMenu(string MenuId,bool deleteRef)
        {
            var item = ctx.CustomizeMenus.Single(t => t.Id == MenuId);
            //单图文，多图文不能进行级联删除
            if((item.ActionType== MenuActionType.图文 || item.ActionType == MenuActionType.多图文)&& deleteRef)
            {
                throw new Exception("单图文，多图文不能进行级联删除");
            }
            else if(item.ActionType== MenuActionType.链接)
            {
                if(deleteRef)
                {
                    var linkItem = ctx.CustomizeMenuLinks.Single(t => t.Id == item.ActionId);
                    ctx.CustomizeMenuLinks.Remove(linkItem);
                }
                ctx.CustomizeMenus.Remove(item);
            }
            else
            {
                if(deleteRef)
                    throw new Exception("未知的菜单类型");
                ctx.CustomizeMenus.Remove(item);
            }
            ctx.SaveChanges();
        }
        public void UpdateMenu(CustomizeMenuInput customizeMenuInput)
        {
            var item = ctx.CustomizeMenus.Single(t => t.Id == customizeMenuInput.Id);
            item.ActionId = customizeMenuInput.ActionId;
            item.ActionType = customizeMenuInput.ActionType;
            item.ParentId = customizeMenuInput.ParentId;
            item.SortCode = customizeMenuInput.SortCode;
            item.Title = customizeMenuInput.Title;
            ctx.SaveChanges();
        }

        public List<CustomizeMenuEntity> GetMenuList()
        {
            return ctx.CustomizeMenus.ToList();
        }

        #region 内部方法
        /// <summary>
        /// 添加菜单链接
        /// </summary>
        /// <param name="title">菜单标题</param>
        /// <param name="menuLinkUrl">菜单链接</param>
        /// <param name="parentId">父菜单Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="appId">AppId</param>
        /// <param name="tenantId">租户Id</param>
        private void AddMenuLink(string title, string menuLinkUrl, string parentId, string userId, string appId, string tenantId)
        {
            DateTime createTime = DateTime.Now;
            int maxOrder = GetMaxDisplayOrder(parentId, appId);
            string actionId = Guid.NewGuid().ToString("N");

            CustomizeMenuLinkEntity cmle = new CustomizeMenuLinkEntity();
            cmle.CreateTime = createTime;
            cmle.CreatorUserId = userId;
            cmle.DeleteMark = false;
            cmle.EnabledMark = true;
            cmle.Id = actionId;
            cmle.MenuLink = menuLinkUrl;
            cmle.TenantId = tenantId;
            ctx.CustomizeMenuLinks.Add(cmle);
            AddCustomizMenuEntity(title, actionId, maxOrder + 1, MenuActionType.链接, parentId, userId, appId, tenantId);
        }
        /// <summary>
        /// 添加图文菜单
        /// </summary>
        /// <param name="title">菜单名称</param>
        /// <param name="newsId">图文消息的Id</param>
        /// <param name="actionType">图文类型，（单图文，多图文）</param>
        /// <param name="parentId">父菜单Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="appId">应用Id</param>
        /// <param name="tenantId">租户Id</param>
        private void AddNewsLink(string title, string newsId, MenuActionType actionType, string parentId, string userId, string appId, string tenantId)
        {
            DateTime createTime = DateTime.Now;
            int maxOrder = GetMaxDisplayOrder(parentId, appId);
            string actionId = Guid.NewGuid().ToString("N");

            CustomizeMenuNewsEntity cmle = new CustomizeMenuNewsEntity();
            cmle.CreateTime = createTime;
            cmle.CreatorUserId = userId;
            cmle.DeleteMark = false;
            cmle.EnabledMark = true;
            cmle.Id = actionId;
            cmle.NewsId = newsId;
            cmle.TenantId = tenantId;
            ctx.CustomizeMenuNews.Add(cmle);
            AddCustomizMenuEntity(title, actionId, maxOrder + 1, actionType, parentId, userId, appId, tenantId);
        }
        /// <summary>
        /// 基础菜单（内部使用）
        /// </summary>
        /// <param name="title">菜单标题</param>
        /// <param name="actionId">动作表Id</param>
        /// <param name="order">顺序</param>
        /// <param name="actionType">菜单动作类型</param>
        /// <param name="parentId">父菜单Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="appId">应用Id</param>
        /// <param name="tenantId">租户Id</param>
        private void AddCustomizMenuEntity(string title, string actionId,int order, MenuActionType actionType, string parentId, string userId, string appId, string tenantId)
        {
            CustomizeMenuEntity cme = new CustomizeMenuEntity();
            cme.AppId = appId;
            cme.ActionType = actionType;
            cme.CreatorUserId = userId;
            cme.CreateTime = DateTime.Now;
            cme.ActionId = actionId;
            cme.DeleteMark = false;
            cme.EnabledMark = true;
            cme.Id = Guid.NewGuid().ToString("N");
            cme.ParentId = parentId;
            cme.SortCode = order;
            if (string.IsNullOrEmpty(parentId))
                cme.LevelNUM = 1;
            else
                cme.LevelNUM = 2;
            cme.TenantId = tenantId;
            cme.Title = title;
            ctx.CustomizeMenus.Add(cme);
        }
        /// <summary>
        /// 获取最大顺序号
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        private int GetMaxDisplayOrder(string parentId,string appId)
        {
            int? maxOrder = -1;
            try
            {
                maxOrder = ctx.CustomizeMenus.Where(x => x.ParentId == parentId && x.AppId == appId && x.DeleteMark==false).Max(t => t.SortCode);
            }
            catch (NullReferenceException ex)
            {
                maxOrder = 0;
            }
            if (maxOrder == null)
                return 0;
            return (int)maxOrder;
        }
        #endregion
    }
}
