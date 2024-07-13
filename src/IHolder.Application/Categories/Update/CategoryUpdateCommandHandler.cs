using ErrorOr;
using IHolder.Application.Categories.Mappers;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Update;

public class CategoryUpdateCommandHandler(ICategoryRepository _categoryRepository) : IRequestHandler<CategoryUpdateCommand, ErrorOr<Category>>
{
    public async Task<ErrorOr<Category>> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        if (await _categoryRepository.ExistsByIdAsync(request.Id) is false)
            return Error.Conflict(description: "Category not found");

        await _categoryRepository.UpdateAsync(request.ToCategoryEntity());

        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null) return Error.Conflict(description: "Failed to retrieve the updated category.");

        return category;
    }
}
