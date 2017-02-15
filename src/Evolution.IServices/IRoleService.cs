using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IRoleService
    {
        Task<int> Delete(string id, string tenantId);
        Task<List<RoleEntity>> GetList(string keyword,string tenantId);
        Task<RoleEntity> GetRoleById(string id,string tenantId);
        Task<int> Save(RoleEntity roleEntity, string[] permissionIds, string keyValue,string tenantId);
    }
}