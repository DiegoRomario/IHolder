namespace IHolder.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    protected Entity(Guid id) => Id = id;

    protected Entity() { }
}
