using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IDutyService
    {
        Task<int> Delete(string keyValue, string tenantId);
        Task<RoleEntity> GetForm(string keyValue, string tenantId);
        Task<List<RoleEntity>> GetList(string keyword, string tenantId);
        Task<int> Save(RoleEntity roleEntity, string keyValue,string tenantId);
    }
}