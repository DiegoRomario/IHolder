using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioConcept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByAsset_Asset_AssetId",
                table: "AllocationByAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByAsset_User_UserId",
                table: "AllocationByAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByCategory_User_UserId",
                table: "AllocationByCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationsByProduct_User_UserId",
                table: "AllocationsByProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetInPortfolio_User_UserId",
                table: "AssetInPortfolio");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "StateSetAt",
                table: "Asset");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AssetInPortfolio",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetInPortfolio_UserId",
                table: "AssetInPortfolio",
                newName: "IX_AssetInPortfolio_PortfolioId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AllocationsByProduct",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationsByProduct_UserId",
                table: "AllocationsByProduct",
                newName: "IX_AllocationsByProduct_PortfolioId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AllocationByCategory",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByCategory_UserId",
                table: "AllocationByCategory",
                newName: "IX_AllocationByCategory_PortfolioId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AllocationByAsset",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByAsset_UserId",
                table: "AllocationByAsset",
                newName: "IX_AllocationByAsset_PortfolioId");

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "AssetInPortfolio",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StateSetAt",
                table: "AssetInPortfolio",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByAsset_AssetInPortfolio_AssetId",
                table: "AllocationByAsset",
                column: "AssetId",
                principalTable: "AssetInPortfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByAsset_Portfolio_PortfolioId",
                table: "AllocationByAsset",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByCategory_Portfolio_PortfolioId",
                table: "AllocationByCategory",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationsByProduct_Portfolio_PortfolioId",
                table: "AllocationsByProduct",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetInPortfolio_Portfolio_PortfolioId",
                table: "AssetInPortfolio",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByAsset_AssetInPortfolio_AssetId",
                table: "AllocationByAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByAsset_Portfolio_PortfolioId",
                table: "AllocationByAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByCategory_Portfolio_PortfolioId",
                table: "AllocationByCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationsByProduct_Portfolio_PortfolioId",
                table: "AllocationsByProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetInPortfolio_Portfolio_PortfolioId",
                table: "AssetInPortfolio");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AssetInPortfolio");

            migrationBuilder.DropColumn(
                name: "StateSetAt",
                table: "AssetInPortfolio");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "AssetInPortfolio",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetInPortfolio_PortfolioId",
                table: "AssetInPortfolio",
                newName: "IX_AssetInPortfolio_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "AllocationsByProduct",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationsByProduct_PortfolioId",
                table: "AllocationsByProduct",
                newName: "IX_AllocationsByProduct_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "AllocationByCategory",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByCategory_PortfolioId",
                table: "AllocationByCategory",
                newName: "IX_AllocationByCategory_UserId");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "AllocationByAsset",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByAsset_PortfolioId",
                table: "AllocationByAsset",
                newName: "IX_AllocationByAsset_UserId");

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Asset",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StateSetAt",
                table: "Asset",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByAsset_Asset_AssetId",
                table: "AllocationByAsset",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByAsset_User_UserId",
                table: "AllocationByAsset",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByCategory_User_UserId",
                table: "AllocationByCategory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationsByProduct_User_UserId",
                table: "AllocationsByProduct",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetInPortfolio_User_UserId",
                table: "AssetInPortfolio",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
