using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CementSaleManagement.Migrations
{
    public partial class Intital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMasters_RoleMasters_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    UserMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerMasters_UserMasters_UserMasterId",
                        column: x => x.UserMasterId,
                        principalTable: "UserMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierName = table.Column<string>(type: "TEXT", nullable: true),
                    Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    UserMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierMasters_UserMasters_UserMasterId",
                        column: x => x.UserMasterId,
                        principalTable: "UserMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfJoining = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Dob = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    UserMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerMasters_UserMasters_UserMasterId",
                        column: x => x.UserMasterId,
                        principalTable: "UserMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMasters_CustomerMasters_CustomerMasterId",
                        column: x => x.CustomerMasterId,
                        principalTable: "CustomerMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMasters_SupplierMasters_SupplierMasterId",
                        column: x => x.SupplierMasterId,
                        principalTable: "SupplierMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SupplierMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseMasters_SupplierMasters_SupplierMasterId",
                        column: x => x.SupplierMasterId,
                        principalTable: "SupplierMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderCancellationMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCancellationMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCancellationMasters_OrderMasters_OrderMasterId",
                        column: x => x.OrderMasterId,
                        principalTable: "OrderMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderMasters_OrderMasterId",
                        column: x => x.OrderMasterId,
                        principalTable: "OrderMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductMasters_ProductMasterId",
                        column: x => x.ProductMasterId,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PruchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PruchaseDetails_ProductMasters_ProductMasterId",
                        column: x => x.ProductMasterId,
                        principalTable: "ProductMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PruchaseDetails_PurchaseMasters_PurchaseMasterId",
                        column: x => x.PurchaseMasterId,
                        principalTable: "PurchaseMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseCancellationMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PurchaseMasterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseCancellationMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseCancellationMasters_PurchaseMasters_PurchaseMasterId",
                        column: x => x.PurchaseMasterId,
                        principalTable: "PurchaseMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoleMasters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleMasters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Worker" });

            migrationBuilder.InsertData(
                table: "RoleMasters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Supplier" });

            migrationBuilder.InsertData(
                table: "RoleMasters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Customer" });

            migrationBuilder.InsertData(
                table: "UserMasters",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone", "RoleId" },
                values: new object[] { 1, "admin@admin.com", "Admin", "Admin", "7898765467", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMasters_UserMasterId",
                table: "CustomerMasters",
                column: "UserMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCancellationMasters_OrderMasterId",
                table: "OrderCancellationMasters",
                column: "OrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderMasterId",
                table: "OrderDetails",
                column: "OrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductMasterId",
                table: "OrderDetails",
                column: "ProductMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_CustomerMasterId",
                table: "OrderMasters",
                column: "CustomerMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMasters_SupplierMasterId",
                table: "ProductMasters",
                column: "SupplierMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PruchaseDetails_ProductMasterId",
                table: "PruchaseDetails",
                column: "ProductMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PruchaseDetails_PurchaseMasterId",
                table: "PruchaseDetails",
                column: "PurchaseMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseCancellationMasters_PurchaseMasterId",
                table: "PurchaseCancellationMasters",
                column: "PurchaseMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMasters_SupplierMasterId",
                table: "PurchaseMasters",
                column: "SupplierMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierMasters_UserMasterId",
                table: "SupplierMasters",
                column: "UserMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMasters_RoleId",
                table: "UserMasters",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerMasters_UserMasterId",
                table: "WorkerMasters",
                column: "UserMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderCancellationMasters");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PruchaseDetails");

            migrationBuilder.DropTable(
                name: "PurchaseCancellationMasters");

            migrationBuilder.DropTable(
                name: "WorkerMasters");

            migrationBuilder.DropTable(
                name: "OrderMasters");

            migrationBuilder.DropTable(
                name: "ProductMasters");

            migrationBuilder.DropTable(
                name: "PurchaseMasters");

            migrationBuilder.DropTable(
                name: "CustomerMasters");

            migrationBuilder.DropTable(
                name: "SupplierMasters");

            migrationBuilder.DropTable(
                name: "UserMasters");

            migrationBuilder.DropTable(
                name: "RoleMasters");
        }
    }
}
