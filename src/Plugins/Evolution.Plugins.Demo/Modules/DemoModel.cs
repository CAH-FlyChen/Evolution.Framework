using Evolution.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Evolution.Data;
using Evolution.Plugins.Abstract;

namespace Evolution.Plugins.Demo.Modules
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
