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
    public class MenuButtonEFConfiguration : EFConfigurationBase<MenuButtonEntity>
    {
        public override void Configure(EntityTypeBuilder<MenuButtonEntity> entity)
        {
            entity.ToTable("Sys_MenuButton");
            base.Configure(entity);
            entity.Property(t => t.MenuId).HasColumnName("F_ModuleId");
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.Icon).HasColumnName("F_Icon");
            entity.Property(t => t.Location).HasColumnName("F_Location");
            entity.Property(t => t.UrlAddress).HasColumnName("F_UrlAddress");
            entity.Property(t => t.IsPublic).HasColumnName("F_IsPublic");
            entity.Property(t => t.AllowEdit).HasColumnName("F_AllowEdit");
            entity.Property(t => t.AllowDelete).HasColumnName("F_AllowDelete");
            entity.Property(t => t.JsEvent).HasColumnName("F_JsEvent");
            entity.Property(t => t.Split).HasColumnName("F_Split");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }

    }
}

