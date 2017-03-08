
using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.IServices
{
    public interface IFirstAttentionService
    {
        Task<FirstAttentionTextEntity> GetFirstAttention(string appId, string tenantId);
        Task<int> AddFirstAttention(FirstAttentionTextEntity rfa);
        Task<int> UpdateFirstAttention(FirstAttentionTextEntity rfa);
    }
}
