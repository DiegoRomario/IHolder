using IHolder.Application.Common;
using IHolder.Domain.Categories;
using IHolder.Infrastructure.Database;
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
}
