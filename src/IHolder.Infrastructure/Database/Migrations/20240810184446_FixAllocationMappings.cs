using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixAllocationMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocationsByProduct_Portfolio_PortfolioId",
                table: "AllocationsByProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationsByProduct_Product_ProductId",
                table: "AllocationsByProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllocationsByProduct",
                table: "AllocationsByProduct");

            migrationBuilder.RenameTable(
                name: "AllocationsByProduct",
                newName: "AllocationByProduct");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationsByProduct_ProductId",
                table: "AllocationByProduct",
                newName: "IX_AllocationByProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationsByProduct_PortfolioId",
                table: "AllocationByProduct",
                newName: "IX_AllocationByProduct_PortfolioId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "VARCHAR(1200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1200)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<byte>(
                name: "Risk",
                table: "Product",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Asset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Asset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Asset",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Asset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationByCategory",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationByAsset",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetId",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationByProduct",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "AllocationByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllocationByProduct",
                table: "AllocationByProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByProduct_Portfolio_PortfolioId",
                table: "AllocationByProduct",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationByProduct_Product_ProductId",
                table: "AllocationByProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByProduct_Portfolio_PortfolioId",
                table: "AllocationByProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AllocationByProduct_Product_ProductId",
                table: "AllocationByProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllocationByProduct",
                table: "AllocationByProduct");

            migrationBuilder.RenameTable(
                name: "AllocationByProduct",
                newName: "AllocationsByProduct");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByProduct_ProductId",
                table: "AllocationsByProduct",
                newName: "IX_AllocationsByProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AllocationByProduct_PortfolioId",
                table: "AllocationsByProduct",
                newName: "IX_AllocationsByProduct_PortfolioId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "VARCHAR(1200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1200)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<byte>(
                name: "Risk",
                table: "Product",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Asset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Asset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Asset",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Asset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationByCategory",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationByCategory",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationByCategory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationByAsset",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetId",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationByAsset",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationByAsset",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentage",
                table: "AllocationsByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<byte>(
                name: "Recommendation",
                table: "AllocationsByProduct",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "AllocationsByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "AllocationsByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDifference",
                table: "AllocationsByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPercentage",
                table: "AllocationsByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "AllocationsByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDifference",
                table: "AllocationsByProduct",
                type: "DECIMAL(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)")
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AllocationsByProduct",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllocationsByProduct",
                table: "AllocationsByProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationsByProduct_Portfolio_PortfolioId",
                table: "AllocationsByProduct",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllocationsByProduct_Product_ProductId",
                table: "AllocationsByProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
