using IHolder.Domain.Allocations;
using IHolder.Domain.Assets;
using IHolder.Domain.Common;

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
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    private string _passwordHash = null!;


    public IEnumerable<AllocationByCategory> AllocationsByCategory { get; private set; } = [];
    public IEnumerable<AllocationByAsset> AllocationsByAsset { get; private set; } = [];
    public IEnumerable<AllocationByProduct> AllocationsByProduct { get; private set; } = [];
    public IEnumerable<AssetInPortfolio> AssetsInPortfolio { get; private set; } = [];

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