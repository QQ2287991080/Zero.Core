using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Core.EfCore.Migrations
{
    public partial class mig202012171646 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CronExpression",
                table: "T_Jobs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TriggerInterval",
                table: "T_Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriggerType",
                table: "T_Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CronExpression",
                table: "T_Jobs");

            migrationBuilder.DropColumn(
                name: "TriggerInterval",
                table: "T_Jobs");

            migrationBuilder.DropColumn(
                name: "TriggerType",
                table: "T_Jobs");
        }
    }
}
