using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "formfields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Attributes = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    FieldType = table.Column<string>(type: "text", nullable: false),
                    Rules = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formfields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "formrecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    LanguageCode = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Arrival = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Departure = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Zip = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    FieldType = table.Column<string>(type: "text", nullable: false),
                    Attributes = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formrecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "Id", "Attributes", "FieldType", "Kind", "Name", "Required", "Rules" },
                values: new object[] { -1, new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, "text", "profile", "first name", true, new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" } });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "Address", "Arrival", "Attributes", "Birthdate", "City", "Country", "Departure", "Email", "FieldType", "FirstName", "Gender", "Kind", "LanguageCode", "LastName", "Nationality", "PhoneNumber", "SecondName", "Zip" },
                values: new object[] { -1, "123 Main St", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" }, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Philadephia", "USA", new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "jane@example.com", "text", "Jane", "Female", "profile", null, "Doe", null, "0714665512", "Doe", "12345" });

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Ask some personal questions for statistics", "Questionnaire" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formfields");

            migrationBuilder.DropTable(
                name: "formrecords");

            migrationBuilder.DropTable(
                name: "forms");
        }
    }
}
