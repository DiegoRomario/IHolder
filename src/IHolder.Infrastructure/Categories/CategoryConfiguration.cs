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
        builder.Property(a => a.Description).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Details).HasColumnType("VARCHAR(600)").IsRequired();
        builder.ToTable("Category");
    }
}
