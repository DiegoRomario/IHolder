using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Users.Create;

public record UserCreateCommand(string FirstName, string LastName, string Email, string Password, string PasswordConfirmation) : IRequest<ErrorOr<AuthenticationResult>>;
