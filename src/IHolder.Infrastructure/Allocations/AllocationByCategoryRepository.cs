using IHolder.Application.Allocations.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Allocations;

internal class AllocationByCategoryRepository(IHolderDbContext _dbContext) : AllocationRepository(_dbContext), IAllocationByCategoryRepository
{
    public async Task<AllocationByCategory?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByCategory.AsNoTracking()
                                                     .Include(a => a.Category)
                                                     .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<PaginatedList<AllocationByCategory>> GetPaginatedAsync(AllocationByCategoriesPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByCategory.AsNoTracking().Include(a => a.Category).AsQueryable();

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(allocation => allocation.Category.Id == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(allocation => allocation.Category.Name.Contains(filter.CategoryName));

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(allocation => allocation.Category.Description.Contains(filter.CategoryDescription));

        if (filter.Recommendation.HasValue)
            query = query.Where(allocation => allocation.Recommendation == filter.Recommendation.Value);

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
