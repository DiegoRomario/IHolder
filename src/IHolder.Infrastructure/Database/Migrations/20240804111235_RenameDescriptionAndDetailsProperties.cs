using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameDescriptionAndDetailsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename the columns
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Product",
                newName: "TempDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Product",
                newName: "TempDescription");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Category",
                newName: "TempDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Category",
                newName: "TempDescription");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Asset",
                newName: "TempDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Asset",
                newName: "TempDescription");

            // Add the new columns with correct types
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Product",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Category",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Asset",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            // Copy data to the new columns
            migrationBuilder.Sql(@"UPDATE Product SET Name = TempDescription, Description = TempDetails");
            migrationBuilder.Sql(@"UPDATE Category SET Name = TempDescription, Description = TempDetails");
            migrationBuilder.Sql(@"UPDATE Asset SET Name = TempDescription, Description = TempDetails");

            // Drop the temporary columns
            migrationBuilder.DropColumn(name: "TempDetails", table: "Product");
            migrationBuilder.DropColumn(name: "TempDescription", table: "Product");
            migrationBuilder.DropColumn(name: "TempDetails", table: "Category");
            migrationBuilder.DropColumn(name: "TempDescription", table: "Category");
            migrationBuilder.DropColumn(name: "TempDetails", table: "Asset");
            migrationBuilder.DropColumn(name: "TempDescription", table: "Asset");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add temporary columns to reverse data migration
            migrationBuilder.AddColumn<string>(
                name: "TempDescription",
                table: "Product",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempDetails",
                table: "Product",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempDescription",
                table: "Category",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempDetails",
                table: "Category",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempDescription",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempDetails",
                table: "Asset",
                type: "VARCHAR(600)",
                nullable: false,
                defaultValue: "");

            // Copy data back to temporary columns
            migrationBuilder.Sql(@"UPDATE Product SET TempDescription = Name, TempDetails = Description");
            migrationBuilder.Sql(@"UPDATE Category SET TempDescription = Name, TempDetails = Description");
            migrationBuilder.Sql(@"UPDATE Asset SET TempDescription = Name, TempDetails = Description");

            // Drop the new columns
            migrationBuilder.DropColumn(name: "Name", table: "Product");
            migrationBuilder.DropColumn(name: "Description", table: "Product");
            migrationBuilder.DropColumn(name: "Name", table: "Category");
            migrationBuilder.DropColumn(name: "Description", table: "Category");
            migrationBuilder.DropColumn(name: "Name", table: "Asset");
            migrationBuilder.DropColumn(name: "Description", table: "Asset");

            // Rename temporary columns back to original names
            migrationBuilder.RenameColumn(
                name: "TempDetails",
                table: "Product",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "TempDescription",
                table: "Product",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "TempDetails",
                table: "Category",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "TempDescription",
                table: "Category",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "TempDetails",
                table: "Asset",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "TempDescription",
                table: "Asset",
                newName: "Description");

            // Revert column type changes
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Product",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Category",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Asset",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(600)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Asset",
                type: "VARCHAR(600)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");
        }
    }
}
