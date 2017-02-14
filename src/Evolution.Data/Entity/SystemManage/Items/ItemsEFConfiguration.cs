﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemManage
{
    public class ItemsEFConfiguration : EFConfigurationBase<ItemsEntity>
    {
        public override void Configure(EntityTypeBuilder<ItemsEntity> entity)
        {
            entity.ToTable("Sys_Items");
            base.Configure(entity);
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.IsTree).HasColumnName("F_IsTree");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }


    }
}
