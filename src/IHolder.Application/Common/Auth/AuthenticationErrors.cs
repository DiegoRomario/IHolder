using ErrorOr;

namespace IHolder.Application.Common.Auth;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials = Error.Validation(code: "Authentication.InvalidCredentials", description: "Invalid credentials");
}