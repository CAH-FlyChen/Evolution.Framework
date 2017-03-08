using Evolution.EFConfiguration;
using Evolution.Plugins.WeiXin.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Plugins.WeiXin.Modules
{
    public class WeiXinDbContext : DbContext
    {
        public DbSet<WeiXinConfigEntity> WeiXinConfigs { get; set; }
        public DbSet<CustomizeMenuEntity> CustomizeMenus { get; set; }
        public DbSet<CustomizeMenuLinkEntity> CustomizeMenuLinks { get; set; }
        public DbSet<CustomizeMenuNewsEntity> CustomizeMenuNews { get; set; }
        //public DbSet<FirstAttentionTextEntity> FirstAttentions { get; set; }
        //public DbSet<KeywordsEntity> Keywords { get; set; }
        //public DbSet<LocationEntity> Locations { get; set; }
        //public DbSet<NoMatchKeywordsEntity> NoMatchKewords { get; set; }
        public DbSet<WeiXinMPUserRelationEntity> WeiXinMPUserRelations { get; set; }
        public DbSet<WeiXinUserEntity> WeiXinUser { get; set; }

        public WeiXinDbContext(DbContextOptions<WeiXinDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new WeiXinConfigEFConfiguration());
            //modelBuilder.AddConfiguration(new CustomizeMenuEFConfiguration());
            //modelBuilder.AddConfiguration(new CustomizeMenuLinkEFConfiguration());
            //modelBuilder.AddConfiguration(new CustomizeMenuNewsEFConfiguration());
            //modelBuilder.AddConfiguration(new FirstAttentionTextEFConfiguration());
            //modelBuilder.AddConfiguration(new KeywordsEFConfiguration());
            //modelBuilder.AddConfiguration(new LocationEFConfiguration());
            //modelBuilder.AddConfiguration(new NoMatchKeywordsEFConfiguration());
            modelBuilder.AddConfiguration(new WeiXinMPUserRelationEFConfiguration());
            modelBuilder.AddConfiguration(new WeiXinUserEFConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void DoMerage()
        {
            
        }
    }
}
