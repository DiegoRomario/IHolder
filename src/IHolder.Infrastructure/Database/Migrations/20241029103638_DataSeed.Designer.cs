﻿// <auto-generated />
using System;
using System.Collections.Generic;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IHolder.Infrastructure.Database.Migrations
{
    [DbContext(typeof(IHolderDbContext))]
    [Migration("20241029103638_DataSeed")]
    partial class DataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByAsset", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("AssetInPortfolioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<byte>("Recommendation")
                        .HasColumnType("TINYINT")
                        .HasColumnOrder(9);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.ComplexProperty<Dictionary<string, object>>("AllocationValues", "IHolder.Domain.Allocations.AllocationByAsset.AllocationValues#AllocationValues", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("AmountDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("AmountDifference")
                                .HasColumnOrder(8);

                            b1.Property<decimal>("CurrentAmount")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentAmount")
                                .HasColumnOrder(4);

                            b1.Property<decimal>("CurrentPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentPercentage")
                                .HasColumnOrder(6);

                            b1.Property<decimal>("PercentageDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("PercentageDifference")
                                .HasColumnOrder(7);

                            b1.Property<decimal>("TargetPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("TargetPercentage")
                                .HasColumnOrder(5);
                        });

                    b.HasKey("Id");

                    b.HasIndex("AssetInPortfolioId");

                    b.HasIndex("PortfolioId", "AssetInPortfolioId")
                        .IsUnique();

                    b.ToTable("AllocationByAsset", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<byte>("Recommendation")
                        .HasColumnType("TINYINT")
                        .HasColumnOrder(9);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.ComplexProperty<Dictionary<string, object>>("AllocationValues", "IHolder.Domain.Allocations.AllocationByCategory.AllocationValues#AllocationValues", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("AmountDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("AmountDifference")
                                .HasColumnOrder(8);

                            b1.Property<decimal>("CurrentAmount")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentAmount")
                                .HasColumnOrder(4);

                            b1.Property<decimal>("CurrentPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentPercentage")
                                .HasColumnOrder(6);

                            b1.Property<decimal>("PercentageDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("PercentageDifference")
                                .HasColumnOrder(7);

                            b1.Property<decimal>("TargetPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("TargetPercentage")
                                .HasColumnOrder(5);
                        });

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PortfolioId", "CategoryId")
                        .IsUnique();

                    b.ToTable("AllocationByCategory", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(3);

                    b.Property<byte>("Recommendation")
                        .HasColumnType("TINYINT")
                        .HasColumnOrder(9);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.ComplexProperty<Dictionary<string, object>>("AllocationValues", "IHolder.Domain.Allocations.AllocationByProduct.AllocationValues#AllocationValues", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("AmountDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("AmountDifference")
                                .HasColumnOrder(8);

                            b1.Property<decimal>("CurrentAmount")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentAmount")
                                .HasColumnOrder(4);

                            b1.Property<decimal>("CurrentPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("CurrentPercentage")
                                .HasColumnOrder(6);

                            b1.Property<decimal>("PercentageDifference")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("PercentageDifference")
                                .HasColumnOrder(7);

                            b1.Property<decimal>("TargetPercentage")
                                .HasColumnType("DECIMAL(18,4)")
                                .HasColumnName("TargetPercentage")
                                .HasColumnOrder(5);
                        });

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PortfolioId", "ProductId")
                        .IsUnique();

                    b.ToTable("AllocationByProduct", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Assets.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnOrder(4);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(3);

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(18,4)")
                        .HasColumnOrder(6);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Asset", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnOrder(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Portfolios.AssetInPortfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AveragePrice")
                        .HasColumnType("DECIMAL(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("FirstInvestmentDate")
                        .HasColumnType("DATETIME2");

                    b.Property<decimal>("InvestedAmount")
                        .HasColumnType("DECIMAL(18,4)");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("DECIMAL(18,4)");

                    b.Property<byte>("State")
                        .HasColumnType("TINYINT");

                    b.Property<DateTime>("StateSetAt")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("AssetId", "PortfolioId")
                        .IsUnique();

                    b.ToTable("AssetInPortfolio", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Portfolios.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Portfolio", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnOrder(4);

                    b.Property<string>("ExchangeId")
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnOrder(6);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(3);

                    b.Property<byte>("Risk")
                        .HasColumnType("TINYINT")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(4);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(2);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1200)")
                        .HasColumnName("PasswordHash")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByAsset", b =>
                {
                    b.HasOne("IHolder.Domain.Portfolios.AssetInPortfolio", "AssetInPortfolio")
                        .WithMany()
                        .HasForeignKey("AssetInPortfolioId")
                        .IsRequired();

                    b.HasOne("IHolder.Domain.Portfolios.Portfolio", "Portfolio")
                        .WithMany("AllocationsByAsset")
                        .HasForeignKey("PortfolioId")
                        .IsRequired();

                    b.Navigation("AssetInPortfolio");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByCategory", b =>
                {
                    b.HasOne("IHolder.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.HasOne("IHolder.Domain.Portfolios.Portfolio", "Portfolio")
                        .WithMany("AllocationsByCategory")
                        .HasForeignKey("PortfolioId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("IHolder.Domain.Allocations.AllocationByProduct", b =>
                {
                    b.HasOne("IHolder.Domain.Portfolios.Portfolio", "Portfolio")
                        .WithMany("AllocationsByProduct")
                        .HasForeignKey("PortfolioId")
                        .IsRequired();

                    b.HasOne("IHolder.Domain.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Portfolio");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IHolder.Domain.Assets.Asset", b =>
                {
                    b.HasOne("IHolder.Domain.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IHolder.Domain.Portfolios.AssetInPortfolio", b =>
                {
                    b.HasOne("IHolder.Domain.Assets.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId")
                        .IsRequired();

                    b.HasOne("IHolder.Domain.Portfolios.Portfolio", "Portfolio")
                        .WithMany("AssetsInPortfolio")
                        .HasForeignKey("PortfolioId")
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("IHolder.Domain.Portfolios.Portfolio", b =>
                {
                    b.HasOne("IHolder.Domain.Users.User", "User")
                        .WithOne("Portfolio")
                        .HasForeignKey("IHolder.Domain.Portfolios.Portfolio", "UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IHolder.Domain.Products.Product", b =>
                {
                    b.HasOne("IHolder.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("IHolder.Domain.Portfolios.Portfolio", b =>
                {
                    b.Navigation("AllocationsByAsset");

                    b.Navigation("AllocationsByCategory");

                    b.Navigation("AllocationsByProduct");

                    b.Navigation("AssetsInPortfolio");
                });

            modelBuilder.Entity("IHolder.Domain.Users.User", b =>
                {
                    b.Navigation("Portfolio")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
