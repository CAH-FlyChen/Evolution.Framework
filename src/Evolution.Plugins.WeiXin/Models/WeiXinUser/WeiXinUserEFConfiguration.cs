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
    public class WeiXinUserEFConfiguration : EFConfigurationBase<WeiXinUserEntity>
    {
        public override void Configure(EntityTypeBuilder<WeiXinUserEntity> entity)
        {
            entity.ToTable("Plugin_WeiXin_User");
            base.Configure(entity);
            entity.Property(t => t.AttentionTime).HasColumnName("F_AttentionTime");
            entity.Property(t => t.City).HasColumnName("F_City");
            entity.Property(t => t.Country).HasColumnName("F_Country");
            entity.Property(t => t.HeadImgUrl).HasColumnName("F_HeadImgUrl");
            entity.Property(t => t.Language).HasColumnName("F_Language");
            entity.Property(t => t.NickName).HasColumnName("F_NickName");
            entity.Property(t => t.Province).HasColumnName("F_Province");
            entity.Property(t => t.Sex).HasColumnName("F_Sex");

            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
