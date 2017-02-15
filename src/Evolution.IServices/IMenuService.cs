using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IMenuService
    {
        Task<int> Delete(string keyValue, string tenantId);
        Task<List<MenuEntity>> GetList(string tenantId);
        Task<MenuEntity> GetMenuById(string keyValue,string tenantId);
        Task<List<MenuEntity>> GetMenuListByRoleId(string roleId,string tenantId,string isSystem);
        Task<int> Save(MenuEntity menuEntity, string keyValue,string userId);
    }
}