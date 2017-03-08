using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface ITenantService
    {
        Task<int> Delete(string keyValue);
        Task<TenantEntity> GetForm(string keyValue);
        Task<List<TenantEntity>> GetList(string keyword = "");
        Task<int> Save(TenantEntity roleEntity, string keyValue, HttpContext context);
    }
}