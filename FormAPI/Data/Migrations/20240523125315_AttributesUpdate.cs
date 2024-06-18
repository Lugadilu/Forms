using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttributesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Attributes",
                table: "formfields",
                type: "text",
                nullable: false,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore");

            migrationBuilder.UpdateData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { "new attribute", new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Attributes",
                table: "formfields",
                type: "hstore",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
