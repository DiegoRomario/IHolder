using IHolder.Application.Allocations.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Portfolios;

internal class PortfolioRepository(IHolderDbContext _dbContext) : IPortfolioRepository
{
    public async Task<Portfolio?> GetByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct, bool includes = false)
    {
        var query = _dbContext.Portfolios.AsNoTracking();

        if (includes)
        {
            query = query.Include(p => p.User)
                         .Include(p => p.AssetsInPortfolio)
                            .ThenInclude(a => a.Asset)
                         .Include(p => p.AllocationsByCategory)
                            .ThenInclude(ac => ac.Category)
                         .Include(p => p.AllocationsByProduct)
                            .ThenInclude(ap => ap.Product)
                         .Include(p => p.AllocationsByAsset)
                            .ThenInclude(aa => aa.AssetInPortfolio)
                            .ThenInclude(a => a.Asset);
        }

        return await query.FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Portfolios.Include(p => p.AssetsInPortfolio)
                                          .ThenInclude(a => a.Asset)
                                          .FirstOrDefaultAsync(p => p.Id == id, ct);
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

    public async Task RemoveAsset(AssetInPortfolio assetInPortfolio, CancellationToken ct)
    {
        _dbContext.Remove(assetInPortfolio);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<AssetInPortfolio?> GetAssetInPortfolioByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AssetsInPortfolio.AsNoTracking()
                                                 .Include(a => a.Asset)
                                                 .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<bool> ExistsAssetInPortfolioByPredicateAsync(Expression<Func<AssetInPortfolio, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AssetsInPortfolio.AsNoTracking()
                                                 .AnyAsync(predicate, ct);
    }

    public async Task<List<Guid>> GetAllCategoryIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct)
    {
        var categoryIds = await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                                     .SelectMany(p => p.AssetsInPortfolio)
                                                     .Select(a => a.Asset.Product.CategoryId)
                                                     .Distinct()
                                                     .ToListAsync(ct);

        return categoryIds;
    }

    public async Task<List<Guid>> GetAllProductIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct)
    {
        var productIds = await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                                     .SelectMany(p => p.AssetsInPortfolio)
                                                     .Select(a => a.Asset.Product.Id)
                                                     .Distinct()
                                                     .ToListAsync(ct);

        return productIds;
    }

    public async Task<List<Guid>> GetAllAssetIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct)
    {
        var assetIds = await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                                  .SelectMany(p => p.AssetsInPortfolio)
                                                  .Select(a => a.Asset.Id)
                                                  .Distinct()
                                                  .ToListAsync(ct);

        return assetIds;
    }

    public async Task<decimal> GetInvestedAmountoByCategory(Guid userId, Guid categoryId, CancellationToken ct)
    {
        return await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                          .SelectMany(p => p.AssetsInPortfolio)
                                          .Where(a => a.Asset.Product.CategoryId == categoryId)
                                          .SumAsync(a => a.InvestedAmount, ct);
    }

    public async Task<decimal> GetInvestedAmountoByProduct(Guid userId, Guid productId, CancellationToken ct)
    {
        return await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                          .SelectMany(p => p.AssetsInPortfolio)
                                          .Where(a => a.Asset.Product.Id == productId)
                                          .SumAsync(a => a.InvestedAmount, ct);
    }

    public async Task<decimal> GetInvestedAmountoByAsset(Guid userId, Guid assetId, CancellationToken ct)
    {
        return await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                          .SelectMany(p => p.AssetsInPortfolio)
                                          .Where(a => a.AssetId == assetId)
                                          .SumAsync(a => a.InvestedAmount, ct);
    }

    public async Task<decimal> GetInvestedAmount(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Portfolios.Where(p => p.UserId == userId)
                                          .SelectMany(p => p.AssetsInPortfolio)
                                          .SumAsync(a => a.InvestedAmount, ct);
    }


    // Allocation
    public async Task<bool> ExistsAllocationByPredicateAsync(Expression<Func<AllocationByAsset, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                   .AnyAsync(predicate, ct);
    }

    public async Task<AllocationByAsset?> GetAllocationByPredicateAsync(Expression<Func<AllocationByAsset, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                  .FirstOrDefaultAsync(predicate, ct);
    }

    public async Task AddAllocationAsync(AllocationByAsset allocation, CancellationToken ct)
    {
        await _dbContext.AllocationsByAsset.AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAllocationAsync(AllocationByAsset allocation, CancellationToken ct)
    {
        _dbContext.AllocationsByAsset.Update(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAllocationAsync(AllocationByAsset allocation, CancellationToken ct)
    {
        _dbContext.Remove(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<AllocationByAsset?> GetAllocationByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                  .Include(a => a.AssetInPortfolio)
                                                  .ThenInclude(a => a.Asset)
                                                  .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<PaginatedList<AllocationByAsset>> GetAllocationsPaginatedAsync(AllocationByAssetsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByAsset.AsNoTracking()
                                                 .Include(a => a.AssetInPortfolio)
                                                 .ThenInclude(a => a.Asset)
                                                 .AsQueryable();

        query = query.Where(allocation => allocation.Portfolio.UserId == filter.UserId);

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.AssetId.HasValue)
            query = query.Where(allocation => allocation.AssetInPortfolioId == filter.AssetId.Value);
        else if (filter.AssetIds != null && filter.AssetIds.Any())
            query = query.Where(allocation => filter.AssetIds.Contains(allocation.AssetInPortfolio.AssetId));

        if (!string.IsNullOrEmpty(filter.AssetTicker))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Ticker.Contains(filter.AssetTicker));

        if (!string.IsNullOrEmpty(filter.AssetName))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Name.Contains(filter.AssetName));

        if (!string.IsNullOrEmpty(filter.AssetDescription))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Description.Contains(filter.AssetDescription));

        if (filter.AssetPrice.HasValue)
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Price == filter.AssetPrice.Value);

        if (filter.Recommendation.HasValue)
            query = query.Where(allocation => allocation.Recommendation == filter.Recommendation.Value);

        if (filter.ProductId.HasValue)
            query = query.Where(allocation => allocation.AssetInPortfolio.PortfolioId == filter.ProductId.Value);

        if (!string.IsNullOrEmpty(filter.ProductName))
            query = query.Where(allocation => allocation.AssetInPortfolio.Portfolio.Name.Contains(filter.ProductName));

        if (!string.IsNullOrEmpty(filter.ProductDescription))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Description.Contains(filter.ProductDescription));

        if (filter.Risk.HasValue)
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Product.Risk == filter.Risk.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Product.CategoryId == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Product.Category.Name.Contains(filter.CategoryName));

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(allocation => allocation.AssetInPortfolio.Asset.Product.Category.Description.Contains(filter.CategoryDescription));

        if (filter.CurrentAmount.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.CurrentAmount == filter.CurrentAmount.Value);

        if (filter.TargetPercentage.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.TargetPercentage == filter.TargetPercentage.Value);

        if (filter.CurrentPercentage.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.CurrentPercentage == filter.CurrentPercentage.Value);

        if (filter.PercentageDifference.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.PercentageDifference == filter.PercentageDifference.Value);

        if (filter.AmountDifference.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.AmountDifference == filter.AmountDifference.Value);

        var count = await query.CountAsync(ct);

        var items = count == 0 ? [] : await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);

        return new(items, count, filter.PageNumber, filter.PageSize);
    }
}
