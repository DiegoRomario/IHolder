using ErrorOr;
using IHolder.Application.Categories.Mappers;
using IHolder.Application.Common;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Create;

public class CategoryCreateCommandHandler(ICategoryRepository _repository) : IRequestHandler<CategoryCreateCommand, ErrorOr<Category>>
{
    public async Task<ErrorOr<Category>> Handle(CategoryCreateCommand request, CancellationToken ct)
    {
        var category = request.ToEntity();

        await _repository.AddAsync(category, ct);

        category = await _repository.GetByIdAsync(category.Id, ct);

        if (category == null) return Error.Conflict(description: "Failed to retrieve the updated category.");

        return category;
    }
}
