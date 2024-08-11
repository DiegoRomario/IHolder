using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Portfolios;

internal class PortfolioRepository(IHolderDbContext _dbContext) : IPortfolioRepository
{
    public Task<Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return _dbContext.Portfolios.AsNoTracking()
                                    .Include(p => p.User)
                                    .Include(p => p.AssetsInPortfolio)
                                        .ThenInclude(a => a.Asset)
                                    .Include(p => p.AllocationsByCategory)
                                        .ThenInclude(ac => ac.Category)
                                    .Include(p => p.AllocationsByProduct)
                                        .ThenInclude(ap => ap.Product)
                                    .Include(p => p.AllocationsByAsset)
                                        .ThenInclude(aa => aa.AssetInPortfolio)
                                        .ThenInclude(a => a.Asset)
                                    .FirstOrDefaultAsync(p => p.UserId == userId, ct);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Portfolios.AsNoTracking()
                                    .AnyAsync(predicate, ct);
    }

    public async Task AddAsync(Portfolio portfolio, CancellationToken ct)
    {
        await _dbContext.AddAsync(portfolio, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Portfolio portfolio, CancellationToken ct)
    {
        _dbContext.Update(portfolio);
        await _dbContext.SaveChangesAsync(ct);
    }
}
