﻿using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Products.List;

public class ProductPaginatedListQueryHandler(IProductRepository _repository) : IRequestHandler<ProductPaginatedListQuery, ErrorOr<PaginatedList<Product>>>
{
    public async Task<ErrorOr<PaginatedList<Product>>> Handle(ProductPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}