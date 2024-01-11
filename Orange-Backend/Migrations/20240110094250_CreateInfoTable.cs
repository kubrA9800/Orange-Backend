using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orange_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Setting");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Sliders",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Setting",
                newName: "CreatedDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setting",
                table: "Setting",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Setting",
                table: "Setting");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Settings");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Sliders",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Settings",
                newName: "CreateDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");
        }
    }
}
