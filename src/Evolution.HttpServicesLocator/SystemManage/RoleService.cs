/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemManage
{
    public class RoleService : IRoleService
    {
        #region 私有变量
        private IRoleRepository service = null;
        private MenuService menuApp = null;
        private MenuButtonService moduleButtonApp = null;
        #endregion
        #region 构造函数
        public RoleService(IRoleRepository service, MenuService menuApp, MenuButtonService moduleButtonApp)
        {
            this.service = service;
            this.menuApp = menuApp;
            this.moduleButtonApp = moduleButtonApp;
        }
        #endregion
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public Task<List<RoleEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FullName.Contains(keyword));
                expression = expression.Or(t => t.EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.Category == 1);
            return service.IQueryable(expression).OrderBy(t => t.SortCode).ToListAsync();
        }
        /// <summary>
        /// 根据Id获取角色对象
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public Task<RoleEntity> GetRoleById(string id)
        {
            return service.FindEntityAsync(id);
        }
        /// <summary>
        /// 删除角色对象及相关授权
        /// </summary>
        /// <param name="id">角色对象</param>
        public Task<int> Delete(string id)
        {
            return service.DeleteAsync(id);
        }
        /// <summary>
        /// 保存角色及角色菜单授权
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="permissionIds">菜单授权Id</param>
        /// <param name="keyValue">角色Id</param>
        public async Task<int> Save(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Id = keyValue;
            }
            else
            {
                roleEntity.Id = Common.GuId();
            }
            var menuData = await menuApp.GetList();
            var buttonData = await moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.Id = Common.GuId();
                roleAuthorizeEntity.ObjectType = 1;
                roleAuthorizeEntity.ObjectId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                if (menuData.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 1;
                }
                if (buttonData.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            //保存菜单授权
            return await service.SaveAsync(roleEntity, roleAuthorizeEntitys, keyValue);
        }
    }
}
