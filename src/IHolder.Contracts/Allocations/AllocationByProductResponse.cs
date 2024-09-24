namespace IHolder.Contracts.Allocations;

public record AllocationByProductResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string ProductDescription,
    byte Recommendation,
    decimal CurrentAmount,
    decimal TargetPercentage,
    decimal CurrentPercentage,
    decimal PercentageDifference,
    decimal AmountDifference,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null);


