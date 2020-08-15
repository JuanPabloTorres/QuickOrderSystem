using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class _20200813100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDestinationKey",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "PBKey",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKKey",
                table: "Stores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PBKey",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "SKKey",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "PaymentDestinationKey",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
