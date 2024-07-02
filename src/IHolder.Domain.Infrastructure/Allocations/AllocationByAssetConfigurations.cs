using IHolder.Domain.Allocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Allocations;
public class AllocationByAssetConfigurations : IEntityTypeConfiguration<AllocationByAsset>
{
    public void Configure(EntityTypeBuilder<AllocationByAsset> builder)
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
        builder.Property(p => p.Asset.Id).IsRequired();
        builder.Property(p => p.Recommendation).IsRequired().HasColumnType("BIT");
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.ToTable("AllocationByAsset");
    }

}

