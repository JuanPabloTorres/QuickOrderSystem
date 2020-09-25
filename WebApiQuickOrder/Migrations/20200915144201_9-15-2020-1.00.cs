using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class _9152020100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailValidations",
                columns: table => new
                {
                    EmailValidationId = table.Column<Guid>(nullable: false),
                    ValidationCode = table.Column<int>(nullable: false),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailValidations", x => x.EmailValidationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailValidations");
        }
    }
}
