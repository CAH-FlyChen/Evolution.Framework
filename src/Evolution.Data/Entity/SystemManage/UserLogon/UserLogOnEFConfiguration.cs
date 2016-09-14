/*******************************************************************************
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
    public class UserLogOnEFConfiguration : EFConfigurationBase<UserLogOnEntity>
    {
        public override void Configure(EntityTypeBuilder<UserLogOnEntity> entity)
        {
            entity.ToTable("Sys_UserLogOn");
            base.Configure(entity);
            entity.Property(t => t.AllowEndTime).HasColumnName("F_AllowEndTime");
            entity.Property(t => t.AllowStartTime).HasColumnName("F_AllowStartTime");
            entity.Property(t => t.AnswerQuestion).HasColumnName("F_AnswerQuestion");
            entity.Property(t => t.ChangePasswordDate).HasColumnName("F_ChangePasswordDate");
            entity.Property(t => t.CheckIPAddress).HasColumnName("F_CheckIPAddress");
            entity.Property(t => t.FirstVisitTime).HasColumnName("F_FirstVisitTime");
            entity.Property(t => t.Language).HasColumnName("F_Language");
            entity.Property(t => t.LastVisitTime).HasColumnName("F_LastVisitTime");
            entity.Property(t => t.LockEndDate).HasColumnName("F_LockEndDate");
            entity.Property(t => t.LockStartDate).HasColumnName("F_LockStartDate");
            entity.Property(t => t.LogOnCount).HasColumnName("F_LogOnCount");
            entity.Property(t => t.MultiUserLogin).HasColumnName("F_MultiUserLogin");
            entity.Property(t => t.PreviousVisitTime).HasColumnName("F_PreviousVisitTime");
            entity.Property(t => t.Question).HasColumnName("F_Question");
            entity.Property(t => t.Theme).HasColumnName("F_Theme");
            entity.Property(t => t.UserId).HasColumnName("F_UserId");
            entity.Property(t => t.UserOnLine).HasColumnName("F_UserOnLine");
            entity.Property(t => t.UserPassword).HasColumnName("F_UserPassword");
            entity.Property(t => t.UserSecretkey).HasColumnName("F_UserSecretkey");
        }

    }
}
