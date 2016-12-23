using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IRoleService
    {
        Task<int> Delete(string id);
        Task<List<RoleEntity>> GetList(string keyword = "");
        Task<RoleEntity> GetRoleById(string id);
        Task<int> Save(RoleEntity roleEntity, string[] permissionIds, string keyValue);
    }
}