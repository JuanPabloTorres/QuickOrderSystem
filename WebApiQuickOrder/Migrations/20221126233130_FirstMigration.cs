using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiQuickOrder.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailValidations",
                columns: table => new
                {
                    EmailValidationId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ValidationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailValidations", x => x.EmailValidationId);
                });

            migrationBuilder.CreateTable(
                name: "ForgotPasswords",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPasswords", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Licences",
                columns: table => new
                {
                    LicenseId = table.Column<Guid>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LicenseHolderUserId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licences", x => x.LicenseId);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(nullable: false),
                    IsConnected = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(nullable: false),
                    FromStore = table.Column<Guid>(nullable: false),
                    RequestAnswer = table.Column<int>(nullable: false),
                    ToUser = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "Subcriptions",
                columns: table => new
                {
                    StripeSubCriptionID = table.Column<string>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StoreLicense = table.Column<Guid>(nullable: false),
                    StripeCustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcriptions", x => x.StripeSubCriptionID);
                });

            migrationBuilder.CreateTable(
                name: "usersConnecteds",
                columns: table => new
                {
                    HubConnectionID = table.Column<string>(nullable: false),
                    ConnecteDate = table.Column<DateTime>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersConnecteds", x => x.HubConnectionID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    IsValidUser = table.Column<bool>(nullable: false),
                    LoginId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    StripeUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                columns: table => new
                {
                    PaymentCardId = table.Column<Guid>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true),
                    Cvc = table.Column<string>(nullable: true),
                    HolderName = table.Column<string>(nullable: true),
                    Month = table.Column<string>(nullable: true),
                    StripeCardId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.PaymentCardId);
                    table.ForeignKey(
                        name: "FK_PaymentCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<Guid>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    PBKey = table.Column<string>(nullable: true),
                    SKKey = table.Column<string>(nullable: true),
                    StoreDescription = table.Column<string>(nullable: true),
                    StoreImage = table.Column<byte[]>(nullable: true),
                    StoreLicenceId = table.Column<Guid>(nullable: false),
                    StoreName = table.Column<string>(nullable: true),
                    StoreRegisterLicenseId = table.Column<Guid>(nullable: false),
                    StoreType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Stores_Licences_StoreRegisterLicenseId",
                        column: x => x.StoreRegisterLicenseId,
                        principalTable: "Licences",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    InventoryQuantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductImage = table.Column<byte[]>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoresWorkHours",
                columns: table => new
                {
                    WorkHourId = table.Column<Guid>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresWorkHours", x => x.WorkHourId);
                    table.ForeignKey(
                        name: "FK_StoresWorkHours_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeWorkHours",
                columns: table => new
                {
                    WorkHourId = table.Column<Guid>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    EmpId = table.Column<Guid>(nullable: false),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    WillWork = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    IsDisisble = table.Column<bool>(nullable: false),
                    BuyerId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    OrderType = table.Column<int>(nullable: false),
                    PrepareByEmployeeId = table.Column<Guid>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_PrepareByEmployeeId",
                        column: x => x.PrepareByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderProductId = table.Column<Guid>(nullable: false),
                    BuyerId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductIdReference = table.Column<Guid>(nullable: false),
                    ProductImage = table.Column<byte[]>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.OrderProductId);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkHours_EmpId",
                table: "EmployeeWorkHours",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrepareByEmployeeId",
                table: "Orders",
                column: "PrepareByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_UserId",
                table: "PaymentCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreRegisterLicenseId",
                table: "Stores",
                column: "StoreRegisterLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresWorkHours_StoreId",
                table: "StoresWorkHours",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LoginId",
                table: "Users",
                column: "LoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailValidations");

            migrationBuilder.DropTable(
                name: "EmployeeWorkHours");

            migrationBuilder.DropTable(
                name: "ForgotPasswords");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "StoresWorkHours");

            migrationBuilder.DropTable(
                name: "Subcriptions");

            migrationBuilder.DropTable(
                name: "usersConnecteds");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Licences");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
