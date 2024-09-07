namespace IHolder.Contracts.Portfolios;

public record PortfolioUpdateAssetRequest(
    decimal AveragePrice,
    decimal Quantity,
    DateTime FirstInvestmentDate);
