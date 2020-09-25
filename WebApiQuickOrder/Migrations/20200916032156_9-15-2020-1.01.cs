using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class _9152020101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidUser",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidUser",
                table: "Users");
        }
    }
}
