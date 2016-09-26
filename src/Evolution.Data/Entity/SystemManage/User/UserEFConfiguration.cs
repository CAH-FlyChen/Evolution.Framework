/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data.Entity;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Evolution.EFConfiguration.SystemManage
{
    public class UserEFConfiguration : EFConfigurationBase<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("Sys_User");
            base.Configure(entity);
            entity.Property(t => t.Account).HasColumnName("F_Account");
            entity.Property(t => t.Birthday).HasColumnName("F_Birthday");
            entity.Property(t => t.DepartmentId).HasColumnName("F_DepartmentId");
            entity.Property(t => t.DutyId).HasColumnName("F_DutyId");
            entity.Property(t => t.Email).HasColumnName("F_Email");
            entity.Property(t => t.Gender).HasColumnName("F_Gender");
            entity.Property(t => t.HeadIcon).HasColumnName("F_HeadIcon");
            entity.Property(t => t.IsAdministrator).HasColumnName("F_IsAdministrator");
            entity.Property(t => t.ManagerId).HasColumnName("F_ManagerId");
            entity.Property(t => t.MobilePhone).HasColumnName("F_MobilePhone");
            entity.Property(t => t.NickName).HasColumnName("F_NickName");
            entity.Property(t => t.OrganizeId).HasColumnName("F_OrganizeId");
            entity.Property(t => t.RealName).HasColumnName("F_RealName");
            entity.Property(t => t.RoleId).HasColumnName("F_RoleId");
            entity.Property(t => t.SecurityLevel).HasColumnName("F_SecurityLevel");
            entity.Property(t => t.Signature).HasColumnName("F_Signature");
            entity.Property(t => t.WeChat).HasColumnName("F_WeChat");
        }
    }

}
