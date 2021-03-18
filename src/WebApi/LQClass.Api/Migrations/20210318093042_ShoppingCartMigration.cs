using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class ShoppingCartMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "254bee24-94a7-48fa-995a-ec1321d68cb4", "2c66d68b-9dc2-48d7-8e60-b91c5c0f20fc" });

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("f0c4df7d-fc10-4341-a596-8ce25c9c1ad3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "254bee24-94a7-48fa-995a-ec1321d68cb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c66d68b-9dc2-48d7-8e60-b91c5c0f20fc");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TouristRouteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShoppingCardId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPresent = table.Column<double>(type: "REAL", nullable: true),
                    ShoppingCartId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineItems_TouristRoutes_TouristRouteId",
                        column: x => x.TouristRouteId,
                        principalTable: "TouristRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8aa61819-4d97-4614-aa86-a7e0be3842e6", "83d1b0e3-2bb8-41af-a4da-86bbcb28c9a5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0afe48ff-7645-402a-b9ff-7eecf4b86a71", 0, null, "c234b31d-e2b5-4c80-8f69-0e0751261c50", "admin@lqclass.com", true, false, null, "ADMIN@LQCLASS.COM", "ADMIN@LQCLASS.COM", "AQAAAAEAACcQAAAAEE7bMvYGHca4Nm6T4Otrg///pvle8vQUwDW3mkTN0xOxHCTWDst8njixzPjRHIdbRw==", "15965893214", false, "94129053-3914-4a9b-ad1c-33208b274b91", false, "admin@lqclass.com" });

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("e8810f8b-608a-424a-bc5c-cbd77108569b"), new DateTime(2021, 3, 18, 9, 30, 41, 376, DateTimeKind.Utc).AddTicks(5830), 0, null, "说明", null, null, null, null, 0m, 3.7999999999999998, "测试标题", 0, 0, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[] { "8aa61819-4d97-4614-aa86-a7e0be3842e6", "0afe48ff-7645-402a-b9ff-7eecf4b86a71", null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("e8810f8b-608a-424a-bc5c-cbd77108569b"));

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_ShoppingCartId",
                table: "LineItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_TouristRouteId",
                table: "LineItems",
                column: "TouristRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8aa61819-4d97-4614-aa86-a7e0be3842e6", "0afe48ff-7645-402a-b9ff-7eecf4b86a71" });

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("e8810f8b-608a-424a-bc5c-cbd77108569b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8aa61819-4d97-4614-aa86-a7e0be3842e6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afe48ff-7645-402a-b9ff-7eecf4b86a71");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "254bee24-94a7-48fa-995a-ec1321d68cb4", "40f868ef-34e7-4414-a4f6-b3c492e64b4d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c66d68b-9dc2-48d7-8e60-b91c5c0f20fc", 0, null, "106fa0e1-581f-4ac0-9495-2e6e65153100", "admin@lqclass.com", true, false, null, "ADMIN@LQCLASS.COM", "ADMIN@LQCLASS.COM", "AQAAAAEAACcQAAAAEBizRIqGfLpNkl3k2iJEq7qjGjobKqkPGQee2w/QHjQLVRo4NyZ08GsqMyiDSJiZeA==", "15965893214", false, "c9ba2b56-56b3-4edf-a8b8-5cd1754daa73", false, "admin@lqclass.com" });

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("f0c4df7d-fc10-4341-a596-8ce25c9c1ad3"), new DateTime(2021, 3, 17, 16, 30, 45, 731, DateTimeKind.Utc).AddTicks(4464), 0, null, "说明", null, null, null, null, 0m, 3.7999999999999998, "测试标题", 0, 0, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[] { "254bee24-94a7-48fa-995a-ec1321d68cb4", "2c66d68b-9dc2-48d7-8e60-b91c5c0f20fc", null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("f0c4df7d-fc10-4341-a596-8ce25c9c1ad3"));
        }
    }
}
