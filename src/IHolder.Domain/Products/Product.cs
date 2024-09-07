using IHolder.Domain.Categories;
using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Products.Events;

namespace IHolder.Domain.Products;

public class Product : AggregateRoot
{
    public Product(string name, string description, Guid categoryId, Risk risk, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        Risk = risk;
        _domainEvents.Add(new ProductCreatedEvent(Id));
    }

    private Product() { }

    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public Risk Risk { get; private set; }
    public Category Category { get; private set; } = default!;
}
