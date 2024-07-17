using IHolder.Domain.Common;
using Encryptor = BCrypt.Net.BCrypt;

namespace IHolder.Infrastructure.Authentication;
public partial class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => Encryptor.EnhancedHashPassword(password);
    public bool IsCorrectPassword(string password, string hash) => Encryptor.EnhancedVerify(password, hash);
}