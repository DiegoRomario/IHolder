using IHolder.Application.Allocations.List;
using IHolder.Application.Assets.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Domain.Assets;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.Common;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Assets;

internal class AssetRepository(IHolderDbContext _dbContext) : IAssetRepository
{
    public Task<Asset?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _dbContext.Assets.AsNoTracking()
                         .Include(a => a.Product)
                         .ThenInclude(p => p.Category)
                         .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public Task<string?> GetExchangeIdByAssetTickerAsync(string ticker, CancellationToken ct)
    {
        return _dbContext.Assets
                         .AsNoTracking()
                         .Where(a => a.Ticker == ticker)
                         .Select(a => a.Product!.ExchangeId)
                         .FirstOrDefaultAsync(ct);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Assets.AsNoTracking()
                                .AnyAsync(predicate, ct);
    }

    public async Task<PaginatedList<Asset>> GetPaginatedAsync(AssetsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.Assets.AsNoTracking()
                                      .Include(p => p.Product)
                                      .ThenInclude(p => p.Category)
                                      .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(asset => asset.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(asset => asset.Description.Contains(filter.Description));

        if (!string.IsNullOrEmpty(filter.Ticker))
            query = query.Where(asset => asset.Ticker.Contains(filter.Ticker));

        if (filter.Id.HasValue)
            query = query.Where(asset => asset.Id == filter.Id.Value);

        if (filter.MinPrice.HasValue || filter.MaxPrice.HasValue)
        {
            var minPrice = filter.MinPrice ?? 0;
            var maxPrice = filter.MaxPrice ?? Constants.MaxDecimal;
            query = query.Where(product => product.Price >= minPrice && product.Price <= maxPrice);
        }

        if (filter.ProductId is not null && filter.ProductId != Guid.Empty)
            query = query.Where(asset => asset.ProductId == filter.ProductId.Value);

        if (!string.IsNullOrEmpty(filter.ProductName))
            query = query.Where(asset => asset.Product.Name.Contains(filter.ProductName));

        if (filter.CategoryId is not null && filter.CategoryId != Guid.Empty)
            query = query.Where(asset => asset.Product.CategoryId == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(asset => asset.Product.Category.Name.Contains(filter.CategoryName));

        var count = await query.CountAsync(ct);

        var items = count == 0 ? [] : await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);

        return new(items, count, filter.PageNumber, filter.PageSize);
    }

    public async Task AddAsync(Asset asset, CancellationToken ct)
    {
        await _dbContext.AddAsync(asset, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Asset asset, CancellationToken ct)
    {
        _dbContext.Update(asset);
        await _dbContext.SaveChangesAsync(ct);
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
