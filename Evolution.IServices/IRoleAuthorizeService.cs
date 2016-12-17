using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IRoleAuthorizeService
    {
        Task<List<RoleAuthorizeEntity>> GetListByObjectId(string ObjectId);
        Task<List<string>> GetResorucePermissionsByRoleId(string roleId);
        Task<RoleEntity> GetResoucesByRoleId(string roleId, out string permissionIds);
        Task<int> Save(string roleId, List<string> resourceIds);
    }
}