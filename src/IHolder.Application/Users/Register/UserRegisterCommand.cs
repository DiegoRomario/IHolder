using ErrorOr;
using IHolder.Application.Common.Authorization;
using MediatR;

namespace IHolder.Application.Users.Register;

public record UserRegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
