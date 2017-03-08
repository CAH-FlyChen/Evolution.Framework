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
    public class NoMatchKeywordsEFConfiguration : EFConfigurationBase<NoMatchKeywordsEntity>
    {
        public override void Configure(EntityTypeBuilder<NoMatchKeywordsEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_NoMatchKeywords");
            base.Configure(entity);
            entity.Property(t => t.Content).HasColumnName("F_Content");
            entity.Property(t => t.NewsId).HasColumnName("F_NewsId");
            entity.Property(t => t.ResType).HasColumnName("F_ResType");
            entity.Property(t => t.AppId).HasColumnName("F_WeiXinConfigId");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
