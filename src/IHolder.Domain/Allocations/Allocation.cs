using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;

namespace IHolder.Domain.Allocations;

public abstract class Allocation : Entity
{
    private const decimal ACCEPTABLE_EXCESS_DIFFERENCE_PERCENTAGE = 50;
    protected Allocation() { }

    // todo: review id base class
    protected Allocation(decimal targetPercentage, Guid userId)
    {
        // todo: review new
        PercentagesAndValues = new AllocationValues(targetPercentage);
        Recommendation = Recommendation.Hold;
        UserId = userId;
    }
    public AllocationValues PercentagesAndValues { get; init; } = default!;
    public Guid UserId { get; private set; }
    public Recommendation Recommendation { get; protected set; }

    public void GenerateRecommendation(decimal amountInvestedPerAllocation, decimal totalAmountInvested)
    {
        PercentagesAndValues.RecalculateValues(amountInvestedPerAllocation, totalAmountInvested);
        Recommendation = ProcessRecommendation();
    }

    protected bool ExceedsPercentageDifference()
    {
        return PercentagesAndValues.CurrentPercentage > PercentagesAndValues.TargetPercentage +
               (PercentagesAndValues.TargetPercentage * ACCEPTABLE_EXCESS_DIFFERENCE_PERCENTAGE / 100);
    }

    protected virtual Recommendation ProcessRecommendation()
    {
        if (ExceedsPercentageDifference())
            return Recommendation.Sell;
        else if (PercentagesAndValues.PercentageDifference <= 0)
            return Recommendation.Hold;

        return Recommendation.Buy;
    }
}
