using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Users;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Users;
public class UserRepository(IHolderDbContext _dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, ct);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email, ct);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Id == id, ct);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Email == email, ct);
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
