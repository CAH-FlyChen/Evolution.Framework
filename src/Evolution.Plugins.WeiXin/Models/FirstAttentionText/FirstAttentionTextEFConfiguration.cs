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
    public class FirstAttentionTextEFConfiguration : EFConfigurationBase<FirstAttentionTextEntity>
    {
        public override void Configure(EntityTypeBuilder<FirstAttentionTextEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_FirstAttentionText");
            base.Configure(entity);
            entity.Property(t => t.Content).HasColumnName("F_Content");
            entity.Property(t => t.Type).HasColumnName("F_Type");
            entity.Property(t => t.AppId).HasColumnName("F_AppId");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
