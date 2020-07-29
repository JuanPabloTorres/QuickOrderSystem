using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_06_03_103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestAnswer",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestAnswer",
                table: "Requests");
        }
    }
}
