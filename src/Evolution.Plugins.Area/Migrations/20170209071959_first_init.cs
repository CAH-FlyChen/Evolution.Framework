using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Plugins.Area.Migrations
{
    public partial class first_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Area",
                columns: table => new
                {
                    F_Id = table.Column<string>(nullable: false),
                    F_CreateTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(nullable: true),
                    F_Description = table.Column<string>(nullable: true),
                    F_EnCode = table.Column<string>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_FullName = table.Column<string>(nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_ParentId = table.Column<string>(nullable: true),
                    F_SimpleSpelling = table.Column<string>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Area", x => x.F_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Area");
        }
    }
}
