using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Portfolios;

internal class PortfolioRepository(IHolderDbContext _dbContext) : IPortfolioRepository
{
    public async Task<Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Portfolios.AsNoTracking()
                                          .Include(p => p.User)
                                          .Include(p => p.AssetsInPortfolio)
                                              .ThenInclude(a => a.Asset)
                                          .Include(p => p.AllocationsByCategory)
                                              .ThenInclude(ac => ac.Category)
                                          .Include(p => p.AllocationsByProduct)
                                              .ThenInclude(ap => ap.Product)
                                          .Include(p => p.AllocationsByAsset)
                                              .ThenInclude(aa => aa.AssetInPortfolio)
                                              .ThenInclude(a => a.Asset)
                                          .FirstOrDefaultAsync(p => p.UserId == userId, ct);
    }

    public async Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.Portfolios.AsNoTracking()
                                    .AnyAsync(predicate, ct);
    }

    public async Task AddAsync(Portfolio portfolio, CancellationToken ct)
    {
        await _dbContext.AddAsync(portfolio, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Portfolio portfolio, CancellationToken ct)
    {
        _dbContext.Update(portfolio);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task AddAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct)
    {
        await _dbContext.AddAsync(assetInPortfolio, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct)
    {
        _dbContext.Update(assetInPortfolio);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task RemoveAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct)
    {
        _dbContext.Remove(assetInPortfolio);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<AssetInPortfolio?> GetAssetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AssetsInPortfolio.AsNoTracking()
                                                 .Include(a => a.Asset)
                                                 .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<bool> ExistsAssetByPredicateAsync(Expression<Func<AssetInPortfolio, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AssetsInPortfolio.AsNoTracking()
                                                 .AnyAsync(predicate, ct);
    }
}
