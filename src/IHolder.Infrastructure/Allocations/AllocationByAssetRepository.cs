using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationByAssetRepository(IHolderDbContext _dbContext) : AllocationRepository(_dbContext), IAllocationByAssetRepository
{
    public async Task<AllocationByAsset?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                     .Include(a => a.AssetInPortfolio)
                                                     .ThenInclude(a => a.Asset)
                                                     .FirstOrDefaultAsync(a => a.Id == id, ct);
    }
}
