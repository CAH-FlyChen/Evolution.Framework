/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.Area.Entities;
using Evolution.Plugins.Area.Modules;

namespace Evolution.Repository.SystemManage
{
    public class AreaRepository : RepositoryBase<AreaEntity>, IAreaRepository
    {
        public AreaRepository(AreaDbContext ctx) : base(ctx)
        {

        }
    }
}
