using Evolution.Plugins.Area.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Area.IServices
{
    public interface IAreaService
    {
        Task<List<AreaEntity>> GetList();
        Task<AreaEntity> GetForm(string keyValue);
        Task<int> Delete(string keyValue);
        Task<int> Save(AreaEntity organizeEntity, string keyValue, string userId);
    }
}
