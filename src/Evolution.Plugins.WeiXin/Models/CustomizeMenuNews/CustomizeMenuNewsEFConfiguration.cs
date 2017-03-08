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
    public class CustomizeMenuNewsEFConfiguration : EFConfigurationBase<CustomizeMenuNewsEntity>
    {
        public override void Configure(EntityTypeBuilder<CustomizeMenuNewsEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_CustomizeMenuNews");
            base.Configure(entity);
            entity.Property(t => t.NewsId).HasColumnName("F_NewsId");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
