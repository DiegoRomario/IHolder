using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationRepository(IHolderDbContext _dbContext) : IAllocationRepository
{
    public async Task AddAsync(AllocationByCategory allocation, CancellationToken ct)
    {
        await _dbContext.AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}
