using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/UsersSeed.sql");
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/PortfoliosSeed.sql");
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/CategoriesSeed.sql");
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/ProductsSeed.sql");
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/AllocationsByCategorySeed.sql");
            ExecuteSqlFile(migrationBuilder, "Database/Seeds/AllocationsByProductSeed.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AllocationByProduct;");
            migrationBuilder.Sql("DELETE FROM AllocationByCategory;");
            migrationBuilder.Sql("DELETE FROM Product;");
            migrationBuilder.Sql("DELETE FROM Category;");
            migrationBuilder.Sql("DELETE FROM Portfolio;");
            migrationBuilder.Sql("DELETE FROM [User];");
        }

        private static void ExecuteSqlFile(MigrationBuilder migrationBuilder, string relativePath)
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fullPath = Path.Combine(basePath, relativePath);

                if (!File.Exists(fullPath)) throw new FileNotFoundException($"SQL file not found: {fullPath}");

                var sql = File.ReadAllText(fullPath);
                migrationBuilder.Sql(sql);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing SQL file '{relativePath}': {ex.Message}", ex);
            }
        }
    }
}
