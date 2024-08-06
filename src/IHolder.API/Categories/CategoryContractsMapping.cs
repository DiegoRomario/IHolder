using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.List;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;

namespace IHolder.API.Mappers.Categories;

public static class CategoryContractsMapping
{
    public static CategoryResponse ToResponse(this Category category)
    {
        return new CategoryResponse(category.Id, category.Name, category.Description, category.CreatedAt, category.UpdatedAt);
    }

    public static CategoryCreateCommand ToCreateCommand(this CategoryCreateRequest request)
    {
        return new CategoryCreateCommand(request.Name, request.Description);
    }

    public static CategoryUpdateCommand ToUpdateCommand(this CategoryUpdateRequest request, Guid id)
    {
        return new CategoryUpdateCommand(id, request.Name, request.Description);
    }

    public static CategoryPaginatedListQuery ToPaginatedListQuery(this CategoryPaginatedListRequest request)
    {
        return new CategoryPaginatedListQuery(new CategoryPaginatedListFilter(request.Id, request.Name, request.Description, request.PageNumber, request.PageSize));
    }

    public static PaginatedList<CategoryResponse> ToResponsePaginatedList(this PaginatedList<Category> category)
    {
        var items = category.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<CategoryResponse>(items, category.TotalCount, category.PageNumber, category.PageSize);
    }
}

