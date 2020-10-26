using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Core.EfCore.Migrations
{
    public partial class mig202020261011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dictionaries",
                table: "Dictionaries");

            migrationBuilder.RenameTable(
                name: "Dictionaries",
                newName: "T_Dictionaries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Dictionaries",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "T_Dictionaries",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Dictionaries",
                table: "T_Dictionaries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Dictionaries",
                table: "T_Dictionaries");

            migrationBuilder.RenameTable(
                name: "T_Dictionaries",
                newName: "Dictionaries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dictionaries",
                table: "Dictionaries",
                column: "Id");
        }
    }
}
