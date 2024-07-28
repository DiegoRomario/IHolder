using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Details = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR(1200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Details = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Risk = table.Column<byte>(type: "TINYINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllocationByCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationByCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationByCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllocationByCategory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllocationsByProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationsByProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationsByProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllocationsByProduct_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Details = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Ticker = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    State = table.Column<byte>(type: "TINYINT", nullable: false),
                    StateSetAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllocationByAsset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationByAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationByAsset_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllocationByAsset_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetInPortfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    InvestedAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstInvestmentDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetInPortfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetInPortfolio_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetInPortfolio_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByAsset_AssetId",
                table: "AllocationByAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByAsset_UserId",
                table: "AllocationByAsset",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByCategory_CategoryId",
                table: "AllocationByCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByCategory_UserId",
                table: "AllocationByCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationsByProduct_ProductId",
                table: "AllocationsByProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationsByProduct_UserId",
                table: "AllocationsByProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ProductId",
                table: "Asset",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_AssetId",
                table: "AssetInPortfolio",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_UserId",
                table: "AssetInPortfolio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocationByAsset");

            migrationBuilder.DropTable(
                name: "AllocationByCategory");

            migrationBuilder.DropTable(
                name: "AllocationsByProduct");

            migrationBuilder.DropTable(
                name: "AssetInPortfolio");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
