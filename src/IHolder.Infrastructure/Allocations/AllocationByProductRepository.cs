using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationByProductRepository(IHolderDbContext _dbContext) : AllocationRepository(_dbContext), IAllocationByProductRepository
{
    public async Task<AllocationByProduct?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByProduct.AsNoTracking()
                                                    .Include(a => a.Product)
                                                    .FirstOrDefaultAsync(a => a.Id == id, ct);
    }
}
