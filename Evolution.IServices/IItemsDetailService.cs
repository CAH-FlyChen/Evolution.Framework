using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IItemsDetailService
    {
        Task<int> Delete(string keyValue);
        Task<ItemsDetailEntity> GetForm(string keyValue);
        Task<List<ItemsDetailEntity>> GetItemList(string enCode);
        Task<List<ItemsDetailEntity>> GetList(string itemId = "", string keyword = "");
        Task<int> Save(ItemsDetailEntity itemsDetailEntity, string keyValue, HttpContext context);
    }
}