using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Data.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IOrganizeService
    {
        Task<int> Delete(string keyValue);
        Task<OrganizeEntity> GetForm(string keyValue);
        Task<List<OrganizeEntity>> GetList();
        Task<int> Save(OrganizeEntity organizeEntity, string keyValue, HttpContext context);
    }
}