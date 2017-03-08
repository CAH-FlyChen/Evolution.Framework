/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.Modules;

namespace Evolution.Repository.SystemManage
{
    public class WeiXinConfigRepository : RepositoryBase<WeiXinConfigEntity>, IWeiXinConfigRepository
    {
        public WeiXinConfigRepository(WeiXinDbContext ctx) : base(ctx)
        {

        }
    }
}
