using IHolder.Domain.Common;

namespace IHolder.Domain.Categories;

public class Category : AggregateRoot
{
    public Category(string description, string details, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Description = description;
        Details = details;
    }
    private Category() { }
    public string Description { get; } = string.Empty;
    public string Details { get; } = string.Empty;
}
