using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public interface IResourceService
    {
        Task<List<ResourceEntity>> GetList();
    }
}