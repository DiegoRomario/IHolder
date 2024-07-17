using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;

namespace IHolder.API.Mappers.Categories;

public static class CategoryContractsMapping
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse(category.Id, category.Description, category.Details, category.CreatedAt, category.UpdatedAt);
    }

    public static CategoryCreateCommand ToCategoryCreateCommand(this CategoryCreateRequest request)
    {
        return new CategoryCreateCommand(request.Description, request.Details);
    }

    public static CategoryUpdateCommand ToCategoryUpdateCommand(this CategoryUpdateRequest request, Guid id)
    {
        return new CategoryUpdateCommand(id, request.Description, request.Details);
    }
}

