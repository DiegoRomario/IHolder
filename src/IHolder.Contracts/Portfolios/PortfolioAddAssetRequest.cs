namespace IHolder.Contracts.Portfolios;

public record PortfolioAddAssetRequest(
    Guid AssetId,
    decimal AveragePrice,
    decimal Quantity,
    DateTime FirstInvestmentDate);
