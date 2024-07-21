using IHolder.Application.Common.Interfaces;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Assets;

internal class AssetRepository(IHolderDbContext _dbContext) : IAssetRepository
{
    public async Task<bool> ExistsByProductIdAsync(Guid productId)
    {
        return await _dbContext.Assets.AsNoTracking()
                                      .AnyAsync(asset => asset.ProductId == productId);
    }
}
