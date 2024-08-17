using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AssetInPortfolioUQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssetInPortfolio_AssetId",
                table: "AssetInPortfolio");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_AssetId_PortfolioId",
                table: "AssetInPortfolio",
                columns: new[] { "AssetId", "PortfolioId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssetInPortfolio_AssetId_PortfolioId",
                table: "AssetInPortfolio");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInPortfolio_AssetId",
                table: "AssetInPortfolio",
                column: "AssetId");
        }
    }
}
