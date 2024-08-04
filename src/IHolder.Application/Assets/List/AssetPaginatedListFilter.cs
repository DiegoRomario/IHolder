using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Assets.List;

public record AssetPaginatedListFilter(
    Guid? Id,
    string? Name,
    string? Description,
    string? Ticker,
    decimal? MinPrice,
    decimal? MaxPrice,
    Guid? ProductId,
    string? ProductName,
    Guid? CategoryId,
    string? CategoryName,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
