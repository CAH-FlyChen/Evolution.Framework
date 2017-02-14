/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Evolution.EFConfiguration;
using Evolution.EFConfiguration.SystemManage;
using Evolution.EFConfiguration.SystemSecurity;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.Entity.SystemSecurity;
using System.Data.Common;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using Evolution.Data.Entity;
using JetBrains.Annotations;
using Evolution.Data.Entity.SystemManage;
using Evolution.Framework;
using Evolution.Plugins.Abstract;

namespace Evolution.Data
{
    public class EvolutionDBContext : DbContext
    {
        public EvolutionDBContext(DbContextOptions<EvolutionDBContext> options) 
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
            modelBuilder.AddConfiguration(new ItemsDetailEFConfiguration());
            modelBuilder.AddConfiguration(new ItemsEFConfiguration());
            modelBuilder.AddConfiguration(new MenuButtonEFConfiguration());
            modelBuilder.AddConfiguration(new ModuleEFConfiguration());
            modelBuilder.AddConfiguration(new RoleAuthorizeEFConfiguration());
            modelBuilder.AddConfiguration(new RoleEFConfiguration());
            modelBuilder.AddConfiguration(new UserLogOnEFConfiguration());
            modelBuilder.AddConfiguration(new UserEFConfiguration());
            modelBuilder.AddConfiguration(new DbBackupEFConfiguration());
            modelBuilder.AddConfiguration(new FilterIPEFConfiguration());
            modelBuilder.AddConfiguration(new LogEFConfiguration());
            modelBuilder.AddConfiguration(new MenuEFConfiguration());
            modelBuilder.AddConfiguration(new OrganizeEFConfiguration());
            modelBuilder.AddConfiguration(new PluginEFConfiguration());
            modelBuilder.AddConfiguration(new TenantEFConfiguration());
            //添加并配置第三方插件
            EvolutionPluginManager.AddPluginEFModle(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<OrganizeEntity> Organizes { get; set; }
        public DbSet<ItemsEntity> Items { get; set; }
        public DbSet<ItemsDetailEntity> ItemsDetails { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<MenuButtonEntity> ModuleButtons { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<RoleAuthorizeEntity> RoleAuthorize { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserLogOnEntity> UserLogOn { get; set; }
        public DbSet<DbBackupEntity> DbBackups { get; set; }
        public DbSet<FilterIPEntity> FilterIPs { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<PluginEntity> Plugins { get; set; }
        public DbSet<TenantEntity> Tenants { get; set; }
    }
}
