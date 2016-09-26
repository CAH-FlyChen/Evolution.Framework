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
    public class RoleAuthorizeEFConfiguration : EFConfigurationBase<RoleAuthorizeEntity>
    {
        public override void Configure(EntityTypeBuilder<RoleAuthorizeEntity> entity)
        {
            entity.ToTable("Sys_RoleAuthorize");
            base.Configure(entity);
            entity.Property(t => t.ItemId).HasColumnName("F_ItemId");
            entity.Property(t => t.ItemType).HasColumnName("F_ItemType");
            entity.Property(t => t.ObjectId).HasColumnName("F_ObjectId");
            entity.Property(t => t.ObjectType).HasColumnName("F_ObjectType");
        }
    }
}
