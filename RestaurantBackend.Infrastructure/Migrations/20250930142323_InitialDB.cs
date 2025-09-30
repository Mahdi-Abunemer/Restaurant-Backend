using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: new Guid("00704d0a-1580-459d-8066-fea40d3d3bb6"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "AverageRating", "Category", "CreateDateTime", "DeleteDate", "Description", "IsVegetarian", "ModifyDateTime", "Name", "Photo", "Price" },
                values: new object[] { new Guid("00704d0a-1580-459d-8066-fea40d3d3bb6"), 0f, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "SoGood", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Seif", "Http//sdfjsdfjks;df", 0.0 });
        }
    }
}
