using IHolder.Domain.Categories;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Categories;

public class CategoryConfiguration : EntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Category");

        builder.Property(a => a.Name).HasColumnType("VARCHAR(80)")
                                     .IsRequired()
                                     .HasColumnOrder(2);

        builder.Property(a => a.Description).HasColumnType("VARCHAR(600)")
                                            .IsRequired()
                                            .HasColumnOrder(3);
    }
}
