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
    public class WeiXinUserRepository : RepositoryBase<WeiXinUserEntity>, IWeiXinUserRepository
    {
        WeiXinDbContext ctx = null;
        public WeiXinUserRepository(WeiXinDbContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        public IAsyncEnumerable<WeiXinUserEntity> GetWXUserByAppId(string appId, string tenantId, Pagination p)
        {
            var query = from a in ctx.WeiXinUser
                        join b in ctx.WeiXinMPUserRelations on a.Id equals b.WeiXinUserId
                        where b.AppId == appId && b.TenantId == tenantId
                        select a;

            return query.ToAsyncEnumerable().Skip(p.rows * (p.page - 1)).Take(p.rows);
                     
        }
    }
}
