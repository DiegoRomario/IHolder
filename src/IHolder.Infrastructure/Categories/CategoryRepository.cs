using IHolder.Application.Categories.List;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Categories;

internal class CategoryRepository(IHolderDbContext _dbContext) : ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid Id, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == Id, ct);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Name == name, ct);
    }

    public async Task<bool> ExistsByIdAsync(Guid Id, CancellationToken ct)
    {
        return await _dbContext.Categories.AsNoTracking().AnyAsync(category => category.Id == Id, ct);
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

    public async Task<PaginatedList<Category>> GetPaginatedAsync(CategoryPaginatedListFilter filter, CancellationToken ct)
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
}
