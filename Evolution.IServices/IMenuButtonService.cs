using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IMenuButtonService
    {
        Task<int> Delete(string id);
        Task<List<MenuButtonEntity>> GetButtonListByRoleId(string roleId);
        Task<List<MenuButtonEntity>> GetList();
        Task<List<MenuButtonEntity>> GetListByMenuId(string menuId);
        Task<MenuButtonEntity> GetMenuButtonById(string id);
        Task Save(MenuButtonEntity menuButtonEntity, string keyValue);
        Task<int> SaveCloneButton(string menuId, string Ids);
    }
}