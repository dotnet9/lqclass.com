using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class InitExtFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("59dadee6-f5bb-442e-b419-5b012410fac1"));

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("7c495638-7122-4b0e-8e68-efae686876b3"), new DateTime(2021, 3, 16, 14, 16, 10, 505, DateTimeKind.Utc).AddTicks(1555), 0, null, "说明", null, null, null, null, 0m, "测试标题", 0, 0, null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("7c495638-7122-4b0e-8e68-efae686876b3"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("7c495638-7122-4b0e-8e68-efae686876b3"));

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("59dadee6-f5bb-442e-b419-5b012410fac1"), new DateTime(2021, 3, 16, 10, 6, 47, 510, DateTimeKind.Utc).AddTicks(6648), null, null, "说明", null, null, null, null, 0m, "测试标题", null, null, null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("59dadee6-f5bb-442e-b419-5b012410fac1"));
        }
    }
}
