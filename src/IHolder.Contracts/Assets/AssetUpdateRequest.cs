namespace IHolder.Contracts.Assets;

public record AssetUpdateRequest(
    Guid ProductId,
    string Description,
    string Details,
    string Ticker,
    decimal Price);
