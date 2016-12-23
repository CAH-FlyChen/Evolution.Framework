using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Demo.Modules
{
    public class ModuleBContext : DbContext
    {
        public DbSet<DemoModel> DemoModels { get; set; }
        public ModuleBContext(DbContextOptions<ModuleBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void DoMerage()
        {
            throw new NotImplementedException();
        }
    }
}
