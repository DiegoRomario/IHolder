using IHolder.Domain.Allocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Allocations;
public class AllocationByAssetConfigurations : IEntityTypeConfiguration<AllocationByAsset>
{
    public void Configure(EntityTypeBuilder<AllocationByAsset> builder)
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
        builder.Property(p => p.AssetId).IsRequired();
        /* Todo: this way of mapping is slightly different from AllocationByProduct and Category
                 given that AllocationByAsset has the Asset property (and a todo to review it later) */
        builder.HasOne(a => a.Asset).WithMany().HasForeignKey(a => a.AssetId);

        builder.Property(p => p.Recommendation).IsRequired().HasColumnType("TINYINT");
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.ToTable("AllocationByAsset");
    }

}

