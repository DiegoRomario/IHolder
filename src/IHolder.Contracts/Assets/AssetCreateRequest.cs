namespace IHolder.Contracts.Assets;

public record AssetCreateRequest(
    Guid ProductId,
    string Name,
    string Description,
    string Ticker,
    decimal Price);

