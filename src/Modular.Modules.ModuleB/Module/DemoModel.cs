using Evolution.Data.Entity;
using Evolution.Plugin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Modular.Modules.ModuleB.Module
{
    public class DemoModel : EntityBase
    {
        public string DemoStr { get; set; }
    }

    public class DemoModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoModel>()
                .ToTable("DemoModel");
        }
    }

}
