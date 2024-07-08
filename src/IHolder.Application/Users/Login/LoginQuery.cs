using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Users.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;