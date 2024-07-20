using ErrorOr;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Categories.List;

public class CategoryPaginatedListQueryHandler(ICategoryRepository _repository) : IRequestHandler<CategoryPaginatedListQuery, ErrorOr<PaginatedList<Category>>>
{
    public async Task<ErrorOr<PaginatedList<Category>>> Handle(CategoryPaginatedListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetPaginatedAsync(request.Filter);
    }
}