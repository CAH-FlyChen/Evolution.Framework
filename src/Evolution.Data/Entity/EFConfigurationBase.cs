using Evolution.EFConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Data.Entity
{
    public class EFConfigurationBase<T> : DbEntityConfiguration<T> where T : EntityBase
    {
        public override void Configure(EntityTypeBuilder<T> entity)
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("F_Id");
            entity.Property(t => t.CreatorTime).HasColumnName("F_CreatorTime");
            entity.Property(t => t.CreatorUserId).HasColumnName("F_CreatorUserId");
            entity.Property(t => t.DeleteMark).HasColumnName("F_DeleteMark");
            entity.Property(t => t.DeleteTime).HasColumnName("F_DeleteTime");
            entity.Property(t => t.DeleteUserId).HasColumnName("F_DeleteUserId");
            entity.Property(t => t.Description).HasColumnName("F_Description");
            entity.Property(t => t.EnabledMark).HasColumnName("F_EnabledMark");
            entity.Property(t => t.LastModifyTime).HasColumnName("F_LastModifyTime");
            entity.Property(t => t.LastModifyUserId).HasColumnName("F_LastModifyUserId");
            entity.Property(t => t.SortCode).HasColumnName("F_SortCode");
        }

    }
}
