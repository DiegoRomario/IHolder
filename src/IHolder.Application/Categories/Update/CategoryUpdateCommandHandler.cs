using ErrorOr;
using IHolder.Application.Categories.Mappers;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Update;

public class CategoryUpdateCommandHandler(ICategoryRepository _repository) : IRequestHandler<CategoryUpdateCommand, ErrorOr<Category>>
{
    public async Task<ErrorOr<Category>> Handle(CategoryUpdateCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsByIdAsync(request.Id, ct) is false)
            return Error.NotFound(description: "Category not found");

        await _repository.UpdateAsync(request.ToEntity(), ct);

        var category = await _repository.GetByIdAsync(request.Id, ct);

        if (category == null) return Error.Conflict(description: "Failed to retrieve the updated category.");

        return category;
    }
}
