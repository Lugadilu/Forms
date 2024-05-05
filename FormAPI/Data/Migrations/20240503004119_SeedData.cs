using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormFields",
                columns: new[] { "Id", "Attributes", "FieldType", "Kind", "Name", "Required", "Rules" },
                values: new object[,]
                {
                    { 1, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, "text", "profile", "Field 1", true, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } },
                    { 2, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, "text", "address", "Field 2", false, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } },
                    { 3, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, "text", "registration", "Field 3", false, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } }
                });

            migrationBuilder.InsertData(
                table: "FormRecords",
                columns: new[] { "Id", "Address", "Arrival", "Attributes", "Birthdate", "City", "Country", "Departure", "Email", "FieldType", "FirstName", "Gender", "Kind", "LanguageCode", "LastName", "Nationality", "PhoneNumber", "SecondName", "Zip" },
                values: new object[,]
                {
                    { 1, "123 Main St", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New oke", "USA", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "text", "John", "Male", "profile", null, "Smith", null, 1234567890, "Doe", "12345" },
                    { 2, "123 Main St", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philadephia", "USA", new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", "text", "Jane", "Female", "profile", null, "Doe", null, 776543210, "Doe", "12345" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormFields",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormRecords",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
