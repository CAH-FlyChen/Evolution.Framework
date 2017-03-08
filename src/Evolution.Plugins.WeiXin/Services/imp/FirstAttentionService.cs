
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Evolution.Plugins.WeiXin.Services
{
    public class FirstAttentionService : IFirstAttentionService
    {
        private IFirstAttentionRepository repo = null;

        public FirstAttentionService(IFirstAttentionRepository repo)
        {
            this.repo = repo;
        }



        #region 查找首次关注
        public Task<FirstAttentionTextEntity> GetFirstAttention(string appId,string tenantId)
        {
            return repo.FindEntityASync(t => t.AppId == appId && t.TenantId == tenantId);
        }
        #endregion

        #region 添加首次关注
        public Task<int> AddFirstAttention(FirstAttentionTextEntity rfa)
        {
            return repo.InsertAsync(rfa);
        }
        #endregion

        #region 更新首次关注
        public Task<int> UpdateFirstAttention(FirstAttentionTextEntity rfa)
        {
            return repo.UpdateAsync(rfa);
        }
        #endregion
    }
}
