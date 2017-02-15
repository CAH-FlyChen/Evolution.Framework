using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IItemsDetailService
    {
        Task<int> Delete(string keyValue, string tenantId);
        Task<ItemsDetailEntity> GetForm(string keyValue, string tenantId);
        Task<List<ItemsDetailEntity>> GetItemList(string enCode, string tenantId);
        Task<List<ItemsDetailEntity>> GetList(string itemId, string keyword, string tenantId);
        Task<int> Save(ItemsDetailEntity itemsDetailEntity, string keyValue, string userId);
    }
}