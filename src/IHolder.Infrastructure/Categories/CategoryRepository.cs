using IHolder.Application.Allocations.List;
using IHolder.Application.Categories.List;
using IHolder.Application.Common;
using IHolder.Domain.Allocations;
using IHolder.Domain.Categories;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Categories;

internal class CategoryRepository(IHolderDbContext _dbContext) : ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid Id, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == Id, ct);
    }

    public async Task<Category?> GetByPredicateAsync(Expression<Func<Category, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Name == name, ct);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Category, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Categories.AsNoTracking()
                                .AnyAsync(predicate, ct);
    }

    public async Task AddAsync(Category category, CancellationToken ct)
    {
        await _dbContext.AddAsync(category, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Category category, CancellationToken ct)
    {
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<PaginatedList<Category>> GetPaginatedAsync(CategoriesPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.Categories.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(category => category.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(category => category.Description.Contains(filter.Description));

        if (filter.Id.HasValue)
            query = query.Where(category => category.Id == filter.Id.Value);

        var count = await query.CountAsync(ct);

        var items = count == 0 ? [] : await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);

        return new(items, count, filter.PageNumber, filter.PageSize);
    }

    public async Task DeleteAsync(Category category, CancellationToken ct)
    {
        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync(ct);
    }

    // Allocation

    public async Task<bool> ExistsAllocationByPredicateAsync(Expression<Func<AllocationByCategory, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByCategory.AsNoTracking()
                                                     .AnyAsync(predicate, ct);
    }

    public async Task<AllocationByCategory?> GetAllocationByPredicateAsync(Expression<Func<AllocationByCategory, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByCategory.AsNoTracking()
                                                     .FirstOrDefaultAsync(predicate, ct);
    }

    public async Task AddAllocationAsync(AllocationByCategory allocation, CancellationToken ct)
    {
        await _dbContext.AllocationsByCategory.AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAllocationAsync(AllocationByCategory allocation, CancellationToken ct)
    {
        _dbContext.AllocationsByCategory.Update(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAllocationAsync(AllocationByCategory allocation, CancellationToken ct)
    {
        _dbContext.Remove(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<AllocationByCategory?> GetAllocationByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByCategory.AsNoTracking()
                                                     .Include(a => a.Category)
                                                     .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<PaginatedList<AllocationByCategory>> GetAllocationsPaginatedAsync(AllocationByCategoriesPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByCategory.AsNoTracking().Include(a => a.Category).AsQueryable();

        query = query.Where(allocation => allocation.Portfolio.UserId == filter.UserId);

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(allocation => allocation.Category.Id == filter.CategoryId.Value);
        else if (filter.CategoryIds != null && filter.CategoryIds.Any())
            query = query.Where(allocation => filter.CategoryIds.Contains(allocation.Category.Id));

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
