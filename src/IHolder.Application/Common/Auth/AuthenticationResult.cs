using IHolder.Domain.Users;

namespace IHolder.Application.Common.Auth;

public record AuthenticationResult(User User, string Token);