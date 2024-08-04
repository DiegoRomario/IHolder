using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Products.Mappers;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.Create;

public class ProductCreateCommandHandler(IProductRepository _repository) : IRequestHandler<ProductCreateCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ProductCreateCommand request, CancellationToken ct)
    {
        var Product = request.ToEntity();

        await _repository.AddAsync(Product, ct);

        Product = await _repository.GetByIdAsync(Product.Id, ct);

        if (Product == null) return Error.Conflict(description: "Failed to retrieve the updated Product.");

        return Product;
    }
}
