﻿using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using MediatR;

namespace IHolder.Application.Users.Login;

public class LoginQueryHandler(IJwtTokenGenerator _jwtTokenGenerator, IPasswordHasher _passwordHasher, IUserRepository _userRepository) : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken ct)
    {
        var user = await _userRepository.GetByPredicateAsync(u => u.Email == query.Email, ct);

        return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
               ? AuthenticationErrors.InvalidCredentials
               : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }
}