using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationRepository(IHolderDbContext dbContext) : IAllocationRepository
{
    protected readonly IHolderDbContext _dbContext = dbContext;

    public async Task<bool> ExistsByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken ct) where T : Allocation
    {
        return await _dbContext.Set<T>().AsNoTracking()
                               .AnyAsync(predicate, ct);
    }

    public async Task<T?> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken ct) where T : Allocation
    {
        var query = _dbContext.Set<T>().AsNoTracking();
        return await query.FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<T?> GetByIdAsync<T>(Guid id, CancellationToken ct) where T : Allocation
    {
        IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

        query = typeof(T) switch
        {
            Type t when t == typeof(AllocationByCategory) => query.Include(a => ((AllocationByCategory)(object)a).Category),
            Type t when t == typeof(AllocationByProduct) => query.Include(a => ((AllocationByProduct)(object)a).Product),
            Type t when t == typeof(AllocationByAsset) => query.Include(a => ((AllocationByAsset)(object)a).AssetInPortfolio)
                                                               .ThenInclude(assetInPortfolio => assetInPortfolio.Asset),
            _ => query
        };

        return await query.FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task AddAsync<T>(T allocation, CancellationToken ct) where T : Allocation
    {
        await _dbContext.Set<T>().AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync<T>(T allocation, CancellationToken ct) where T : Allocation
    {
        _dbContext.Set<T>().Update(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }
}
