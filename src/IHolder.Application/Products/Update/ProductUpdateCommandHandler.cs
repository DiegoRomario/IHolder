using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Products.Mappers;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.Update;

public class ProductUpdateCommandHandler(IProductRepository _repository) : IRequestHandler<ProductUpdateCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ProductUpdateCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsByIdAsync(request.Id, ct) is false)
            return Error.NotFound(description: "Product not found");

        await _repository.UpdateAsync(request.ToEntity(), ct);

        var Product = await _repository.GetByIdAsync(request.Id, ct);

        if (Product == null) return Error.Conflict(description: "Failed to retrieve the updated Product.");

        return Product;
    }
}
