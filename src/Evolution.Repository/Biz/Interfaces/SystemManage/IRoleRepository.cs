/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IRoleRepository : IRepositoryBase<RoleEntity>
    {
        /// <summary>
        /// 删除角色对象及相关授权
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        Task<int> DeleteAsync(string keyValue);
        /// <summary>
        /// 保存角色对象及角色菜单授权
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="roleAuthorizeEntitys">角色的授权列表</param>
        /// <param name="keyValue">角色Id</param>
        Task<int> SaveAsync(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);
    }
}
