using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;

namespace IHolder.API.Mappers.Categories;

public static class CategoryContractsMapping
{
    public static CategoryCreateResponse ToCategoryResponse(this Category category)
    {
        return new CategoryCreateResponse(category.Id, category.Description, category.Details);
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

