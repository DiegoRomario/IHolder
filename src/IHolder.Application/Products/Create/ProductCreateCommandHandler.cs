using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Products.Mappers;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.Create;

public class ProductCreateCommandHandler(IProductRepository _repository) : IRequestHandler<ProductCreateCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var Product = request.ToProductEntity();

        await _repository.AddAsync(Product);

        Product = await _repository.GetByIdAsync(Product.Id);

        if (Product == null) return Error.Conflict(description: "Failed to retrieve the updated Product.");

        return Product;
    }
}
