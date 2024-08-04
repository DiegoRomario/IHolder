using IHolder.Application.Users.Create;
using IHolder.Domain.Users;

namespace IHolder.Application.Users.Mappers;

public static class UserCommandsMapping
{
    public static User ToEntity(this UserCreateCommand command)
    {
        return new User(command.FirstName, command.LastName, command.Email, command.Password);
    }
}
