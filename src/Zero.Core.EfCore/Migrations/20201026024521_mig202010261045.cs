using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Core.EfCore.Migrations
{
    public partial class mig202010261045 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    IsAllow = table.Column<bool>(nullable: false),
                    Memo = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Permission_T_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "T_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Permission_Code",
                table: "T_Permission",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Permission_MenuId",
                table: "T_Permission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Permission_Name",
                table: "T_Permission",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Permission");
        }
    }
}
