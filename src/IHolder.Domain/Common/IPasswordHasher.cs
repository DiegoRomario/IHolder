namespace IHolder.Domain.Common;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    bool IsCorrectPassword(string password, string hash);
}