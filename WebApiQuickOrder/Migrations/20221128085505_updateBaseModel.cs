using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class updateBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Licences_StoreRegisterLicenseId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Users_UserId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_StoresWorkHours_Stores_StoreId",
                table: "StoresWorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Logins_LoginId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usersConnecteds",
                table: "usersConnecteds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcriptions",
                table: "Subcriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoresWorkHours",
                table: "StoresWorkHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_UserId",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCards_UserId",
                table: "PaymentCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Licences",
                table: "Licences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeWorkHours",
                table: "EmployeeWorkHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailValidations",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkHourId",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PaymentCardId",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDisisble",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PrepareByEmployeeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "WorkHourId",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmailValidationId",
                table: "EmailValidations");

            migrationBuilder.RenameColumn(
                name: "StripeSubCriptionID",
                table: "Subcriptions",
                newName: "StripeSubcriptionID");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "StoresWorkHours",
                newName: "StoreID");

            migrationBuilder.RenameIndex(
                name: "IX_StoresWorkHours_StoreId",
                table: "StoresWorkHours",
                newName: "IX_StoresWorkHours_StoreID");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Products",
                newName: "StoreID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                newName: "IX_Products_StoreID");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "OrderProducts",
                newName: "StoreID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderProducts",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderID");

            migrationBuilder.AlterColumn<string>(
                name: "HubConnectionID",
                table: "usersConnecteds",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "usersConnecteds",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "usersConnecteds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "usersConnecteds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "usersConnecteds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "StripeSubcriptionID",
                table: "Subcriptions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Subcriptions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Subcriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Subcriptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "Subcriptions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Subcriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "StoresWorkHours",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "StoresWorkHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "StoresWorkHours",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "StoresWorkHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Stores",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserID",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Stores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Stores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Stores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Requests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "Requests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "PaymentCards",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserID",
                table: "PaymentCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "PaymentCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "PaymentCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "PaymentCards",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "PaymentCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PrepareByID",
                table: "Orders",
                nullable: true);

           

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderID",
                table: "OrderProducts",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "OrderProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "OrderProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "OrderProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "OrderProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Logins",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Logins",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Logins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Logins",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Licences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Licences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Licences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "Licences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Licences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "EmployeeWorkHours",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "EmployeeWorkHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "EmployeeWorkHours",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "EmployeeWorkHours",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "EmployeeWorkHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Employees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeUserID",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "Employees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "EmailValidations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "EmailValidations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisasble",
                table: "EmailValidations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreID",
                table: "EmailValidations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "EmailValidations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_usersConnecteds",
                table: "usersConnecteds",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcriptions",
                table: "Subcriptions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoresWorkHours",
                table: "StoresWorkHours",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Licences",
                table: "Licences",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeWorkHours",
                table: "EmployeeWorkHours",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailValidations",
                table: "EmailValidations",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AppUserID",
                table: "Stores",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_AppUserID",
                table: "PaymentCards",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrepareByID",
                table: "Orders",
                column: "PrepareByID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeUserID",
                table: "Employees",
                column: "EmployeeUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_EmployeeUserID",
                table: "Employees",
                column: "EmployeeUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderID",
                table: "OrderProducts",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_PrepareByID",
                table: "Orders",
                column: "PrepareByID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCards_Users_AppUserID",
                table: "PaymentCards",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreID",
                table: "Products",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Users_AppUserID",
                table: "Stores",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Licences_StoreRegisterLicenseId",
                table: "Stores",
                column: "StoreRegisterLicenseId",
                principalTable: "Licences",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoresWorkHours_Stores_StoreID",
                table: "StoresWorkHours",
                column: "StoreID",
                principalTable: "Stores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Logins_LoginId",
                table: "Users",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_EmployeeUserID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderID",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_PrepareByID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCards_Users_AppUserID",
                table: "PaymentCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Users_AppUserID",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Licences_StoreRegisterLicenseId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_StoresWorkHours_Stores_StoreID",
                table: "StoresWorkHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Logins_LoginId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usersConnecteds",
                table: "usersConnecteds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcriptions",
                table: "Subcriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoresWorkHours",
                table: "StoresWorkHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AppUserID",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCards_AppUserID",
                table: "PaymentCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PrepareByID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Licences",
                table: "Licences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeWorkHours",
                table: "EmployeeWorkHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeUserID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailValidations",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "usersConnecteds");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "usersConnecteds");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "usersConnecteds");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "usersConnecteds");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "StoresWorkHours");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PrepareByID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "EmployeeWorkHours");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeUserID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "IsDisasble",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "EmailValidations");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "EmailValidations");

            migrationBuilder.RenameColumn(
                name: "StripeSubcriptionID",
                table: "Subcriptions",
                newName: "StripeSubCriptionID");

            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "StoresWorkHours",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_StoresWorkHours_StoreID",
                table: "StoresWorkHours",
                newName: "IX_StoresWorkHours_StoreId");

            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "Products",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_StoreID",
                table: "Products",
                newName: "IX_Products_StoreId");

            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "OrderProducts",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "OrderProducts",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderID",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "HubConnectionID",
                table: "usersConnecteds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StripeSubCriptionID",
                table: "Subcriptions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkHourId",
                table: "StoresWorkHours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Stores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentCardId",
                table: "PaymentCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisisble",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PrepareByEmployeeId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "OrderProducts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LoginId",
                table: "Logins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Licences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkHourId",
                table: "EmployeeWorkHours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmailValidationId",
                table: "EmailValidations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_usersConnecteds",
                table: "usersConnecteds",
                column: "HubConnectionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcriptions",
                table: "Subcriptions",
                column: "StripeSubCriptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoresWorkHours",
                table: "StoresWorkHours",
                column: "WorkHourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards",
                column: "PaymentCardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "OrderProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "LoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Licences",
                table: "Licences",
                column: "LicenseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeWorkHours",
                table: "EmployeeWorkHours",
                column: "WorkHourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailValidations",
                table: "EmailValidations",
                column: "EmailValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_UserId",
                table: "PaymentCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrepareByEmployeeId",
                table: "Orders",
                column: "PrepareByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkHours_Employees_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_PrepareByEmployeeId",
                table: "Orders",
                column: "PrepareByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCards_Users_UserId",
                table: "PaymentCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Licences_StoreRegisterLicenseId",
                table: "Stores",
                column: "StoreRegisterLicenseId",
                principalTable: "Licences",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Users_UserId",
                table: "Stores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoresWorkHours_Stores_StoreId",
                table: "StoresWorkHours",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Logins_LoginId",
                table: "Users",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "LoginId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
