using IHolder.Domain.Categories;

namespace IHolder.Application.Common;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<Category?> GetByDescriptionAsync(string description);
    Task<bool> ExistsByIdAsync(Guid Id);
    Task<Category?> GetByIdAsync(Guid Id);
    Task UpdateAsync(Category category);
}