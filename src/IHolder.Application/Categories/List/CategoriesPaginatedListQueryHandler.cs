using ErrorOr;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Categories.List;

public class CategoriesPaginatedListQueryHandler(ICategoryRepository _repository) : IRequestHandler<CategoriesPaginatedListQuery, ErrorOr<PaginatedList<Category>>>
{
    public async Task<ErrorOr<PaginatedList<Category>>> Handle(CategoriesPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}