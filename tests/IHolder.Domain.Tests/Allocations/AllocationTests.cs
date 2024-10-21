using IHolder.Domain.Allocations;
using IHolder.Domain.Assets;
using IHolder.Domain.Categories;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Portfolios;
using IHolder.Domain.Products;

namespace IHolder.Domain.Tests.Allocations;

[Trait("Category", "unit")]
public class AllocationTests
{
    [Theory(DisplayName = @"Given an asset, target percentage, amount invested per asset and total amount invested,
                            When generating recommendation
                            Should update recommendation correctly")]
    [InlineData(25, 2000, 10000, Recommendation.Buy)]
    [InlineData(10, 8000, 10000, Recommendation.Sell)]
    [InlineData(50, 6000, 10000, Recommendation.Hold)]
    public void GenerateRecommendationPerAsset
        (decimal targetPercentage, decimal amountInvestedPerAllocation, decimal totalAmountInvested, Recommendation recommendation)
    {
        // Arrange
        Asset asset = new(Guid.NewGuid(), "Demo Company", "Stuff about Demo Company", "TEST3", 10);
        AssetInPortfolio assetInPortfolio = new(Guid.NewGuid(), asset.Id, asset.Price, 10);
        AllocationByAsset allocation = new(assetInPortfolio, targetPercentage);

        // Act
        allocation.GenerateRecommendation(amountInvestedPerAllocation, totalAmountInvested);

        // Assert
        Assert.Equal(allocation.Recommendation, recommendation);
    }


    [Theory(DisplayName = @"Given a product, target percentage, amount invested per product and total amount invested,
                            When generating recommendation
                            Should update recommendation correctly")]
    [InlineData(10, 3000, 50000, Recommendation.Buy)]
    [InlineData(20, 10000, 15000, Recommendation.Sell)]
    [InlineData(15, 1500, 10000, Recommendation.Hold)]
    public void GenerateRecommendationPerProduct
    (decimal targetPercentage, decimal amountInvestedPerAllocation, decimal totalAmountInvested, Recommendation recommendation)
    {
        // Arrange
        Product product = new("Product A", "Stuff about Produtct A", Guid.NewGuid(), Risk.Medium, "SA");
        AllocationByProduct allocation = new(product.Id, Guid.NewGuid(), targetPercentage);
        // Act
        allocation.GenerateRecommendation(amountInvestedPerAllocation, totalAmountInvested);
        // Assert
        Assert.Equal(allocation.Recommendation, recommendation);
    }

    [Theory(DisplayName = @"Given a category, target percentage, amount invested per category and total amount invested,
                            When generating recommendation
                            Should update recommendation correctly")]
    [InlineData(50, 15000, 8000000, Recommendation.Buy)]
    [InlineData(10, 80000, 100000, Recommendation.Sell)]
    [InlineData(20, 22000, 100000, Recommendation.Hold)]
    public void DadoPercentualObjetivoValorInvestidoPorTipoEValorTotalInvestido_DeveAtualizarOrientacaoCorretamente
     (decimal targetPercentage, decimal amountInvestedPerAllocation, decimal totalAmountInvested, Recommendation recommendation)
    {
        // Arrange
        Category category = new("Variable Income", "Stuff about variable income");
        AllocationByCategory allocation = new(category.Id, Guid.NewGuid(), targetPercentage);

        // Act
        allocation.GenerateRecommendation(amountInvestedPerAllocation, totalAmountInvested);

        // Assert
        Assert.Equal(allocation.Recommendation, recommendation);
    }

}