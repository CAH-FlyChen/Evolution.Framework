﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Repository;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;

namespace Evolution.Repository.SystemManage
{
    public class MenuRepository : RepositoryBase<MenuEntity>, IMenuRepository
    {
        public MenuRepository(EvolutionDBContext ctx) : base(ctx)
        {

        }
    }
}
