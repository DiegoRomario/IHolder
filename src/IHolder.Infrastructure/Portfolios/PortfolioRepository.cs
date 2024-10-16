﻿using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
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

    public async Task<bool> HasAllocationsByAssetInPortfolioAsync(Guid assetInPortfolioId, CancellationToken ct)
    {
        return await _dbContext.AllocationsByAsset.AsNoTracking()
                                                  .AnyAsync(allocation => allocation.AssetId == assetInPortfolioId, ct);
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
}
