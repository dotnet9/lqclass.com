using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LQClass.Api.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("0b227792-5d63-4b54-ad4d-a4a7613650cb"), new DateTime(2021, 3, 16, 5, 59, 12, 910, DateTimeKind.Utc).AddTicks(5543), null, "说明", null, null, null, null, 0m, "测试标题", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("0b227792-5d63-4b54-ad4d-a4a7613650cb"));
        }
    }
}
