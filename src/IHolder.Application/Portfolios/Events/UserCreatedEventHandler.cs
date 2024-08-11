using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Users.Events;
using MediatR;

namespace IHolder.Application.Portfolios.Events;

internal class UserCreatedEventHandler(IPortfolioRepository _repository) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent userCreatedEvent, CancellationToken ct)
    {
        var portfolio = userCreatedEvent.ToEntity();
        await _repository.AddAsync(portfolio, ct);
    }
}
