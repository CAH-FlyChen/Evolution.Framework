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

namespace Evolution.Repository.SystemManage
{
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(EvolutionDbContext ctx) : base(ctx){}
        /// <summary>
        /// 删除角色对象及相关授权
        /// </summary>
        /// <param name="keyValue">角色Id</param>
        public void Delete(string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                db.Delete<RoleEntity>(t => t.Id == keyValue);
                db.Delete<RoleAuthorizeEntity>(t => t.ObjectId == keyValue);
                db.Commit();
            }
        }
        /// <summary>
        /// 保存角色对象及角色菜单授权
        /// </summary>
        /// <param name="roleEntity">角色对象</param>
        /// <param name="roleAuthorizeEntitys">角色的授权列表</param>
        /// <param name="keyValue">角色Id</param>
        public void Save(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(roleEntity);
                }
                else
                {
                    roleEntity.Category = 1;
                    db.Insert(roleEntity);
                }
                db.Delete<RoleAuthorizeEntity>(t => t.ObjectId == roleEntity.Id && t.ItemType==1);
                db.Insert(roleAuthorizeEntitys);
                db.Commit();
            }
        }
    }
}
