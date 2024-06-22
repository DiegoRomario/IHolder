using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;
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
        State = State.Regular;
        StateSetAt = DateTime.Now;
    }

    private Asset() { }
    public Guid ProductId { get; private set; }
    public string Description { get; } = string.Empty;
    public string Details { get; } = string.Empty;
    public string Ticker { get; } = string.Empty;
    public decimal Price { get; private set; }
    public State State { get; private set; }
    public DateTime StateSetAt { get; private set; }
    public Product Product { get; private set; } = default!;

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void UpdateState(State state)
    {
        State = state;
        StateSetAt = DateTime.Now;
    }
}
