namespace IHolder.Contracts.Portfolios;

public record PortfolioUpdateAssetRequest(
    Guid AssetId,
    decimal AveragePrice,
    decimal Quantity,
    DateTime FirstInvestmentDate);
