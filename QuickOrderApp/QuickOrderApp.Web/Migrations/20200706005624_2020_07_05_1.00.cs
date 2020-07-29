using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_07_05_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours");

            migrationBuilder.AddColumn<string>(
                name: "StoreDescription",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrepareByEmployeeId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrepareByEmployeeId",
                table: "Orders",
                column: "PrepareByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_PrepareByEmployeeId",
                table: "Orders",
                column: "PrepareByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreDescription",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
