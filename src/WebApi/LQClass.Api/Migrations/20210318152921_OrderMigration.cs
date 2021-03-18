using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "LineItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDateUTC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransactionMetadata = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0efb0f9a-6afe-4e4a-85b1-d3e0c7040343", "d31cdbe4-15ab-459c-9351-ce0fe233150e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "417da887-e6bb-447b-84cc-6c677d3d9f78", 0, null, "9ce712fe-d939-43b4-9fbc-7274249d42f6", "admin@lqclass.com", true, false, null, "ADMIN@LQCLASS.COM", "ADMIN@LQCLASS.COM", "AQAAAAEAACcQAAAAEEsCWCn2W250kf+m5/AoaW6GuwPOJkjAIEgReczHQW5xTBF73kQgkkfZPT8AKXcpcw==", "15965893214", false, "541499a1-0da7-4506-9492-b0e0b698286b", false, "admin@lqclass.com" });

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("36f45915-0632-4ade-a971-9bbd2742e914"), new DateTime(2021, 3, 18, 15, 29, 20, 441, DateTimeKind.Utc).AddTicks(354), 0, null, "说明", null, null, null, null, 0m, 3.7999999999999998, "测试标题", 0, 0, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[] { "0efb0f9a-6afe-4e4a-85b1-d3e0c7040343", "417da887-e6bb-447b-84cc-6c677d3d9f78", null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("36f45915-0632-4ade-a971-9bbd2742e914"));

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_OrderId",
                table: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_OrderId",
                table: "LineItems");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0efb0f9a-6afe-4e4a-85b1-d3e0c7040343", "417da887-e6bb-447b-84cc-6c677d3d9f78" });

            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("36f45915-0632-4ade-a971-9bbd2742e914"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0efb0f9a-6afe-4e4a-85b1-d3e0c7040343");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "417da887-e6bb-447b-84cc-6c677d3d9f78");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LineItems");

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
        }
    }
}
