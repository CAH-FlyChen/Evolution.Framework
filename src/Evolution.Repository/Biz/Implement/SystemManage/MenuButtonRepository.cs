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
    public class MenuButtonRepository : RepositoryBase<MenuButtonEntity>, IMenuButtonRepository
    {
        public MenuButtonRepository(EvolutionDbContext ctx) : base(ctx)
        {

        }
        /// <summary>
        /// 保存克隆的按钮
        /// </summary>
        /// <param name="entitys">按钮对象列表</param>
        public Task<int> SaveCloneButton(List<MenuButtonEntity> entitys)
        {
            
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                foreach (var item in entitys)
                {
                    db.InsertAsync(item);
                }
                return db.CommitAsync();
            }
        }
    }
}
