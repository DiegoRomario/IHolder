namespace IHolder.Contracts.Assets;

public record AssetCreateRequest(
    Guid ProductId,
    string Description,
    string Details,
    string Ticker,
    decimal Price);

