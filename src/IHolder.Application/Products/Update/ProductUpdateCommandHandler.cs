using ErrorOr;
using IHolder.Application.Common;
using IHolder.Application.Products.Mappers;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.Update;

public class ProductUpdateCommandHandler(IProductRepository _repository) : IRequestHandler<ProductUpdateCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByIdAsync(request.Id) is false) return Error.Conflict(description: "Product not found");

        await _repository.UpdateAsync(request.ToProductEntity());

        var Product = await _repository.GetByIdAsync(request.Id);

        if (Product == null) return Error.Conflict(description: "Failed to retrieve the updated Product.");

        return Product;
    }
}
