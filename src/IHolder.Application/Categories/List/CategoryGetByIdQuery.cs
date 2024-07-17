using ErrorOr;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.List;

public record CategoryGetByIdQuery(Guid Id) : IRequest<ErrorOr<Category?>>;
