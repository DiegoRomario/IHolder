namespace IHolder.Contracts.Assets;

public record AssetUpdateRequest(
    Guid ProductId,
    string Name,
    string Description,
    string Ticker,
    decimal Price);
