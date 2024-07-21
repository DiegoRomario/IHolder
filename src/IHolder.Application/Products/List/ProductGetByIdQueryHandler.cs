using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.List;

public class ProductListByIdQueryHandler(IProductRepository _repository) : IRequestHandler<ProductGetByIdQuery, ErrorOr<Product?>>
{
    public async Task<ErrorOr<Product?>> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
    {
        var Product = await _repository.GetByIdAsync(request.Id);

        if (Product is null) return Error.NotFound(description: "Product not found");

        return Product;
    }
}
