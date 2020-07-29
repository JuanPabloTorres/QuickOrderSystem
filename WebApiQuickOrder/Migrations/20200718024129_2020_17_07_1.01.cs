using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApiQuickOrder.Migrations
{
    public partial class _2020_17_07_101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PaymentCards",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PaymentCards",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
