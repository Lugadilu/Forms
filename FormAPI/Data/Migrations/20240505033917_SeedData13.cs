using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormRecords",
                table: "FormRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormFields",
                table: "FormFields");

            migrationBuilder.RenameTable(
                name: "FormRecords",
                newName: "formrecords");

            migrationBuilder.RenameTable(
                name: "FormFields",
                newName: "formfields");

            migrationBuilder.AddPrimaryKey(
                name: "PK_formrecords",
                table: "formrecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_formfields",
                table: "formfields",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Attributes", "Rules" },
                values: new object[] { new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_formrecords",
                table: "formrecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_formfields",
                table: "formfields");

            migrationBuilder.RenameTable(
                name: "formrecords",
                newName: "FormRecords");

            migrationBuilder.RenameTable(
                name: "formfields",
                newName: "FormFields");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormRecords",
                table: "FormRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormFields",
                table: "FormFields",
                column: "Id");

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
                values: new object[] { new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Arrival", "Attributes", "Departure" },
                values: new object[] { new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
