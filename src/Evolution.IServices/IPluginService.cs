using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;

namespace Evolution.Application.SystemManage
{
    public interface IPluginService
    {
        bool Activate(string pluginId);
        Task<List<PluginEntity>> GetList(Pagination pagination, string keyword, string tenantId);
    }
}