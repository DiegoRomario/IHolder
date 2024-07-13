using ErrorOr;
using IHolder.Application.Categories.Mappers;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Create;

public class CategoryCreateCommandHandler(ICategoryRepository _categoryRepository) : IRequestHandler<CategoryCreateCommand, ErrorOr<Category>>
{
    public async Task<ErrorOr<Category>> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        var category = request.ToCategoryEntity();

        await _categoryRepository.AddAsync(category);

        category = await _categoryRepository.GetByIdAsync(category.Id);

        if (category == null) return Error.Conflict(description: "Failed to retrieve the updated category.");

        return category;
    }
}
