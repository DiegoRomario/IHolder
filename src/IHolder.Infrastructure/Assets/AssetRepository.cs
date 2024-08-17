using IHolder.Application.Assets.List;
using IHolder.Application.Common.Interfaces;
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

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Assets.AsNoTracking()
                                .AnyAsync(predicate, ct);
    }

    public async Task<bool> HasAllocationsAsync(Guid allocationByAssetId, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                  .AnyAsync(allocation => allocation.Id == allocationByAssetId, ct);
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
}
