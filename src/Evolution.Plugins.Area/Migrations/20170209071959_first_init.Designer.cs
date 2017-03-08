using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Evolution.Plugins.Area.Modules;

namespace Evolution.Plugins.Area.Migrations
{
    [DbContext(typeof(AreaDbContext))]
    [Migration("20170209071959_first_init")]
    partial class first_init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evolution.Plugins.Area.Entities.AreaEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("F_Id");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("F_CreateTime");

                    b.Property<string>("CreatorUserId")
                        .HasColumnName("F_CreatorUserId");

                    b.Property<bool?>("DeleteMark")
                        .HasColumnName("F_DeleteMark");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnName("F_DeleteTime");

                    b.Property<string>("DeleteUserId")
                        .HasColumnName("F_DeleteUserId");

                    b.Property<string>("Description")
                        .HasColumnName("F_Description");

                    b.Property<string>("EnCode")
                        .HasColumnName("F_EnCode");

                    b.Property<bool?>("EnabledMark")
                        .HasColumnName("F_EnabledMark");

                    b.Property<string>("FullName")
                        .HasColumnName("F_FullName");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnName("F_LastModifyTime");

                    b.Property<string>("LastModifyUserId")
                        .HasColumnName("F_LastModifyUserId");

                    b.Property<int?>("Layers")
                        .HasColumnName("F_Layers");

                    b.Property<string>("ParentId")
                        .HasColumnName("F_ParentId");

                    b.Property<string>("SimpleSpelling")
                        .HasColumnName("F_SimpleSpelling");

                    b.Property<int?>("SortCode")
                        .HasColumnName("F_SortCode");

                    b.HasKey("Id");

                    b.ToTable("Sys_Area");
                });
        }
    }
}
