/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IRoleAuthorizeRepository : IRepositoryBase<RoleAuthorizeEntity>
    {
        /// <summary>
        /// 根据角色id获取权限ID
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>资源权限列表</returns>
        List<string> GetResorucePermissionsByRoleId(string roleId);
    }
}
