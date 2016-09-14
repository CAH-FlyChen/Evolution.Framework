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
    public class OrganizeEFConfiguration : EFConfigurationBase<OrganizeEntity>
    {
        public override void Configure(EntityTypeBuilder<OrganizeEntity> entity)
        {
            entity.ToTable("Sys_Organize");
            base.Configure(entity);
            entity.Property(t => t.Address).HasColumnName("F_Address");
            entity.Property(t => t.AllowDelete).HasColumnName("F_AllowDelete");
            entity.Property(t => t.AllowEdit).HasColumnName("F_AllowEdit");
            entity.Property(t => t.AreaId).HasColumnName("F_AreaId");
            entity.Property(t => t.CategoryId).HasColumnName("F_CategoryId");
            entity.Property(t => t.Email).HasColumnName("F_Email");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.Fax).HasColumnName("F_Fax");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.ManagerId).HasColumnName("F_ManagerId");
            entity.Property(t => t.MobilePhone).HasColumnName("F_MobilePhone");
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.ShortName).HasColumnName("F_ShortName");
            entity.Property(t => t.TelePhone).HasColumnName("F_TelePhone");
            entity.Property(t => t.WeChat).HasColumnName("F_WeChat");
        }
    }
}
