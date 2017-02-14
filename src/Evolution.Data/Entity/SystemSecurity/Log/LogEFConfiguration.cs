﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemSecurity
{
    public class LogEFConfiguration : EFConfigurationBase<LogEntity>
    {
        public override void Configure(EntityTypeBuilder<LogEntity> entity)
        {
            entity.ToTable("Sys_Log");
            base.Configure(entity);
            entity.Property(t => t.Account).HasColumnName("F_Account");
            entity.Property(t => t.Date).HasColumnName("F_Date");
            entity.Property(t => t.IPAddress).HasColumnName("F_IPAddress");
            entity.Property(t => t.IPAddressName).HasColumnName("F_IPAddressName");
            entity.Property(t => t.ModuleId).HasColumnName("F_ModuleId");
            entity.Property(t => t.ModuleName).HasColumnName("F_ModuleName");
            entity.Property(t => t.NickName).HasColumnName("F_NickName");
            entity.Property(t => t.Result).HasColumnName("F_Result");
            entity.Property(t => t.Type).HasColumnName("F_Type");
            entity.Property(t => t.TenantId).HasColumnName("F_TenantId");
        }
    }
}
