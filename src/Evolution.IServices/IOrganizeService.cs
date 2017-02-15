using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Data.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IOrganizeService
    {
        Task<int> Delete(string keyValue, string tenantId);
        Task<OrganizeEntity> GetForm(string keyValue,string tenantId);
        Task<List<OrganizeEntity>> GetList(string tenantId);
        Task<int> Save(OrganizeEntity organizeEntity, string keyValue, string userId);
    }
}