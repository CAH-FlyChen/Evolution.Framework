using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Plugins.Demo.Modules;
using Evolution.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Demo.Repos
{
    public class DemoRepository : RepositoryBase<DemoModel>,IDemoRepositroy
    {
        ModuleBContext dbContext = null;
        public DemoRepository(ModuleBContext ctx) : base(ctx)
        {
            dbContext = ctx;
        }
        public string GetDemoInfo()
        {
            //var x = dbContext.DemoModels.ToList();
            return "ABC";
        }
    }
}
