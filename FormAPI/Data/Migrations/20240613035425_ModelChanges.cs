using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Arrival", "Departure" },
                values: new object[] { new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Arrival", "Departure" },
                values: new object[] { new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
