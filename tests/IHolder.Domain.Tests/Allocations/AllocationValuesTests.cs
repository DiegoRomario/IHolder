using IHolder.Domain.Allocations;

namespace IHolder.Domain.Tests.Allocations;

[Trait("Category", "unit")]
public class AllocationValuesTests
{
    private AllocationValues _allocationValues = new(0);

    [Theory(DisplayName = @"Given target percentage, amount invested per allocation and total amount invested
                            When recalculating values
                            Should calculate current percentage, amounts and differences correctly")]
    [InlineData(25, 2000, 10000, 20, 5, 500)]
    [InlineData(50, 0, 10000, 0, 50, 5000)]
    public void RecalculateValuesTest(
                decimal targetPercentage,
                decimal amountInvestedPerAllocation,
                decimal totalAmountInvested,
                decimal expectedCurrentPercentage,
                decimal expectedPercentageDifference,
                decimal expectedAmountDifference)
    {
        // Arrange
        var allocationValues = new AllocationValues(targetPercentage);

        // Act
        allocationValues.RecalculateValues(amountInvestedPerAllocation, totalAmountInvested);

        // Assert
        Assert.Equal(expectedCurrentPercentage, allocationValues.CurrentPercentage);
        Assert.Equal(expectedPercentageDifference, allocationValues.PercentageDifference);
        Assert.Equal(expectedAmountDifference, allocationValues.AmountDifference);
    }

    [Fact(DisplayName = @"Given target percentage with X decimal places
                         When updating target percentage
                         Should update the percentage rounding down with two decimal places")]
    public void RoundTargetPercentageTest()
    {
        // Arrange
        _allocationValues = new(50);
        // Act
        _allocationValues.UpdateTargetPercentage(14.65688m);
        // Assert
        Assert.Equal(14.65m, _allocationValues.TargetPercentage);
    }
}