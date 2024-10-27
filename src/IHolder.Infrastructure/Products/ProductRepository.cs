using IHolder.Application.Allocations.List;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Products.List;
using IHolder.Domain.Allocations;
using IHolder.Domain.Products;
using IHolder.Infrastructure.Database;
using IHolder.SharedKernel.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Products;

internal class ProductRepository(IHolderDbContext _dbContext) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(Product => Product.Id == id, ct);
    }

    public async Task<Product?> GetByPredicateAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(predicate, ct);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Product, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Products.AsNoTracking()
                                .AnyAsync(predicate, ct);
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

    // Allocation

    public async Task<bool> ExistsAllocationByPredicateAsync(Expression<Func<AllocationByProduct, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByProduct.AsNoTracking()
                                                     .AnyAsync(predicate, ct);
    }

    public async Task<AllocationByProduct?> GetAllocationByPredicateAsync(Expression<Func<AllocationByProduct, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.AllocationsByProduct.AsNoTracking()
                                                     .FirstOrDefaultAsync(predicate, ct);
    }

    public async Task AddAllocationAsync(AllocationByProduct allocation, CancellationToken ct)
    {
        await _dbContext.AllocationsByProduct.AddAsync(allocation, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAllocationAsync(AllocationByProduct allocation, CancellationToken ct)
    {
        _dbContext.AllocationsByProduct.Update(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAllocationAsync(AllocationByProduct allocation, CancellationToken ct)
    {
        _dbContext.Remove(allocation);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<AllocationByProduct?> GetAllocationByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.AllocationsByProduct.AsNoTracking()
                                                    .Include(a => a.Product)
                                                    .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<PaginatedList<AllocationByProduct>> GetAllocationsPaginatedAsync(AllocationByProductsPaginatedListFilter filter, CancellationToken ct)
    {
        var query = _dbContext.AllocationsByProduct.AsNoTracking().Include(a => a.Product).AsQueryable();

        query = query.Where(allocation => allocation.Portfolio.UserId == filter.UserId);

        if (filter.Id.HasValue)
            query = query.Where(allocation => allocation.Id == filter.Id.Value);

        if (filter.ProductId.HasValue)
            query = query.Where(allocation => allocation.ProductId == filter.ProductId.Value);
        else if (filter.ProductIds != null && filter.ProductIds.Any())
            query = query.Where(allocation => filter.ProductIds.Contains(allocation.ProductId));

        if (!string.IsNullOrEmpty(filter.ProductName))
            query = query.Where(allocation => allocation.Product.Name.Contains(filter.ProductName));

        if (!string.IsNullOrEmpty(filter.ProductDescription))
            query = query.Where(allocation => allocation.Product.Description.Contains(filter.ProductDescription));

        if (filter.Recommendation.HasValue)
            query = query.Where(allocation => allocation.Recommendation == filter.Recommendation.Value);

        if (filter.Risk.HasValue)
            query = query.Where(allocation => allocation.Product.Risk == filter.Risk.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(allocation => allocation.Product.CategoryId == filter.CategoryId.Value);

        if (!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(allocation => allocation.Product.Category.Name.Contains(filter.CategoryName));

        if (!string.IsNullOrEmpty(filter.CategoryDescription))
            query = query.Where(allocation => allocation.Product.Category.Description.Contains(filter.CategoryDescription));

        if (filter.CurrentAmount.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.CurrentAmount == filter.CurrentAmount.Value);

        if (filter.TargetPercentage.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.TargetPercentage == filter.TargetPercentage.Value);

        if (filter.CurrentPercentage.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.CurrentPercentage == filter.CurrentPercentage.Value);

        if (filter.PercentageDifference.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.PercentageDifference == filter.PercentageDifference.Value);

        if (filter.AmountDifference.HasValue)
            query = query.Where(allocation => allocation.AllocationValues.AmountDifference == filter.AmountDifference.Value);

        var count = await query.CountAsync(ct);

        var items = count == 0 ? [] : await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);

        return new(items, count, filter.PageNumber, filter.PageSize);
    }
}
