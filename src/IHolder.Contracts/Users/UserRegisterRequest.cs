namespace IHolder.Contracts.Users;

public record UserRegisterRequest(string FirstName, string LastName, string Email, string Password) { }
