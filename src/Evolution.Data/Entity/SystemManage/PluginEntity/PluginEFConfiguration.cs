/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Data.Entity;
using Evolution.Data.Entity.SystemManage;
using Evolution.Domain.Entity.SystemManage;

namespace Evolution.EFConfiguration
{
    public class PluginEFConfiguration : EFConfigurationBase<PluginEntity>
    {
        public override void Configure(EntityTypeBuilder<PluginEntity> entity)
        {
            entity.ToTable("Sys_Plugin");
            base.Configure(entity);
            entity.Property(t => t.Activated).HasColumnName("F_Activated");
            entity.Property(t => t.AssessmblyName).HasColumnName("F_AssessmblyName");
            entity.Property(t => t.Name).HasColumnName("F_Name");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantID");
        }
    }
}
