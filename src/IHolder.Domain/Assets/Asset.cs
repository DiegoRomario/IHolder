using IHolder.Domain.Common;
using IHolder.Domain.Products;

namespace IHolder.Domain.Assets;

public class Asset : AggregateRoot
{
    public Asset(Guid productId, string name, string description, string ticker, decimal price, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Ticker = ticker;
        Price = price;
    }

    private Asset() { }
    public Guid ProductId { get; private set; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public string Ticker { get; } = string.Empty;
    public decimal Price { get; private set; }
    public Product Product { get; private set; } = default!;

    public void UpdatePrice(decimal newPrice) => Price = newPrice;
}
