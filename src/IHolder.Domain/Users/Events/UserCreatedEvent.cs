using IHolder.Domain.Common;

namespace IHolder.Domain.Users.Events;

public record UserCreatedEvent(Guid UserId, string FirstName, string LastName) : IDomainEvent;
