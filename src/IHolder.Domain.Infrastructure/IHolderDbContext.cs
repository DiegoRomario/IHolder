﻿using IHolder.Domain.Allocations;
using IHolder.Domain.Assets;
using IHolder.Domain.Categories;
using IHolder.Domain.Common;
using IHolder.Domain.Products;
using IHolder.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IHolder.Domain.Infrastructure;
public class IHolderDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IPublisher _publisher) : DbContext(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DbSet<AssetInPortfolio> AssetsInPortfolio { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AllocationByAsset> AllocationsByAsset { get; set; }
    public DbSet<AllocationByProduct> AllocationsByProduct { get; set; }
    public DbSet<AllocationByCategory> AllocationsByCategory { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetDefaultDBType(modelBuilder, typeof(string), "VARCHAR(100)");
        SetDefaultDBType(modelBuilder, typeof(DateTime), "DATETIME");
        SetDefaultDBType(modelBuilder, typeof(DateTime?), "DATETIME");
        SetDefaultDBType(modelBuilder, typeof(decimal), "DECIMAL(12,2)");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }

    private static void SetDefaultDBType(ModelBuilder modelBuilder, Type type, string dbFieldType)
    {
        foreach (var property in modelBuilder
              .Model
              .GetEntityTypes()
              .SelectMany(e => e.GetProperties().Where(p => p.ClrType == type)))
        {
            property.SetColumnType(dbFieldType);
        }
    }


    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();

        if (_httpContextAccessor.HttpContext is not null)
        {

            var domainEvents = ChangeTracker.Entries<Entity>()
                                            .Select(entry => entry.Entity.PopDomainEvents())
                                            .SelectMany(x => x)
                                            .ToList();

            if (IsUserWaitingOnline())
            {
                AddDomainEventsToOfflineProcessingQueue(domainEvents);
            }
            else
            {
                await PublishDomainEvents(_publisher, domainEvents);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private static async Task PublishDomainEvents(IPublisher _publisher, List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // FETCH QUEUE FROM HTTP CONTEXT OR CREATE A NEW QUEUE IF IT DOESN'T EXIST
        var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
                                .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                                ? existingDomainEvents
                                : new Queue<IDomainEvent>();

        // ADD THE DOMAIN EVENTS TO THE END OF THE QUEUE
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // STORE THE QUEUE IN THE HTTP CONTEXT
        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }

    private void SetTimestamps()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null && entry.Entity.GetType().GetProperty("DataAlteracao") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                entry.Property("UpdatedAt").IsModified = false;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
        }
    }
}
