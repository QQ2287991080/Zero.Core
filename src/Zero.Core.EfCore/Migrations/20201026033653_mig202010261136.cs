using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Core.EfCore.Migrations
{
    public partial class mig202010261136 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_Permission_Name",
                table: "T_Permission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_T_Permission_Name",
                table: "T_Permission",
                column: "Name",
                unique: true);
        }
    }
}
