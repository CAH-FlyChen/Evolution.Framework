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
    public class WeiXinConfigEFConfiguration : EFConfigurationBase<WeiXinConfigEntity>
    {
        public override void Configure(EntityTypeBuilder<WeiXinConfigEntity> entity)
        {
            entity.ToTable("Plugin_WeiXin_Config");
            base.Configure(entity);
            entity.Property(t => t.AppId).HasColumnName("F_AppId");
            entity.Property(t => t.EncodingAESKey).HasColumnName("F_EncodingAESKey");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
            entity.Property(t => t.Token).HasColumnName("F_Token");
            entity.Property(t => t.AppSecret).HasColumnName("F_AppSecret");
        }
    }
}
