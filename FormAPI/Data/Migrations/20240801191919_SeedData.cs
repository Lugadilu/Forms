using System;
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
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("8a6c19d3-59b7-4e1f-bb08-3942db2226d7"), "A survey to collect employee opinions.", "Employee Survey" },
                    { new Guid("909f3db4-f61e-4fed-a1cd-b93d05cf1341"), "A form to collect customer feedback.", "Customer Feedback Form" }
                });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[,]
                {
                    { new Guid("4c988889-813a-4723-8407-45717fb8c82d"), new DateTime(2024, 8, 1, 19, 19, 18, 882, DateTimeKind.Utc).AddTicks(9305), "{\"firstName\": \"Yael\", \"lastName\": \"Doe\"}", new Guid("8a6c19d3-59b7-4e1f-bb08-3942db2226d7") },
                    { new Guid("8ef9d581-4bf2-4c9d-ae1f-b592db155a50"), new DateTime(2024, 8, 1, 19, 19, 18, 882, DateTimeKind.Utc).AddTicks(9302), "{\"firstName\": \"Michelle\", \"lastName\": \"Smith\"}", new Guid("909f3db4-f61e-4fed-a1cd-b93d05cf1341") }
                });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[,]
                {
                    { new Guid("6c85514a-7ab5-4a99-ac20-4776d0668aed"), new Guid("8a6c19d3-59b7-4e1f-bb08-3942db2226d7") },
                    { new Guid("91da9b52-cb82-49d2-8373-027e813f5740"), new Guid("909f3db4-f61e-4fed-a1cd-b93d05cf1341") }
                });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "Id", "Attributes", "FieldType", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[,]
                {
                    { new Guid("0147cdb1-e997-41a7-87c3-5cff34d7a8b9"), "{}", "firstName", "profile", "profileFirstName", new Guid("91da9b52-cb82-49d2-8373-027e813f5740"), false, "{\"minLength\": 2, \"maxLength\": 128}" },
                    { new Guid("92b5dce4-c941-4c21-86b1-4435142bc480"), "{}", "lastName", "profile", "profileLastName", new Guid("6c85514a-7ab5-4a99-ac20-4776d0668aed"), false, "{\"minLength\": 2, \"maxLength\": 128}" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: new Guid("0147cdb1-e997-41a7-87c3-5cff34d7a8b9"));

            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "Id",
                keyValue: new Guid("92b5dce4-c941-4c21-86b1-4435142bc480"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("4c988889-813a-4723-8407-45717fb8c82d"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("8ef9d581-4bf2-4c9d-ae1f-b592db155a50"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("6c85514a-7ab5-4a99-ac20-4776d0668aed"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("91da9b52-cb82-49d2-8373-027e813f5740"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("8a6c19d3-59b7-4e1f-bb08-3942db2226d7"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("909f3db4-f61e-4fed-a1cd-b93d05cf1341"));
        }
    }
}
