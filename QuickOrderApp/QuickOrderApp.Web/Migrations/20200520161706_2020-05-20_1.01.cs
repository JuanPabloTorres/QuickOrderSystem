using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickOrderApp.Web.Migrations
{
    public partial class _20200520_101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreRegisterLicense",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreRegisterLicenseId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StoreLicences",
                columns: table => new
                {
                    LicenseId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLicences", x => x.LicenseId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreRegisterLicenseId",
                table: "Users",
                column: "StoreRegisterLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_StoreLicences_StoreRegisterLicenseId",
                table: "Users",
                column: "StoreRegisterLicenseId",
                principalTable: "StoreLicences",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_StoreLicences_StoreRegisterLicenseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "StoreLicences");

            migrationBuilder.DropIndex(
                name: "IX_Users_StoreRegisterLicenseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StoreRegisterLicenseId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreRegisterLicense",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
