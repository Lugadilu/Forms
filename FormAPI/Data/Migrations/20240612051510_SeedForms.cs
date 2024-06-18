using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "ask some personal questions for statistics", "questionnaire" });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "Id", "Attributes", "FieldType", "FormId", "Kind", "Name", "Required", "Rules" },
                values: new object[] { -1, "new attribute", "text", 1, "profile", "first name", true, "string" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "Address", "Arrival", "Birthdate", "City", "Country", "Departure", "Email", "FirstName", "FormId", "Gender", "LanguageCode", "LastName", "Nationality", "PhoneNumber", "SecondName", "Zip" },
                values: new object[] { -1, "123 Main St", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philadephia", "USA", new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "jane@example.com", "Jane", 1, "Female", null, "Doe", null, "0714665512", "Doe", "12345" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
