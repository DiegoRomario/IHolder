using ErrorOr;
using IHolder.Domain.Common;

namespace IHolder.Domain.Categories.Events;

public record CategoryCreatedEvent(Guid CategoryId) : IDomainEvent
{
    public static readonly Error PortfolioNotFound = EventualConsistencyError.From(
    code: "CategoryCreatedEvent.PortfolioNotFound",
    description: "Portfolio not found");
}