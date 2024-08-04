using IHolder.Domain.Common;

namespace IHolder.Domain.Categories;

public class Category : AggregateRoot
{
    public Category(string name, string description, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }
    private Category() { }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
}
