using IHolder.Domain.Common;
using IHolder.Domain.Portfolios;

namespace IHolder.Domain.Users;

public class User : AggregateRoot
{
    public User(string firstName, string lastName, string email, string passwordHash, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
    }

    private User() { }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private string _passwordHash = string.Empty;

    public Portfolio Portfolio { get; set; } = default!;

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public void UpdateUserDetails(string firstName, string lastName, string email, string? passwordHash = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        if (!string.IsNullOrEmpty(passwordHash)) _passwordHash = passwordHash;
    }
}