using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.Modules;
using Evolution.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.Repos
{
    public class LocationRepository: RepositoryBase<LocationEntity>, ILocationRepository
    {
        public LocationRepository(WeiXinDbContext ctx) : base(ctx)
        {

        }
    }
}
