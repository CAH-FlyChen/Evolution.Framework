/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Repository;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Repository.SystemManage;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Evolution.Repository.SystemManage
{
    public class RoleAuthorizeRepository : RepositoryBase<RoleAuthorizeEntity>, IRoleAuthorizeRepository
    {
        public RoleAuthorizeRepository(EvolutionDbContext ctx) : base(ctx)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>被授权的ItemId</returns>
        public List<string> GetResorucePermissionsByRoleId(string roleId)
        {
            var hasPermissionPathres = dbcontext.RoleAuthorize.Where(t => t.ObjectType == 1 && t.ObjectId == roleId && t.ItemType == 4).Select(t => t.ItemId).ToList();
            return hasPermissionPathres;
        }
    }
}
