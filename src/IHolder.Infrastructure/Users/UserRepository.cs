using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Users;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IHolder.Infrastructure.Users;
public class UserRepository(IHolderDbContext _dbContext) : IUserRepository
{
    public async Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(predicate, ct);
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct)
    {
        return _dbContext.Users.AsNoTracking()
                                .AnyAsync(predicate, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _dbContext.AddAsync(user, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync(ct);
    }
}
