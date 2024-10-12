using IHolder.Application.Allocations.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
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

    public async Task<PaginatedList<AllocationByAsset>> GetPaginatedAsync(AllocationByAssetsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByAsset.AsNoTracking()
                                                 .Include(a => a.AssetInPortfolio)
                                                 .ThenInclude(a => a.Asset)
                                                 .AsQueryable();

        query = query.Where(allocation => allocation.Portfolio.UserId == filter.UserId);

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.AssetId.HasValue)
            query = query.Where(allocation => allocation.AssetId == filter.AssetId.Value);
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
