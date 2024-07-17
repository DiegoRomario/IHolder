namespace IHolder.Contracts.Users;

public record UserCreateRequest(string FirstName, string LastName, string Email, string Password, string PasswordConfirmation) { }
