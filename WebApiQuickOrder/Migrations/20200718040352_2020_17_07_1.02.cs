using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApiQuickOrder.Migrations
{
    public partial class _2020_17_07_102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exp",
                table: "PaymentCards");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "PaymentCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "PaymentCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PaymentCards");

            migrationBuilder.AddColumn<DateTime>(
                name: "Exp",
                table: "PaymentCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
