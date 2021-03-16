using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class InitTravalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("071e901a-1530-45ce-9af9-0b667c3fc455"));

            migrationBuilder.AddColumn<int>(
                name: "DepartureCity",
                table: "TouristRoutes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravelDays",
                table: "TouristRoutes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TripType",
                table: "TouristRoutes",
                type: "INTEGER",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("59dadee6-f5bb-442e-b419-5b012410fac1"));

            migrationBuilder.DropColumn(
                name: "DepartureCity",
                table: "TouristRoutes");

            migrationBuilder.DropColumn(
                name: "TravelDays",
                table: "TouristRoutes");

            migrationBuilder.DropColumn(
                name: "TripType",
                table: "TouristRoutes");

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("071e901a-1530-45ce-9af9-0b667c3fc455"), new DateTime(2021, 3, 16, 9, 1, 17, 253, DateTimeKind.Utc).AddTicks(988), null, "说明", null, null, null, null, 0m, "测试标题", null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("071e901a-1530-45ce-9af9-0b667c3fc455"));
        }
    }
}
