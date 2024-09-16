using ErrorOr;
using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;
using MediatR;

namespace IHolder.Application.Categories.Delete;

public class CategoryDeleteCommandHandler(ICategoryRepository _repository, IProductRepository _productRepository) : IRequestHandler<CategoryDeleteCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(CategoryDeleteCommand request, CancellationToken ct)
    {
        var category = await _repository.GetByIdAsync(request.Id, ct);

        if (category is null) return Error.NotFound(description: "Category not found");

        var productsExists = await _productRepository.ExistsByPredicateAsync(p => p.CategoryId == request.Id, ct);

        if (productsExists) return Error.Conflict(description: "Unable to delete category. This category is linked to one or more products.");

        await _repository.DeleteAsync(category, ct);

        return Result.Deleted;
    }
}
