using IHolder.Domain.Categories;

namespace IHolder.Application.Common;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid Id);
    Task<Category?> GetByDescriptionAsync(string description);
    Task<bool> ExistsByIdAsync(Guid Id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
}