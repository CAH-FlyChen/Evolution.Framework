/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.Modules;
using System.Linq;
using Evolution.Framework;

namespace Evolution.Repository.SystemManage
{
    public class FirstAttentionRepository : RepositoryBase<FirstAttentionTextEntity>, IFirstAttentionRepository
    {
        WeiXinDbContext ctx = null;
        public FirstAttentionRepository(WeiXinDbContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

    }
}
