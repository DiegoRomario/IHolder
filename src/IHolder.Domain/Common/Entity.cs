namespace IHolder.Domain.Common;

public abstract class Entity
{
    protected Entity(Guid id) => Id = id;
    protected Entity() { }
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

}
