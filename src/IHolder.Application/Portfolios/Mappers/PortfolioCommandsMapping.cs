using IHolder.Application.Portfolios.Create;
using IHolder.Application.Portfolios.Update;
using IHolder.Domain.Portfolios;

namespace IHolder.Application.Portfolios.Mappers;

public static class PortfolioCommandsMapping
{
    public static Portfolio ToEntity(this PortfolioCreateCommand command)
    {
        return new Portfolio(command.UserId, command.Name);
    }

    public static Portfolio ToEntity(this PortfolioUpdateCommand command)
    {
        return new Portfolio(command.UserId, command.Name, id: command.Id);
    }
}
