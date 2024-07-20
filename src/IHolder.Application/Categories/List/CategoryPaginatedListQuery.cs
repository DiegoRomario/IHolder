using ErrorOr;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Categories.List;

public record CategoryPaginatedListQuery(CategoryPaginatedListFilter Filter) : IRequest<ErrorOr<PaginatedList<Category>>>;