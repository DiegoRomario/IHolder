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
