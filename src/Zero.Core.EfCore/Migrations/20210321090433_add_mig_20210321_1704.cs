using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Zero.Core.EfCore.Migrations
{
    public partial class add_mig_20210321_1704 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_PhotoManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false, comment: "标题"),
                    Url = table.Column<string>(nullable: false, comment: "图片路径"),
                    PhotoClass = table.Column<string>(nullable: true, comment: "图片样式"),
                    Link = table.Column<string>(nullable: true, comment: "图片链接")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PhotoManager", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_PhotoManager");
           
        }
    }
}
