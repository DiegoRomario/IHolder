using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Products.List;

[Authorization]
public record ProductsPaginatedListQuery(ProductsPaginatedListFilter Filter) : IRequest<ErrorOr<PaginatedList<Product>>>;