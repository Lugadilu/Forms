using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formfields_forms_FormId",
                table: "formfields");

            migrationBuilder.DropForeignKey(
                name: "FK_formrecords_forms_FormId",
                table: "formrecords");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "formrecords",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "formfields",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_formfields_forms_FormId",
                table: "formfields",
                column: "FormId",
                principalTable: "forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_formrecords_forms_FormId",
                table: "formrecords",
                column: "FormId",
                principalTable: "forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "formrecords",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "formfields",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
    }
}
