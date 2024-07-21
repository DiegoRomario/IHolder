using IHolder.Application.Products.List;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByDescriptionAsync(string description);
    Task<PaginatedList<Product>> GetPaginatedAsync(ProductPaginatedListFilter filter);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByCategoryIdAsync(Guid categoryId);
    Task AddAsync(Product Product);
    Task UpdateAsync(Product Product);
}