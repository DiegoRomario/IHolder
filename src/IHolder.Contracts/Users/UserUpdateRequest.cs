namespace IHolder.Contracts.Users;

public record UserUpdateRequest(string FirstName, string LastName, string Email, string? Password, string? PasswordConfirmation) { }
