using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _2020_27_100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EmployeeWorkHours",
                columns: table => new
                {
                    WorkHourId = table.Column<Guid>(nullable: false),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    EmpId = table.Column<Guid>(nullable: false),
                    Day = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkHours", x => x.WorkHourId);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkHours_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkHours_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeWorkHours");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "StoresWorkHours",
                type: "uniqueidentifier",
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
    }
}
