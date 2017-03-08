/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Data.Entity;
using Evolution.Plugins.WeiXin.Entities;

namespace Evolution.EFConfiguration
{
    public class WeiXinMPUserRelationEFConfiguration : DbEntityConfiguration<WeiXinMPUserRelationEntity>
    {
        public override void Configure(EntityTypeBuilder<WeiXinMPUserRelationEntity> entity)
        {
            entity.ToTable("Plugin_WeiXin_MPUserRelation");
            entity.HasKey(pc => new { pc.AppId, pc.WeiXinUserId });

            entity.Property(t => t.AppId).HasColumnName("F_WeiXinAppId");
            entity.Property(t => t.WeiXinUserId).HasColumnName("F_WeiXinUserId");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
