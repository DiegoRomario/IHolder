using IHolder.Application.Portfolios.Update;
using IHolder.Domain.Portfolios;
using IHolder.Domain.Users.Events;

namespace IHolder.Application.Portfolios.Mappers;

public static class PortfolioCommandsMapping
{
    public static Portfolio ToEntity(this UserCreatedEvent userCreatedEvent)
    {
        return new Portfolio(userCreatedEvent.UserId, $"{userCreatedEvent.FirstName} {userCreatedEvent.LastName}'s portfolio"); // TODO: i18n
    }

    public static Portfolio ToEntity(this PortfolioUpdateCommand command)
    {
        return new Portfolio(command.UserId, command.Name, id: command.Id);
    }
}
