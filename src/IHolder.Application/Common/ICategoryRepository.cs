using IHolder.Application.Categories.List;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using System.Linq.Expressions;

namespace IHolder.Application.Common;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid Id, CancellationToken ct);
    Task<Category?> GetByPredicateAsync(Expression<Func<Category, bool>> predicate, CancellationToken ct);
    Task<PaginatedList<Category>> GetPaginatedAsync(CategoriesPaginatedListFilter filter, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Category, bool>> predicate, CancellationToken ct);
    Task AddAsync(Category category, CancellationToken ct);
    Task UpdateAsync(Category category, CancellationToken ct);
    Task DeleteAsync(Category category, CancellationToken ct);
}