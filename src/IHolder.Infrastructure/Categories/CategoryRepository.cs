using IHolder.Application.Categories.List;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Categories;

internal class CategoryRepository(IHolderDbContext _dbContext) : ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid Id)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == Id);
    }

    public async Task<Category?> GetByDescriptionAsync(string description)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Description == description);
    }

    public async Task<bool> ExistsByIdAsync(Guid Id)
    {
        return await _dbContext.Categories.AsNoTracking().AnyAsync(category => category.Id == Id);
    }

    public async Task AddAsync(Category category)
    {
        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PaginatedList<Category>> GetPaginatedAsync(CategoryPaginatedListFilter filter)
    {
        var query = _dbContext.Categories.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(category => category.Description.Contains(filter.Description));

        if (!string.IsNullOrEmpty(filter.Details))
            query = query.Where(category => category.Details.Contains(filter.Details));

        if (filter.Id.HasValue)
            query = query.Where(category => category.Id == filter.Id.Value);

        var count = await query.CountAsync();
        var items = await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

        return new PaginatedList<Category>(items, count, filter.PageNumber, filter.PageSize);
    }

    public async Task DeleteAsync(Category category)
    {
        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}
