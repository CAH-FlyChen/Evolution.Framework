using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Evolution.Plugins.ScrumKanBan.Models;

namespace Evolution.Plugins.ScrumKanBan.Migrations
{
    [DbContext(typeof(KanBanDbContext))]
    [Migration("20161108062258_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evolution.Plugins.ScrumKanBan.Models.ProjectEntity", b =>
                {
                    b.Property<string>("Id")
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

                    b.Property<bool?>("EnabledMark")
                        .HasColumnName("F_EnabledMark");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnName("F_LastModifyTime");

                    b.Property<string>("LastModifyUserId")
                        .HasColumnName("F_LastModifyUserId");

                    b.Property<string>("Name");

                    b.Property<int?>("SortCode")
                        .HasColumnName("F_SortCode");

                    b.HasKey("Id");

                    b.ToTable("P_KanBan_Project");
                });

            modelBuilder.Entity("Evolution.Plugins.ScrumKanBan.Models.StoryStatusEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("F_Id");

                    b.Property<string>("ButtonDisplayName");

                    b.Property<string>("Code");

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

                    b.Property<bool?>("EnabledMark")
                        .HasColumnName("F_EnabledMark");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnName("F_LastModifyTime");

                    b.Property<string>("LastModifyUserId")
                        .HasColumnName("F_LastModifyUserId");

                    b.Property<int?>("SortCode")
                        .HasColumnName("F_SortCode");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("P_KanBan_StoryStatus");
                });

            modelBuilder.Entity("Evolution.Plugins.ScrumKanBan.Models.UserStoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("F_Id");

                    b.Property<string>("AssignToUserId");

                    b.Property<string>("Content");

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

                    b.Property<bool?>("EnabledMark")
                        .HasColumnName("F_EnabledMark");

                    b.Property<int>("ItemTypeCode");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnName("F_LastModifyTime");

                    b.Property<string>("LastModifyUserId")
                        .HasColumnName("F_LastModifyUserId");

                    b.Property<string>("ListID");

                    b.Property<int>("Point");

                    b.Property<string>("ProjectId");

                    b.Property<int?>("SortCode")
                        .HasColumnName("F_SortCode");

                    b.Property<string>("StatusCode");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("P_KanBan_UserStory");
                });

            modelBuilder.Entity("Evolution.Plugins.ScrumKanBan.Models.UserStoryEntity", b =>
                {
                    b.HasOne("Evolution.Plugins.ScrumKanBan.Models.ProjectEntity", "Project")
                        .WithMany("Stories")
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
