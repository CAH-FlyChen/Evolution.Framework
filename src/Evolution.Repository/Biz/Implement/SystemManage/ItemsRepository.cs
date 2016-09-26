/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Repository;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Repository.SystemManage;

namespace Evolution.Repository.SystemManage
{
    public class ItemsRepository : RepositoryBase<ItemsEntity>, IItemsRepository
    {
        public ItemsRepository(EvolutionDbContext ctx) : base(ctx)
        {

        }
    }
}
