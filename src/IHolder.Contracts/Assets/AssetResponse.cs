namespace IHolder.Contracts.Assets;

public record AssetResponse(
    Guid Id,
    string Description,
    string Details,
    string Ticker,
    decimal Price,
    Guid ProductId,
    string ProductDescription,
    Guid CategoryId,
    string CategoryDescription,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null);
