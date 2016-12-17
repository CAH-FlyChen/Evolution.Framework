using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IItemsService
    {
        Task<int> Delete(string keyValue);
        Task<ItemsEntity> GetForm(string keyValue);
        Task<List<ItemsEntity>> GetList();
        Task<int> Save(ItemsEntity itemsEntity, string keyValue, HttpContext context);
    }
}