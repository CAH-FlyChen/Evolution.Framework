using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IRoleAuthorizeService
    {
        Task<List<RoleAuthorizeEntity>> GetListByObjectId(string ObjectId, string tenantId);
        Task<List<string>> GetResorucePermissionsByRoleId(string roleId, string tenantId);
        Task<RoleEntity> GetResoucesByRoleId(string roleId, string tenantId, out string permissionIds);
        Task<int> Save(string roleId, List<string> resourceIds, string userId, string tenantId);
    }
}