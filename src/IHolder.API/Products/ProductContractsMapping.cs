using IHolder.Application.Products.Create;
using IHolder.Application.Products.List;
using IHolder.Application.Products.Update;
using IHolder.Contracts.Products;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;

namespace IHolder.API.Mappers.Products;

public static class ProductContractsMapping
{
    public static ProductResponse ToResponse(this Product Product)
    {
        return new ProductResponse(
            Product.Id,
            Product.Name,
            Product.Description,
            Product.CategoryId,
            Product.Category.Description,
            (byte)Product.Risk,
            Product.CreatedAt,
            Product.UpdatedAt);
    }

    public static ProductCreateCommand ToCreateCommand(this ProductCreateRequest request)
    {
        return new ProductCreateCommand(request.Name, request.Description, request.CategoryId, (Risk)request.Risk);
    }

    public static ProductUpdateCommand ToUpdateCommand(this ProductUpdateRequest request, Guid id)
    {
        return new ProductUpdateCommand(id, request.Name, request.Description, request.CategoryId, (Risk)request.Risk);
    }

    public static ProductPaginatedListQuery ToPaginatedListQuery(this ProductPaginatedListRequest request)
    {
        return new ProductPaginatedListQuery(new ProductPaginatedListFilter(request.Id, request.Name, request.Description, request.CategoryId, request.CategoryDescription, (Risk?)request.Risk, request.PageNumber, request.PageSize));
    }

    public static PaginatedList<ProductResponse> ToResponsePaginatedList(this PaginatedList<Product> Product)
    {
        var items = Product.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<ProductResponse>(items, Product.TotalCount, Product.PageNumber, Product.PageSize);
    }
}

