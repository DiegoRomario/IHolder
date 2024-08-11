﻿using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Allocations;
public class AllocationByCategoryConfigurations : EntityConfiguration<AllocationByCategory>
{
    public override void Configure(EntityTypeBuilder<AllocationByCategory> builder)
    {
        base.Configure(builder);

        builder.ToTable("AllocationByCategory");

        builder.Property(a => a.PortfolioId).IsRequired()
                                            .HasColumnOrder(2);

        builder.Property(a => a.CategoryId).IsRequired()
                                           .HasColumnOrder(3);

        builder.ComplexProperty(a => a.AllocationValues, i =>
        {
            i.Property(a => a.CurrentAmount).HasColumnName("CurrentAmount")
                                            .HasColumnType("DECIMAL(18,4)")
                                            .IsRequired()
                                            .HasColumnOrder(4);

            i.Property(a => a.TargetPercentage).HasColumnName("TargetPercentage")
                                               .HasColumnType("DECIMAL(18,4)")
                                               .IsRequired()
                                               .HasColumnOrder(5);

            i.Property(a => a.CurrentPercentage).HasColumnName("CurrentPercentage")
                                                .HasColumnType("DECIMAL(18,4)")
                                                .IsRequired()
                                                .HasColumnOrder(6);

            i.Property(a => a.PercentageDifference).HasColumnName("PercentageDifference")
                                                   .HasColumnType("DECIMAL(18,4)")
                                                   .IsRequired()
                                                   .HasColumnOrder(7);

            i.Property(a => a.AmountDifference).HasColumnName("AmountDifference")
                                               .HasColumnType("DECIMAL(18,4)")
                                               .IsRequired()
                                               .HasColumnOrder(8);
        });


        builder.Property(a => a.Recommendation).IsRequired()
                                               .HasColumnType("TINYINT")
                                               .HasColumnOrder(9);

        builder.HasOne(a => a.Category)
               .WithMany()
               .HasForeignKey(a => a.CategoryId);

        builder.HasOne(a => a.Portfolio)
                .WithMany(a => a.AllocationsByCategory)
                .HasForeignKey(a => a.PortfolioId);
    }

}

