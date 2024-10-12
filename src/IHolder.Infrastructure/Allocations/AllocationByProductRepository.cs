using IHolder.Application.Allocations.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
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

    public async Task<PaginatedList<AllocationByProduct>> GetPaginatedAsync(AllocationByProductsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByProduct.AsNoTracking().Include(a => a.Product).AsQueryable();

        query = query.Where(allocation => allocation.Portfolio.UserId == filter.UserId);

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.ProductId.HasValue)
            query = query.Where(allocation => allocation.ProductId == filter.ProductId.Value);
        else if (filter.ProductIds != null && filter.ProductIds.Any())
            query = query.Where(allocation => filter.ProductIds.Contains(allocation.ProductId));

        if (!string.IsNullOrEmpty(filter.ProductName))
            query = query.Where(allocation => allocation.Product.Name.Contains(filter.ProductName));

        if (!string.IsNullOrEmpty(filter.ProductDescription))
            query = query.Where(allocation => allocation.Product.Description.Contains(filter.ProductDescription));

        if (filter.Recommendation.HasValue)
            query = query.Where(allocation => allocation.Recommendation == filter.Recommendation.Value);

        if (filter.Risk.HasValue)
            query = query.Where(allocation => allocation.Product.Risk == filter.Risk.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(allocation => allocation.Product.CategoryId == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(allocation => allocation.Product.Category.Name.Contains(filter.CategoryName));

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(allocation => allocation.Product.Category.Description.Contains(filter.CategoryDescription));

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
