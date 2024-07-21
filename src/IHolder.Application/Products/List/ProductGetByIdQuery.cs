using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.List;

[Authorization]
public record ProductGetByIdQuery(Guid Id) : IRequest<ErrorOr<Product?>>;
