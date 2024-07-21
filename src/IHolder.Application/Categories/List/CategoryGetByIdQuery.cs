using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.List;

[Authorization]
public record CategoryGetByIdQuery(Guid Id) : IRequest<ErrorOr<Category?>>;
