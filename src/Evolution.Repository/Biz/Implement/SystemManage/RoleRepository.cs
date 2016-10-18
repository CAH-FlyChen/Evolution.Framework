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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolution.Repository.SystemManage
{
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(EvolutionDBContext ctx) : base(ctx){}
        /// <summary>
        /// 删除角色对象及相关授权
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        public Task<int> DeleteAsync(string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                db.DeleteAsync<RoleEntity>(t => t.Id == keyValue);
                db.DeleteAsync<RoleAuthorizeEntity>(t => t.ObjectId == keyValue);
                return db.CommitAsync();
            }
        }
        /// <summary>
        /// 保存角色对象及角色菜单授权
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="roleAuthorizeEntitys">角色的授权列表</param>
        /// <param name="keyValue">角色Id</param>
        public Task<int> SaveAsync(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.UpdateAsync(roleEntity);
                }
                else
                {
                    roleEntity.Category = 1;
                    db.InsertAsync(roleEntity);
                }
                db.DeleteAsync<RoleAuthorizeEntity>(t => t.ObjectId == roleEntity.Id && t.ItemType==1);
                db.InsertAsync(roleAuthorizeEntitys);
                return db.CommitAsync();
            }
        }
    }
}
