using IHolder.Application.Common.Interfaces;
using IHolder.Application.Products.List;
using IHolder.Domain.Products;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Products;

internal class ProductRepository(IHolderDbContext _dbContext) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(Product => Product.Id == id, ct);
    }

    public async Task<Product?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .FirstOrDefaultAsync(Product => Product.Name == name, ct);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .AnyAsync(Product => Product.Id == id, ct);
    }

    public async Task<bool> ExistsByCategoryIdAsync(Guid categoryId, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .AnyAsync(product => product.CategoryId == categoryId, ct);
    }

    public async Task<bool> HasAllocationsAsync(Guid productId, CancellationToken ct)
    {
        return await _dbContext.AllocationsByProduct.AsNoTracking()
                                                    .AnyAsync(allocation => allocation.ProductId == productId, ct);
    }

    public async Task<PaginatedList<Product>> GetPaginatedAsync(ProductsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.Products.AsNoTracking()
                                       .Include(p => p.Category)
                                       .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(product => product.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(product => product.Description.Contains(filter.Description));

        if (filter.Id.HasValue)
            query = query.Where(product => product.Id == filter.Id.Value);

        if (filter.Risk.HasValue)
            query = query.Where(product => product.Risk == filter.Risk.Value);

        if (filter.CategoryId is not null && filter.CategoryId != Guid.Empty)
            query = query.Where(product => product.CategoryId == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(product => product.Category.Description.Contains(filter.CategoryDescription));

        var count = await query.CountAsync(ct);

        var items = count == 0 ? [] : await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);

        return new(items, count, filter.PageNumber, filter.PageSize);
    }

    public async Task AddAsync(Product product, CancellationToken ct)
    {
        await _dbContext.AddAsync(product, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Product product, CancellationToken ct)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Product product, CancellationToken ct)
    {
        _dbContext.Remove(product);
        await _dbContext.SaveChangesAsync(ct);
    }
}
