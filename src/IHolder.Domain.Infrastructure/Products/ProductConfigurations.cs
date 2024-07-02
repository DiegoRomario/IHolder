using IHolder.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Products;
public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Description).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Details).HasColumnType("VARCHAR(600)").IsRequired();
        builder.Property(d => d.CategoryId).IsRequired();
        builder.Property(a => a.Risk).HasColumnType("TINYINT").IsRequired();
        builder.ToTable("Product");
    }
}
