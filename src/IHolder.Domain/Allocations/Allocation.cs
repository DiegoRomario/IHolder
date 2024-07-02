using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Users;

namespace IHolder.Domain.Allocations;

public abstract class Allocation : Entity
{
    private const decimal ACCEPTABLE_EXCESS_DIFFERENCE_PERCENTAGE = 50;
    protected Allocation() { }

    // todo: review id base class
    protected Allocation(decimal targetPercentage, Guid userId)
    {
        // todo: review new
        AllocationValues = new AllocationValues(targetPercentage);
        Recommendation = Recommendation.Hold;
        UserId = userId;
    }
    public AllocationValues AllocationValues { get; init; } = default!;
    public User User { get; private set; } = default!;
    public Guid UserId { get; private set; }
    public Recommendation Recommendation { get; protected set; }

    public void GenerateRecommendation(decimal amountInvestedPerAllocation, decimal totalAmountInvested)
    {
        AllocationValues.RecalculateValues(amountInvestedPerAllocation, totalAmountInvested);
        Recommendation = ProcessRecommendation();
    }

    protected bool ExceedsPercentageDifference()
    {
        return AllocationValues.CurrentPercentage > AllocationValues.TargetPercentage +
               (AllocationValues.TargetPercentage * ACCEPTABLE_EXCESS_DIFFERENCE_PERCENTAGE / 100);
    }

    protected virtual Recommendation ProcessRecommendation()
    {
        if (ExceedsPercentageDifference())
            return Recommendation.Sell;
        else if (AllocationValues.PercentageDifference <= 0)
            return Recommendation.Hold;

        return Recommendation.Buy;
    }
}
