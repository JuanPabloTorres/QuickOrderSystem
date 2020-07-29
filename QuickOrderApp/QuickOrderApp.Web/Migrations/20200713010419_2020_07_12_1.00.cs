using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_07_12_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "Products");
        }
    }
}
