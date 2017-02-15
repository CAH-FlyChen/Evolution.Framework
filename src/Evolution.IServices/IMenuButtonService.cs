using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IMenuButtonService
    {
        Task<int> Delete(string id, string tenantId);
        Task<List<MenuButtonEntity>> GetButtonListByRoleId(string roleId,bool isSystem, string tenantId);
        Task<List<MenuButtonEntity>> GetList( string tenantId);
        Task<List<MenuButtonEntity>> GetListByMenuId(string menuId, string tenantId);
        Task<MenuButtonEntity> GetMenuButtonById(string id, string tenantId);
        Task<int> SaveCloneButton(string menuId, string Ids,string tenantId);
        Task Save(MenuButtonEntity menuButtonEntity, string keyValue, string userId);
    }
}