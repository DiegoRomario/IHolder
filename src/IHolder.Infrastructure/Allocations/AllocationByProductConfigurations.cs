using IHolder.Domain.Allocations;
using IHolder.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Allocations;
public class AllocationByProductConfigurations : IEntityTypeConfiguration<AllocationByProduct>
{
    public void Configure(EntityTypeBuilder<AllocationByProduct> builder)
    {
        builder.HasKey(d => d.Id);
        builder.OwnsOne(a => a.AllocationValues, i =>
        {
            i.Property(a => a.TargetPercentage).HasColumnName("TargetPercentage").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.CurrentPercentage).HasColumnName("CurrentPercentage").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.PercentageDifference).HasColumnName("PercentageDifference").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.CurrentAmount).HasColumnName("CurrentAmount").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.AmountDifference).HasColumnName("AmountDifference").HasColumnType("DECIMAL(18,4)").IsRequired();
        });
        builder.Property(p => p.ProductId).IsRequired();
        builder.HasOne<Product>().WithMany().HasForeignKey(p => p.ProductId);

        builder.Property(p => p.Recommendation).IsRequired().HasColumnType("TINYINT");
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.ToTable("AllocationByProduct");
    }

}

