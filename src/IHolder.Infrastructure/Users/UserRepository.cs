using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Users;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IHolder.Infrastructure.Users;
public class UserRepository(IHolderDbContext _dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Id == id);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(user => user.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
