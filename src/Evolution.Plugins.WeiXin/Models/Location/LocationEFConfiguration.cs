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
    public class LocationEFConfiguration : EFConfigurationBase<LocationEntity>
    {
        public override void Configure(EntityTypeBuilder<LocationEntity> entity)
        {
            entity.ToTable("Plugin_Weixin_Location");
            base.Configure(entity);
            entity.Property(t => t.BDLatitude).HasColumnName("F_BDLatitude");
            entity.Property(t => t.BDLongitude).HasColumnName("F_BDLongitude");
            entity.Property(t => t.Business).HasColumnName("F_Business");
            entity.Property(t => t.City).HasColumnName("F_City");
            entity.Property(t => t.Label).HasColumnName("F_Label");
            entity.Property(t => t.Latitude).HasColumnName("F_Latitude");
            entity.Property(t => t.Longitude).HasColumnName("F_Longitude");
            entity.Property(t => t.WeiXinAppId).HasColumnName("F_MPId");
            entity.Property(t => t.Precision).HasColumnName("F_Precision");
            entity.Property(t => t.Province).HasColumnName("F_Province");
            entity.Property(t => t.Street).HasColumnName("F_Street");
            entity.Property(t => t.StreetNumber).HasColumnName("F_StreetNumber");
            entity.Property(t => t.WeiXinUserID).HasColumnName("F_WeiXinUserID");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
