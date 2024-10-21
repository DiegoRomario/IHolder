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
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Risk = table.Column<byte>(type: "TINYINT", nullable: false),
                    ExchangeId = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolio_User_UserId",
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
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Ticker = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                name: "AllocationByCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                        name: "FK_AllocationByCategory_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllocationByProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationByProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationByProduct_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllocationByProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetInPortfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    InvestedAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    FirstInvestmentDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    State = table.Column<byte>(type: "TINYINT", nullable: false),
                    StateSetAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
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
                        name: "FK_AssetInPortfolio_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllocationByAsset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetInPortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TargetPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    PercentageDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    AmountDifference = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    Recommendation = table.Column<byte>(type: "TINYINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationByAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationByAsset_AssetInPortfolio_AssetInPortfolioId",
                        column: x => x.AssetInPortfolioId,
                        principalTable: "AssetInPortfolio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllocationByAsset_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByAsset_AssetInPortfolioId",
                table: "AllocationByAsset",
                column: "AssetInPortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByAsset_PortfolioId_AssetInPortfolioId",
                table: "AllocationByAsset",
                columns: new[] { "PortfolioId", "AssetInPortfolioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByCategory_CategoryId",
                table: "AllocationByCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByCategory_PortfolioId_CategoryId",
                table: "AllocationByCategory",
                columns: new[] { "PortfolioId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByProduct_PortfolioId_ProductId",
                table: "AllocationByProduct",
                columns: new[] { "PortfolioId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllocationByProduct_ProductId",
                table: "AllocationByProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ProductId",
                table: "Asset",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_AssetId_PortfolioId",
                table: "AssetInPortfolio",
                columns: new[] { "AssetId", "PortfolioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_PortfolioId",
                table: "AssetInPortfolio",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio",
                column: "UserId",
                unique: true);

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
                name: "AllocationByProduct");

            migrationBuilder.DropTable(
                name: "AssetInPortfolio");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
