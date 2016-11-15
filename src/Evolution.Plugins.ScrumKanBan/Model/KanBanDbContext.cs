using Evolution.EFConfiguration;
using Evolution.Plugins.ScrumKanBan.Models;
using Evolution.Plugins.ScrumKanBan.Modules;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    public class KanBanDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<StoryStatusEntity> StoryStatus { get; set; }
        public DbSet<UserStoryEntity> UserStories { get; set; }
        public KanBanDbContext(DbContextOptions<KanBanDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //KanBanModleBuilder builder = new KanBanModleBuilder();
            //builder.Build(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public void DoMerage()
        {
            
        }
    }
}
