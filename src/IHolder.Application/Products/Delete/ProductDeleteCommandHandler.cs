using ErrorOr;
using IHolder.Application.Common.Interfaces;
using MediatR;

namespace IHolder.Application.Products.Delete;

public class ProductDeleteCommandHandler(IProductRepository _productRepository, IAssetRepository _assetRepository) : IRequestHandler<ProductDeleteCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(ProductDeleteCommand request, CancellationToken ct)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, ct);

        if (product is null) return Error.NotFound(description: "Product not found");

        var hasAllocations = await _productRepository.HasAllocationsAsync(request.Id, ct);
        if (hasAllocations) return Error.Conflict(description: "Unable to delete product. There are allocations for this product.");

        var assetsExists = await _assetRepository.ExistsByPredicateAsync(a => a.ProductId == request.Id, ct);
        if (assetsExists) return Error.Conflict(description: "Unable to delete product. This product is linked to one or more assets.");

        await _productRepository.DeleteAsync(product, ct);

        return Result.Deleted;
    }
}
