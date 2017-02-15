using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IItemsService
    {
        Task<int> Delete(string keyValue, string tenantId);
        Task<ItemsEntity> GetForm(string keyValue, string tenantId);
        Task<List<ItemsEntity>> GetList(string tenantId);
        Task<int> Save(ItemsEntity itemsEntity, string keyValue, string userId);
    }
}