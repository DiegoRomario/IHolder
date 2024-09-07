using IHolder.Domain.Allocations;
using IHolder.Domain.Categories.Events;
using IHolder.Domain.Portfolios.Events;
using IHolder.Domain.Products.Events;

namespace IHolder.Application.Allocations.Mappers;

public static class AllocationCommandsMapping
{
    public static AllocationByCategory ToEntity(this CategoryCreatedEvent categoryCreatedEvent, Guid portfolioId, decimal targetPercentage = 0)
    {
        return new AllocationByCategory(categoryCreatedEvent.CategoryId, portfolioId, targetPercentage);
    }

    public static AllocationByProduct ToEntity(this ProductCreatedEvent productCreatedEvent, Guid portfolioId, decimal targetPercentage = 0)
    {
        return new AllocationByProduct(productCreatedEvent.ProductId, portfolioId, targetPercentage);
    }

    public static AllocationByAsset ToEntity(this AssetInPortfolioAddedEvent assetCreatedEvent, decimal targetPercentage = 0)
    {
        return new AllocationByAsset(assetCreatedEvent.AssetInPortfolio, targetPercentage);
    }
}

