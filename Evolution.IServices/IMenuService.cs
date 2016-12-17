using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IMenuService
    {
        Task<int> Delete(string keyValue);
        Task<List<MenuEntity>> GetList();
        Task<MenuEntity> GetMenuById(string keyValue);
        Task<List<MenuEntity>> GetMenuListByRoleId(string roleId);
        Task<int> Save(MenuEntity menuEntity, string keyValue);
    }
}