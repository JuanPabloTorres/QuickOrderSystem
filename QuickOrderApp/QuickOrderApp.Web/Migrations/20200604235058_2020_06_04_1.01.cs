using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_06_04_101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Employees");
        }
    }
}
