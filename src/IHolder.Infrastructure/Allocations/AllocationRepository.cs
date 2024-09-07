using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationRepository(IHolderDbContext _dbContext) : IAllocationRepository
{
    public async Task AddAsync<T>(T allocation, CancellationToken ct) where T : Allocation
    {
        await _dbContext.Set<T>().AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}
