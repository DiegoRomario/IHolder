namespace IHolder.Contracts.Allocations;

public record AllocationByCategoryResponse(
    Guid Id,
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    byte Recommendation,
    decimal CurrentAmount,
    decimal TargetPercentage,
    decimal CurrentPercentage,
    decimal PercentageDifference,
    decimal AmountDifference,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null);


