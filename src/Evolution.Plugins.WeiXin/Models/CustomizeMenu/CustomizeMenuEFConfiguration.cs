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
    public class CustomizeMenuEFConfiguration : EFConfigurationBase<CustomizeMenuEntity>
    {
        public override void Configure(EntityTypeBuilder<CustomizeMenuEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_CustomizeMenu");
            base.Configure(entity);
            entity.Property(t => t.ActionId).HasColumnName("F_ActionId");
            entity.Property(t => t.ActionType).HasColumnName("F_ActionType");
            entity.Property(t => t.LevelNUM).HasColumnName("F_LevelNUM");
            entity.Property(t => t.NeedOAuth).HasColumnName("F_NeedOAuth");
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Title).HasColumnName("F_Title");
            entity.Property(t => t.AppId).HasColumnName("F_AppId");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
