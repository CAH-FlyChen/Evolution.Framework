/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Data.Entity;
using Evolution.Plugins.Area.Entities;

namespace Evolution.EFConfiguration
{
    public class AreaEFConfiguration : EFConfigurationBase<AreaEntity>
    {
        public override void Configure(EntityTypeBuilder<AreaEntity> entity)
        {
            entity.ToTable("Sys_Area");
            base.Configure(entity);
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.SimpleSpelling).HasColumnName("F_SimpleSpelling");
        }
    }
}
