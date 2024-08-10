using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Products.List;

public class ProductsPaginatedListQueryHandler(IProductRepository _repository) : IRequestHandler<ProductsPaginatedListQuery, ErrorOr<PaginatedList<Product>>>
{
    public async Task<ErrorOr<PaginatedList<Product>>> Handle(ProductsPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}