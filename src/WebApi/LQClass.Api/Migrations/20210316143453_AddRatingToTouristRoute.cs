using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class AddRatingToTouristRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("7c495638-7122-4b0e-8e68-efae686876b3"));

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "TouristRoutes",
                type: "REAL",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureCity", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Rating", "Title", "TravelDays", "TripType", "UpdateTime" },
                values: new object[] { new Guid("dba1d911-c8ec-472a-88b7-16703368b749"), new DateTime(2021, 3, 16, 14, 34, 52, 857, DateTimeKind.Utc).AddTicks(6645), 0, null, "说明", null, null, null, null, 0m, 3.7999999999999998, "测试标题", 0, 0, null });

            migrationBuilder.UpdateData(
                table: "TouristRoutePictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "TouristRouteId",
                value: new Guid("dba1d911-c8ec-472a-88b7-16703368b749"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("dba1d911-c8ec-472a-88b7-16703368b749"));

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TouristRoutes");

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
    }
}
