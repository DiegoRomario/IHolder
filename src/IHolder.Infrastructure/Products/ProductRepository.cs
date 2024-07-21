using IHolder.Application.Common;
using IHolder.Application.Products.List;
using IHolder.Domain.Products;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Products;

internal class ProductRepository(IHolderDbContext _dbContext) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Guid Id)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(Product => Product.Id == Id);
    }

    public async Task<Product?> GetByDescriptionAsync(string description)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .FirstOrDefaultAsync(Product => Product.Description == description);
    }

    public async Task<bool> ExistsByIdAsync(Guid Id)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .AnyAsync(Product => Product.Id == Id);
    }

    public async Task AddAsync(Product Product)
    {
        await _dbContext.AddAsync(Product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product Product)
    {
        _dbContext.Update(Product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PaginatedList<Product>> GetPaginatedAsync(ProductPaginatedListFilter filter)
    {
        var query = _dbContext.Products.AsNoTracking()
                                       .Include(p => p.Category)
                                       .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(product => product.Description.Contains(filter.Description));

        if (!string.IsNullOrEmpty(filter.Details))
            query = query.Where(product => product.Details.Contains(filter.Details));

        if (filter.Id.HasValue)
            query = query.Where(product => product.Id == filter.Id.Value);

        if (filter.Risk.HasValue)
            query = query.Where(product => product.Risk == filter.Risk.Value);

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(product => product.Category.Description.Contains(filter.CategoryDescription));

        var count = await query.CountAsync();
        var items = await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

        return new PaginatedList<Product>(items, count, filter.PageNumber, filter.PageSize);
    }

}
