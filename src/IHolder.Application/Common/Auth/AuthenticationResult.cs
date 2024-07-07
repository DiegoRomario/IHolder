using IHolder.Domain.Users;

namespace IHolder.Application.Common.Authorization;
public record AuthenticationResult(User User, string Token);