using IHolder.Application.Assets.List;
using IHolder.Application.Products.List;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByDescriptionAsync(string description);
    Task<PaginatedList<Product>> GetPaginatedAsync(ProductPaginatedListFilter filter);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByCategoryIdAsync(Guid categoryId);
    Task<bool> HasAllocationsAsync(Guid productId);
    Task AddAsync(Product Product);
    Task UpdateAsync(Product Product);
    Task DeleteAsync(Product Product);
}