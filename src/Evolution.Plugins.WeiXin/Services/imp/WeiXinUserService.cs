
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.Framework;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.IServices;
using Evolution.Domain.IRepository.SystemManage;

namespace Evolution.Plugins.WeiXin.Services
{
    public class WeiXinUserService : IWeiXinUserService
    {
        private IWeiXinUserRepository repo = null;

        public WeiXinUserService(IWeiXinUserRepository repo)
        {
            this.repo = repo;
        }

        public IAsyncEnumerable<WeiXinUserEntity> GetAllByAppId(string appId, string tenantId, Pagination p)
        {
            return repo.GetWXUserByAppId(appId, tenantId, p);
        }

        public Task<List<WeiXinUserEntity>> GetAllByTenantId(string tenantId, Pagination p)
        {
            return repo.FindListAsync(t => t.TenantId == tenantId, p);
        }

        public Task<WeiXinUserEntity> GetWXUserInfo(string wxUserId)
        {
            return repo.FindEntityASync(t => t.Id == wxUserId);
        }
    }
}
