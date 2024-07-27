using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Assets;

internal class AssetRepository(IHolderDbContext _dbContext) : IAssetRepository
{
    public Task<Asset?> GetByIdAsync(Guid id)
    {
        return _dbContext.Assets.AsNoTracking()
                         .Include(a => a.Product)
                         .ThenInclude(p => p.Category)
                         .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate)
    {
        return _dbContext.Assets.AsNoTracking()
                                .AnyAsync(predicate);
    }

    public async Task AddAsync(Asset asset)
    {
        await _dbContext.AddAsync(asset);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Asset asset)
    {
        _dbContext.Update(asset);
        await _dbContext.SaveChangesAsync();
    }
}
