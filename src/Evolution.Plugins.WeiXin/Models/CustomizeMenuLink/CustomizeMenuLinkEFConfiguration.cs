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
    public class CustomizeMenuLinkEFConfiguration : EFConfigurationBase<CustomizeMenuLinkEntity>
    {
        public override void Configure(EntityTypeBuilder<CustomizeMenuLinkEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_CustomizeMenuLink");
            base.Configure(entity);
            entity.Property(t => t.MenuLink).HasColumnName("F_MenuLink");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
