﻿using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.List;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;

namespace IHolder.API.Mappers.Categories;

public static class CategoryContractsMapping
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse(category.Id, category.Name, category.Description, category.CreatedAt, category.UpdatedAt);
    }

    public static CategoryCreateCommand ToCategoryCreateCommand(this CategoryCreateRequest request)
    {
        return new CategoryCreateCommand(request.Name, request.Description);
    }

    public static CategoryUpdateCommand ToCategoryUpdateCommand(this CategoryUpdateRequest request, Guid id)
    {
        return new CategoryUpdateCommand(id, request.Name, request.Description);
    }

    public static CategoryPaginatedListQuery ToCategoryPaginatedListQuery(this CategoryPaginatedListRequest request)
    {
        return new CategoryPaginatedListQuery(new CategoryPaginatedListFilter(request.Id, request.Name, request.Description, request.PageNumber, request.PageSize));
    }

    public static PaginatedList<CategoryResponse> ToCategoryResponsePaginatedList(this PaginatedList<Category> category)
    {
        var items = category.Items.Select(c => c.ToCategoryResponse()).ToList();
        return new PaginatedList<CategoryResponse>(items, category.TotalCount, category.PageNumber, category.PageSize);
    }
}

