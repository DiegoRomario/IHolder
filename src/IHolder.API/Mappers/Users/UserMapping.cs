using IHolder.Application.Common.Auth;
using IHolder.Application.Users.Update;
using IHolder.Contracts.Users;
using IHolder.Domain.Users;

namespace IHolder.API.Mappers.Users;

public static class UserMapping
{
    public static AuthenticationResponse ToResponse(this AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
    }

    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email);
    }

    public static UserUpdateCommand ToUpdateCommand(this UserUpdateRequest request, Guid id)
    {
        return new UserUpdateCommand(id, request.FirstName, request.LastName, request.Email, request.Password, request.PasswordConfirmation);
    }

}
