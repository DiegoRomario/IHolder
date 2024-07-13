using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using IHolder.Domain.Users;
using MediatR;

namespace IHolder.Application.Users.Register;

public class UserRegisterCommandHandler(IUserRepository _userRepository, IJwtTokenGenerator _jwtTokenGenerator, IPasswordHasher _passwordHasher) : IRequestHandler<UserRegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email)) return Error.Conflict(description: "User already exists");

        var hashPasswordResult = _passwordHasher.HashPassword(request.Password);

        if (hashPasswordResult.IsError) return hashPasswordResult.Errors;

        var user = new User(request.FirstName, request.LastName, request.Email, hashPasswordResult.Value);

        await _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
