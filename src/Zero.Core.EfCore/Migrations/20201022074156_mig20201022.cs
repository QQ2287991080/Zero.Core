using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Core.EfCore.Migrations
{
    public partial class mig20201022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "T_User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_User_UserName",
                table: "T_User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_User_UserName",
                table: "T_User");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "T_User");
        }
    }
}
