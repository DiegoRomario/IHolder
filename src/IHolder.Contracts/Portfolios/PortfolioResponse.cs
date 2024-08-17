namespace IHolder.Contracts.Portfolios;

public record PortfolioResponse(
    Guid Id,
    string Name,
    Guid UserId,
    string UserFirstName,
    string UserLastName,
    List<AssetInPortfolioResponse> AssetsInPortfolio,
    List<AllocationByCategoryResponse> AllocationsByCategory,
    List<AllocationByProductResponse> AllocationsByProduct,
    List<AllocationByAssetResponse> AllocationsByAsset,
    decimal TotalInvestedAmount,
    decimal TotalCurrentValue,
    decimal PortfolioPerformanceValue,
    decimal PortfolioPerformancePercentage,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null)
{ }

public record AllocationBaseResponse
{
    public Guid Id { get; init; }
    public decimal AmountDifference { get; init; }
    public decimal CurrentAmount { get; init; }
    public decimal CurrentPercentage { get; init; }
    public decimal PercentageDifference { get; init; }
    public decimal TargetPercentage { get; init; }
    public string Recommendation { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public record AllocationByCategoryResponse : AllocationBaseResponse
{
    public string CategoryName { get; init; } = default!;
}

public record AllocationByProductResponse : AllocationBaseResponse
{
    public string ProductName { get; init; } = default!;
}

public record AllocationByAssetResponse : AllocationBaseResponse
{
    public string Ticker { get; init; } = default!;
}

public record AssetInPortfolioResponse(
    Guid Id,
    Guid AssetId,
    string Ticker,
    decimal AveragePrice,
    decimal Quantity,
    decimal InvestedAmount,
    DateTime FirstInvestmentDate,
    string State,
    DateTime StateSetAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null);