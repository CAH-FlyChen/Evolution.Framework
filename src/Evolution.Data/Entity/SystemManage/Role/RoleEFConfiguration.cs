﻿/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemManage
{
    public class RoleEFConfiguration : EFConfigurationBase<RoleEntity>
    {
        public override void Configure(EntityTypeBuilder<RoleEntity> entity)
        {
            entity.ToTable("Sys_Role");
            base.Configure(entity);
            entity.Property(t => t.AllowDelete).HasColumnName("F_AllowDelete");
            entity.Property(t => t.AllowEdit).HasColumnName("F_AllowEdit");
            entity.Property(t => t.Category).HasColumnName("F_Category");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.OrganizeId).HasColumnName("F_OrganizeId");
            entity.Property(t => t.Type).HasColumnName("F_Type");
        }


    }
}
