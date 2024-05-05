using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Arrival", "Attributes", "Birthdate", "Departure" },
                values: new object[] { new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, DateTime.UtcNow, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Arrival", "Attributes", "Birthdate", "Departure" },
                values: new object[] { new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, DateTime.UtcNow, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No need to specify Down method for this migration
        }
    }


}
