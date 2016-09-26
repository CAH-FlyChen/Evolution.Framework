/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Evolution.EFConfiguration;
using Evolution.EFConfiguration.SystemManage;
using Evolution.EFConfiguration.SystemSecurity;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.Entity.SystemSecurity;
using System.Data.Common;
using System.Data;

namespace Evolution.Data
{
    public class EvolutionDbContext : DbContext
    {
        public EvolutionDbContext(DbContextOptions<EvolutionDbContext> options) 
            : base(options)
        {
        }

        //public EvolutionDbContext()
        //{
        //    //this.Configuration.AutoDetectChangesEnabled = false;
        //    //this.Configuration.ValidateOnSaveEnabled = false;
        //    //this.Configuration.LazyLoadingEnabled = false;
        //    //this.Configuration.ProxyCreationEnabled = false;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AreaEFConfiguration());
            modelBuilder.AddConfiguration(new ItemsDetailEFConfiguration());
            modelBuilder.AddConfiguration(new ItemsEFConfiguration());
            modelBuilder.AddConfiguration(new MenuButtonEFConfiguration());
            modelBuilder.AddConfiguration(new ModuleEFConfiguration());
            modelBuilder.AddConfiguration(new OrganizeEFConfiguration());
            modelBuilder.AddConfiguration(new RoleAuthorizeEFConfiguration());
            modelBuilder.AddConfiguration(new RoleEFConfiguration());
            modelBuilder.AddConfiguration(new UserLogOnEFConfiguration());
            modelBuilder.AddConfiguration(new UserEFConfiguration());
            modelBuilder.AddConfiguration(new DbBackupEFConfiguration());
            modelBuilder.AddConfiguration(new FilterIPEFConfiguration());
            modelBuilder.AddConfiguration(new LogEFConfiguration());
            modelBuilder.AddConfiguration(new MenuEFConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AreaEntity> Areas { get; set; }
        public DbSet<ItemsEntity> Items { get; set; }
        public DbSet<ItemsDetailEntity> ItemsDetails { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<MenuButtonEntity> ModuleButtons { get; set; }
        public DbSet<OrganizeEntity> Organizes { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<RoleAuthorizeEntity> RoleAuthorize { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserLogOnEntity> UserLogOn { get; set; }


        public DbSet<DbBackupEntity> DbBackups { get; set; }
        public DbSet<FilterIPEntity> FilterIPs { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
    }
}
