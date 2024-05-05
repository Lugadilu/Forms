using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { null, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, null });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { null, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 1, 7, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 10, 7, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 1, 7, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 10, 7, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
