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
    public class DbBackupEFConfiguration : EFConfigurationBase<DbBackupEntity>
    {
        public override void Configure(EntityTypeBuilder<DbBackupEntity> entity)
        {
            entity.ToTable("Sys_DbBackup");
            base.Configure(entity);
            entity.Property(t => t.BackupTime).HasColumnName("F_BackupTime");
            entity.Property(t => t.BackupType).HasColumnName("F_BackupType");
            entity.Property(t => t.DbName).HasColumnName("F_DbName");
            entity.Property(t => t.FileName).HasColumnName("F_FileName");
            entity.Property(t => t.FilePath).HasColumnName("F_FilePath");
            entity.Property(t => t.FileSize).HasColumnName("F_FileSize");
        }


    }
}
