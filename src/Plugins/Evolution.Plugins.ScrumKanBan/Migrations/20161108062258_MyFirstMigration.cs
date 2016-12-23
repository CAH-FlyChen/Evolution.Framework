using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Plugins.ScrumKanBan.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "P_KanBan_Project",
                columns: table => new
                {
                    F_Id = table.Column<string>(nullable: false),
                    F_CreateTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(nullable: true),
                    F_Description = table.Column<string>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_KanBan_Project", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "P_KanBan_StoryStatus",
                columns: table => new
                {
                    F_Id = table.Column<string>(nullable: false),
                    ButtonDisplayName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    F_CreateTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(nullable: true),
                    F_Description = table.Column<string>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_KanBan_StoryStatus", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "P_KanBan_UserStory",
                columns: table => new
                {
                    F_Id = table.Column<string>(nullable: false),
                    AssignToUserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    F_CreateTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(nullable: true),
                    F_Description = table.Column<string>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    ItemTypeCode = table.Column<int>(nullable: false),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(nullable: true),
                    ListID = table.Column<string>(nullable: true),
                    Point = table.Column<int>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_KanBan_UserStory", x => x.F_Id);
                    table.ForeignKey(
                        name: "FK_P_KanBan_UserStory_P_KanBan_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "P_KanBan_Project",
                        principalColumn: "F_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_P_KanBan_UserStory_ProjectId",
                table: "P_KanBan_UserStory",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "P_KanBan_StoryStatus");

            migrationBuilder.DropTable(
                name: "P_KanBan_UserStory");

            migrationBuilder.DropTable(
                name: "P_KanBan_Project");
        }
    }
}
