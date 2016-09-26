/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemManage
{
    public class MenuEFConfiguration : EFConfigurationBase<MenuEntity>
    {
        public override void Configure(EntityTypeBuilder<MenuEntity> entity)
        {
            entity.ToTable("Sys_Menu");
            base.Configure(entity);
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.Icon).HasColumnName("F_Icon");
            entity.Property(t => t.UrlAddress).HasColumnName("F_UrlAddress");
            entity.Property(t => t.Target).HasColumnName("F_Target");
            entity.Property(t => t.IsMenu).HasColumnName("F_IsMenu");
            entity.Property(t => t.IsExpand).HasColumnName("F_IsExpand");
            entity.Property(t => t.IsPublic).HasColumnName("F_IsPublic");
            entity.Property(t => t.AllowEdit).HasColumnName("F_AllowEdit");
            entity.Property(t => t.AllowDelete).HasColumnName("F_AllowDelete");

        }

    }
}
