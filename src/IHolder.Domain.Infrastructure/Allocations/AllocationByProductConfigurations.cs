using IHolder.Domain.Allocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Allocations;
public class AllocationByProductConfigurations : IEntityTypeConfiguration<AllocationByProduct>
{
    public void Configure(EntityTypeBuilder<AllocationByProduct> builder)
    {
        builder.HasKey(d => d.Id);
        builder.OwnsOne(a => a.AllocationValues, i =>
        {
            i.Property(a => a.TargetPercentage).IsRequired();
            i.Property(a => a.CurrentPercentage).IsRequired();
            i.Property(a => a.PercentageDifference).IsRequired();
            i.Property(a => a.CurrentAmount).IsRequired();
            i.Property(a => a.AmountDifference).IsRequired();
        });
        builder.Property(p => p.Product.Id).IsRequired();
        builder.Property(p => p.Recommendation).IsRequired().HasColumnType("BIT");
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.ToTable("AllocationByProduct");
    }

}

