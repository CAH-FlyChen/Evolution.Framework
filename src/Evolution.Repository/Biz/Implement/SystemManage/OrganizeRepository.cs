﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;

namespace Evolution.Repository.SystemManage
{
    public class OrganizeRepository : RepositoryBase<OrganizeEntity>, IOrganizeRepository
    {
        public OrganizeRepository(EvolutionDBContext ctx) : base(ctx)
        {

        }
    }
}
