using ErrorOr;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.List;

public class CategoryListByIdQueryHandler(ICategoryRepository _repository) : IRequestHandler<CategoryGetByIdQuery, ErrorOr<Category?>>
{
    public async Task<ErrorOr<Category?>> Handle(CategoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);

        if (category is null) return Error.NotFound(description: "Category not found");

        return category;
    }
}
