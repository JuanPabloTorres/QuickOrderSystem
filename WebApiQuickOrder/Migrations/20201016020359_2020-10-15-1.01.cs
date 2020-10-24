using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class _20201015101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Requests");
        }
    }
}
