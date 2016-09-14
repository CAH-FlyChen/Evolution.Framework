/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Data.Entity;
using Evolution.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evolution.EFConfiguration.SystemManage
{
    public class AreaEFConfiguration : EFConfigurationBase<AreaEntity>
    {
        public override void Configure(EntityTypeBuilder<AreaEntity> entity)
        {
            entity.ToTable("Sys_Area");
            base.Configure(entity);
            entity.Property(t => t.ParentId).HasColumnName("F_ParentId");
            entity.Property(t => t.Layers).HasColumnName("F_Layers");
            entity.Property(t => t.EnCode).HasColumnName("F_EnCode");
            entity.Property(t => t.FullName).HasColumnName("F_FullName");
            entity.Property(t => t.SimpleSpelling).HasColumnName("F_SimpleSpelling");
        }
    }

}
