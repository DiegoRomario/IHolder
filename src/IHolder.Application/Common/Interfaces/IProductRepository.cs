using IHolder.Application.Products.List;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Product?> GetByPredicateAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct);
    Task<PaginatedList<Product>> GetPaginatedAsync(ProductsPaginatedListFilter filter, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct);
    Task<bool> HasAllocationsAsync(Guid productId, CancellationToken ct);
    Task AddAsync(Product Product, CancellationToken ct);
    Task UpdateAsync(Product Product, CancellationToken ct);
    Task DeleteAsync(Product Product, CancellationToken ct);
}