using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "formrecords");

            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "formrecords");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "formrecords");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "formrecords",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rules",
                table: "formfields",
                type: "text",
                nullable: false,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore");

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "formfields",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_formrecords_FormId",
                table: "formrecords",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_formfields_FormId",
                table: "formfields",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_formfields_forms_FormId",
                table: "formfields",
                column: "FormId",
                principalTable: "forms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_formrecords_forms_FormId",
                table: "formrecords",
                column: "FormId",
                principalTable: "forms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formfields_forms_FormId",
                table: "formfields");

            migrationBuilder.DropForeignKey(
                name: "FK_formrecords_forms_FormId",
                table: "formrecords");

            migrationBuilder.DropIndex(
                name: "IX_formrecords_FormId",
                table: "formrecords");

            migrationBuilder.DropIndex(
                name: "IX_formfields_FormId",
                table: "formfields");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "formrecords");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "formfields");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "Attributes",
                table: "formrecords",
                type: "hstore",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "FieldType",
                table: "formrecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "formrecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Rules",
                table: "formfields",
                type: "hstore",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "Id", "Attributes", "FieldType", "Kind", "Name", "Required", "Rules" },
                values: new object[] { -1, "new attribute", "text", "profile", "first name", true, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "Address", "Arrival", "Attributes", "Birthdate", "City", "Country", "Departure", "Email", "FieldType", "FirstName", "Gender", "Kind", "LanguageCode", "LastName", "Nationality", "PhoneNumber", "SecondName", "Zip" },
                values: new object[] { -1, "123 Main St", new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philadephia", "USA", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "jane@example.com", "text", "Jane", "Female", "profile", null, "Doe", null, "0714665512", "Doe", "12345" });

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Ask some personal questions for statistics", "Questionnaire" });
        }
    }
}
