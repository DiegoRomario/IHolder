using IHolder.Application.Categories.List;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid Id, CancellationToken ct);
    Task<Category?> GetByNameAsync(string name, CancellationToken ct);
    Task<PaginatedList<Category>> GetPaginatedAsync(CategoriesPaginatedListFilter filter, CancellationToken ct);
    Task<bool> ExistsByIdAsync(Guid Id, CancellationToken ct);
    Task AddAsync(Category category, CancellationToken ct);
    Task UpdateAsync(Category category, CancellationToken ct);
    Task DeleteAsync(Category category, CancellationToken ct);
}