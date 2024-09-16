using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using IHolder.Domain.Users;
using MediatR;

namespace IHolder.Application.Users.Create;

public class UserCreateCommandHandler(IUserRepository _userRepository, IJwtTokenGenerator _jwtTokenGenerator, IPasswordHasher _passwordHasher) : IRequestHandler<UserCreateCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(UserCreateCommand request, CancellationToken ct)
    {
        if (await _userRepository.ExistsByPredicateAsync(u => u.Email == request.Email, ct)) return Error.Conflict(description: "User already exists");

        var hashPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(request.FirstName, request.LastName, request.Email, hashPassword);

        await _userRepository.AddAsync(user, ct);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
