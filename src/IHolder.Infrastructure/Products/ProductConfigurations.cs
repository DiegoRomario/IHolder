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

        builder.ToTable("Product");

        builder.Property(d => d.CategoryId).IsRequired()
                                           .HasColumnOrder(2);

        builder.Property(a => a.Name).HasColumnType("VARCHAR(80)")
                                     .IsRequired()
                                     .HasColumnOrder(3);

        builder.Property(a => a.Description).HasColumnType("VARCHAR(600)")
                                            .IsRequired()
                                            .HasColumnOrder(4);

        builder.Property(a => a.Risk).HasColumnType("TINYINT")
                                     .IsRequired()
                                     .HasColumnOrder(5);

    }
}
