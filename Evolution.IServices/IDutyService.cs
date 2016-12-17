using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IDutyService
    {
        Task<int> Delete(string keyValue);
        Task<RoleEntity> GetForm(string keyValue);
        Task<List<RoleEntity>> GetList(string keyword = "");
        Task<int> Save(RoleEntity roleEntity, string keyValue, HttpContext context);
    }
}