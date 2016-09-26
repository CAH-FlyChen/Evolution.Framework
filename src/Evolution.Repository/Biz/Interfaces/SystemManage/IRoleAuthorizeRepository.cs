/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IRoleAuthorizeRepository : IRepositoryBase<RoleAuthorizeEntity>
    {
        /// <summary>
        /// 通过角色id获取权限id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<string> GetPermissionsByRoleId(string roleId);
    }
}
