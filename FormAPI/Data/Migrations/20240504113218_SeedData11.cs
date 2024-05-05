using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime?>(
        name: "Birthdate",
        table: "FormRecords",
        type: "timestamp with time zone",
        nullable: true, // Nullable to allow for null values
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "FormRecords",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "FormRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

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
                column: "Attributes",
                value: new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "Attributes",
                value: new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Departure",
                table: "FormRecords",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "FormRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Arrival",
                table: "FormRecords",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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
                column: "Attributes",
                value: new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                column: "Attributes",
                value: new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" });
        }
    }
}
