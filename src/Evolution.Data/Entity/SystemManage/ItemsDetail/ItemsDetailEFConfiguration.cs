/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemManage
{
    public class ItemsDetailEFConfiguration : EFConfigurationBase<ItemsDetailEntity>
    {
        public override void Configure(EntityTypeBuilder<ItemsDetailEntity> entity)
        {
            entity.ToTable("Sys_ItemsDetail");
            base.Configure(entity);
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.ItemId).HasColumnName("F_ItemId");
            entity.Property(t => t.ItemCode).HasColumnName("F_ItemCode");
            entity.Property(t => t.ItemName).HasColumnName("F_ItemName");
            entity.Property(t => t.SimpleSpelling).HasColumnName("F_SimpleSpelling");
            entity.Property(t => t.IsDefault).HasColumnName("F_IsDefault");
        }

    }
}
