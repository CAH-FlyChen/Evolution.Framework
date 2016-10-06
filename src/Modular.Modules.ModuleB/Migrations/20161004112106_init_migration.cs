using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Modular.Modules.ModuleB.Migrations
{
    public partial class init_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoModels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatorTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    DeleteMark = table.Column<bool>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    DeleteUserId = table.Column<string>(nullable: true),
                    DemoStr = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EnabledMark = table.Column<bool>(nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifyUserId = table.Column<string>(nullable: true),
                    SortCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoModels");
        }
    }
}
