using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Categories.List;

[Authorization]
public record CategoryPaginatedListQuery(CategoryPaginatedListFilter Filter) : IRequest<ErrorOr<PaginatedList<Category>>>;