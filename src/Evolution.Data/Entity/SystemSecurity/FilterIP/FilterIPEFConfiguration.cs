/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Data.Entity;

namespace Evolution.EFConfiguration.SystemSecurity
{
    public class FilterIPEFConfiguration : EFConfigurationBase<FilterIPEntity>
    {
        public override void Configure(EntityTypeBuilder<FilterIPEntity> entity)
        {
            entity.ToTable("Sys_FilterIP");
            base.Configure(entity);
            entity.Property(t => t.StartIP).HasColumnName("F_StartIP");
            entity.Property(t => t.EndIP).HasColumnName("F_EndIP");
            entity.Property(t => t.Type).HasColumnName("F_Type");
        }

    }
}
