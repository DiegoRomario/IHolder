using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Portfolios;

namespace IHolder.Domain.Allocations;

public abstract class Allocation : Entity
{
    private const decimal ACCEPTABLE_EXCESS_DIFFERENCE_PERCENTAGE = 50;
    protected Allocation() { }

    // todo: review id base class
    protected Allocation(decimal targetPercentage, Guid portfolioId)
    {
        // todo: review new
        AllocationValues = new AllocationValues(targetPercentage);
        Recommendation = Recommendation.Hold;
        PortfolioId = portfolioId;
    }

    public Guid PortfolioId { get; private set; }
    public AllocationValues AllocationValues { get; private set; } = default!;
    public Recommendation Recommendation { get; protected set; }

    public Portfolio Portfolio { get; private set; } = default!;

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
