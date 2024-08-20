using IHolder.Domain.Common;

namespace IHolder.Domain.Categories.Events;

public record CategoryCreatedEvent(Guid CategoryId) : IDomainEvent;