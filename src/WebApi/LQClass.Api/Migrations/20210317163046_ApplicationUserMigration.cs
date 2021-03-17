using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class ApplicationUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("0430a920-5cea-4a14-894b-ca16754be29d"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserTokens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserRoles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserLogins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserClaims",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_ApplicationUserId",
                table: "AspNetUserTokens",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_ApplicationUserId",
                table: "AspNetUserLogins",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_ApplicationUserId",
                table: "AspNetUserClaims",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_ApplicationUserId",
                table: "AspNetUserClaims",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_ApplicationUserId",
                table: "AspNetUserLogins",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_ApplicationUserId",
                table: "AspNetUserTokens",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_ApplicationUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_ApplicationUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_ApplicationUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_ApplicationUserId",
                table: "AspNetUserClaims");

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

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserClaims");

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("0430a920-5cea-4a14-894b-ca16754be29d"), new DateTime(2021, 3, 17, 15, 43, 37, 736, DateTimeKind.Utc).AddTicks(9204), 0, null, "说明", null, null, null, null, 0m, 3.7999999999999998, "测试标题", 0, 0, null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("0430a920-5cea-4a14-894b-ca16754be29d"));
        }
    }
}
