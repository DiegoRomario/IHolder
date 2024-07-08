using IHolder.Application.Common.Auth;
using IHolder.Contracts.Users;

namespace IHolder.API.Mappers.Users;

public static class UserMapping
{
    public static AuthenticationResponse ToAuthenticationResponse(this AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
    }
}
