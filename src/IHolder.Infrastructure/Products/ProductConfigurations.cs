using IHolder.Domain.Products;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Products;
public class ProductConfigurations : EntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.Property(a => a.Description).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Details).HasColumnType("VARCHAR(600)").IsRequired();
        builder.Property(d => d.CategoryId).IsRequired();
        builder.Property(a => a.Risk).HasColumnType("TINYINT").IsRequired();
        builder.ToTable("Product");
    }
}
