using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_25_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "StoresWorkHours",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoresWorkHours_EmployeeId",
                table: "StoresWorkHours",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoresWorkHours_Employees_EmployeeId",
                table: "StoresWorkHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoresWorkHours_Employees_EmployeeId",
                table: "StoresWorkHours");

            migrationBuilder.DropIndex(
                name: "IX_StoresWorkHours_EmployeeId",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "StoresWorkHours");
        }
    }
}
