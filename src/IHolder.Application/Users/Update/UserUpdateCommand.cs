using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Users;
using MediatR;

namespace IHolder.Application.Users.Update;

[Authorization()]
public record UserUpdateCommand
    (Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Password,
    string? PasswordConfirmation) : IRequest<ErrorOr<User>>;
