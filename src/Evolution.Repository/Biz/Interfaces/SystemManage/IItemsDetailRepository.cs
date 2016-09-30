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
    public interface IItemsDetailRepository : IRepositoryBase<ItemsDetailEntity>
    {
        Task<List<ItemsDetailEntity>> GetItemList(string enCode);
    }
}
