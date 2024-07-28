using IHolder.Domain.Common;
using IHolder.Domain.Products;

namespace IHolder.Domain.Assets;

public class Asset : AggregateRoot
{
    public Asset(Guid productId, string description, string details, string ticker, decimal price, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        ProductId = productId;
        Description = description;
        Details = details;
        Ticker = ticker;
        Price = price;
    }

    private Asset() { }
    public Guid ProductId { get; private set; }
    public string Description { get; } = string.Empty;
    public string Details { get; } = string.Empty;
    public string Ticker { get; } = string.Empty;
    public decimal Price { get; private set; }
    public Product Product { get; private set; } = default!;

    public void UpdatePrice(decimal newPrice) => Price = newPrice;
}
