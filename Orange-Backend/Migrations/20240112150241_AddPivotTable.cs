using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orange_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPivotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Brands_BrandId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BrandId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "BrandCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrandCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandCategory_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrandCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandCategoryId",
                table: "Products",
                column: "BrandCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandCategory_BrandId",
                table: "BrandCategory",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandCategory_CategoryId",
                table: "BrandCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BrandCategory_BrandCategoryId",
                table: "Products",
                column: "BrandCategoryId",
                principalTable: "BrandCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BrandCategory_BrandCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "BrandCategory");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BrandId",
                table: "Categories",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Brands_BrandId",
                table: "Categories",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }
    }
}
