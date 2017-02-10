using Evolution.EFConfiguration;
using Evolution.Plugins.Area.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Plugins.Area.Modules
{
    public class AreaDbContext : DbContext
    {
        public DbSet<AreaEntity> Areas { get; set; }
        public AreaDbContext(DbContextOptions<AreaDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AreaEFConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public void DoMerage()
        {
            
        }
    }
}
