using IHolder.Domain.Assets;

namespace IHolder.Domain.Tests.Assets;

[Trait("Category", "unit")]
public class AssetInPortfolioTests
{
    [Fact(DisplayName = @"Given new asset in portfolio with Average Price and Quantity
                          When creating a new instance of the entity (AssetInPortfolio)
                          Should calculate invested amount (per asset) correctly")]
    public void AssetInPortfolioConstructorTest()
    {
        // Arrange & Act (constructor calls CalculateInvestedAmount method)
        AssetInPortfolio assetInPortfolio = new(
            assetId: Guid.NewGuid(),
            averagePrice: 33.45M, quantity: 825,
            userId: Guid.NewGuid(),
            firstInvestmentDate: DateTime.Now);

        // Assert 
        Assert.Equal(27596.25M, assetInPortfolio.InvestedAmount, 2);
    }


    [Fact(DisplayName = @"Given average price and quantity with big values​​
                          When creating a new instance of the entity (AssetInPortfolio)
                          Should raise and argument exception")]
    public void AssetInPortfolioConstructorWithBigValuesTest()
    {
        // Arrange & Act
        var exception = Record.Exception(() =>
        {
            AssetInPortfolio assetInPortfolio = new(
            assetId: Guid.NewGuid(),
            averagePrice: 999999999999999999995M, quantity: 999999999999999999995M,
            userId: Guid.NewGuid());

        });

        // Assert
        Assert.IsType<ArgumentException>(exception);
        Assert.Equal("The values exceeds the maximum value allowed for its type. (Parameter 'InvestedAmount')", exception.Message);
    }

}